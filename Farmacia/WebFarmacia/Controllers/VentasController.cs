using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebFarmacia.Models;
using System.Linq;
using System.Threading.Tasks;

namespace WebFarmacia.Controllers
{
    [Authorize]
    public class VentasController : Controller
    {
        private readonly FarmaciaContext _context;

        public VentasController(FarmaciaContext context)
        {
            _context = context;
        }

        // Clase para manejar datos del carrito
        public class CarritoData
        {
            public List<CarritoItem> items { get; set; } = new List<CarritoItem>();
        }

        public class CarritoItem
        {
            public int medicamentoId { get; set; }
            public decimal precioUnitario { get; set; }
            public int cantidad { get; set; }
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var ventas = _context.Ventas.Where(x => x.Estado != -1).Include(v => v.IdUsuarioNavigation);
            return View(await ventas.ToListAsync());
        }

        // GET: Ventas/NuevaVenta
        public IActionResult NuevaVenta()
        {
            ViewData["Productos"] = _context.Medicamentos
                .Where(p => p.Estado == 1 && p.Stock > 0)
                .Select(p => new
                {
                    p.Id,
                    p.Nombre,
                    p.PrecioVenta,
                    p.Stock
                }).ToList();

            return View();
        }

        // Buscar cliente por documento
        [Route("Ventas/BuscarCliente")]
        [HttpGet]
        public JsonResult BuscarCliente(string? documento)
        {
            if (string.IsNullOrWhiteSpace(documento))
            {
                return Json(new { success = false, message = "Debe proporcionar un documento" });
            }

            var cliente = _context.Clientes
                .Where(c => c.CedulaIdentidad == documento)
                .Select(c => new
                {
                    c.NombreCompleto
                })
                .FirstOrDefault();

            if (cliente == null)
            {
                return Json(new { success = false, message = "Cliente no encontrado" });
            }

            return Json(new { success = true, cliente });
        }

        // Buscar medicamento por código
[HttpGet]
public JsonResult BuscarProducto(string? codigo = null, int? idMedicamento = null)
{
    if (string.IsNullOrWhiteSpace(codigo) && idMedicamento == null)
    {
        return Json(new { success = false, message = "Debe proporcionar un código o un ID de medicamento." });
    }

    var medicamento = _context.Medicamentos
        .Where(p => p.Estado == 1 && 
                    (p.Codigo == codigo || p.Id == idMedicamento)) // Filtra por código o ID
        .Select(p => new
        {
            p.Id, // ID del medicamento para usarlo al registrar la venta
            p.Nombre,
            p.Descripcion,
            p.PrecioVenta,
            p.Stock
        })
        .FirstOrDefault();

    if (medicamento == null)
    {
        return Json(new { success = false, message = "Medicamento no encontrado." });
    }

    return Json(new { success = true, producto = medicamento });
}





        // POST: Ventas/RegistrarVenta
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarVenta([FromBody] VentaViewModel ventaViewModel)
        {
            if (ventaViewModel == null || ventaViewModel.VentaDetalles == null || !ventaViewModel.VentaDetalles.Any())
            {
                return BadRequest(new { success = false, message = "Debe incluir al menos un producto en la venta." });
            }

            if (string.IsNullOrWhiteSpace(ventaViewModel.DocumentoCliente))
            {
                return BadRequest(new { success = false, message = "Debe proporcionar el documento del cliente." });
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Buscar el cliente por documento
                    var cliente = await _context.Clientes
                        .FirstOrDefaultAsync(c => c.CedulaIdentidad == ventaViewModel.DocumentoCliente && c.Estado == 1);

                    if (cliente == null)
                    {
                        return BadRequest(new { success = false, message = "Cliente no encontrado o inactivo." });
                    }

                    // Obtener el usuario autenticado
                    var usuarioNombre = User.Identity?.Name ?? "Sistema";
                    var usuario = await _context.Usuarios
                        .FirstOrDefaultAsync(u => u.Usuario1 == usuarioNombre && u.Estado == 1);

                    if (usuario == null)
                    {
                        return BadRequest(new { success = false, message = "Usuario no encontrado o inactivo." });
                    }

                    // Crear la venta
                    var nuevaVenta = new Venta
                    {
                        IdCliente = cliente.Id,
                        IdUsuario = usuario.IdUsuario,
                        Total = ventaViewModel.MontoTotal,
                        FechaVenta = DateTime.Now,
                        FechaRegistro = DateTime.Now,
                        UsuarioRegistro = usuarioNombre,
                        Estado = 1,
                        VentaDetalles = ventaViewModel.VentaDetalles.Select(vd => new VentaDetalle
                        {
                            IdMedicamento = vd.IdMedicamento,
                            PrecioUnitario = vd.PrecioUnitario,
                            Cantidad = (int)vd.Cantidad,
                            Estado = 1,
                            FechaRegistro = DateTime.Now,
                            UsuarioRegistro = usuarioNombre
                        }).ToList()
                    };

                    _context.Ventas.Add(nuevaVenta);
                    await _context.SaveChangesAsync();

                    // Procesar los detalles de la venta y actualizar stock
                    foreach (var detalle in nuevaVenta.VentaDetalles)
                    {
                        // Verificar que el medicamento existe
                        var medicamento = await _context.Medicamentos
                            .FirstOrDefaultAsync(p => p.Id == detalle.IdMedicamento);

                        if (medicamento == null)
                        {
                            await transaction.RollbackAsync();
                            return BadRequest(new { success = false, message = $"Medicamento con ID {detalle.IdMedicamento} no encontrado." });
                        }

                        // Verificar stock disponible
                        if (medicamento.Stock < detalle.Cantidad)
                        {
                            await transaction.RollbackAsync();
                            return BadRequest(new { success = false, message = $"Stock insuficiente para {medicamento.Nombre}. Disponible: {medicamento.Stock}, Solicitado: {detalle.Cantidad}." });
                        }

                        // Reducir el stock
                        medicamento.Stock -= detalle.Cantidad;

                        // Asignar el IdVenta al detalle
                        detalle.IdVenta = nuevaVenta.IdVenta;
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Ok(new { success = true, message = "Venta registrada exitosamente." });
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return BadRequest(new { success = false, message = "Error al registrar la venta.", error = ex.Message });
                }
            }
        }


        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventum = await _context.Ventas
                .Include(v => v.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (ventum == null)
            {
                return NotFound();
            }

            return View(ventum);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes.Where(c => c.Estado == 1), "IdCliente", "NombreCompleto");
            return View();
        }

        // GET: Ventas/GetMedicamentosJson
        [HttpGet]
        public IActionResult GetMedicamentosJson()
        {
            var medicamentos = _context.Medicamentos
                .Where(m => m.Estado == 1 && m.Stock > 0)
                .Select(m => new
                {
                    m.Id,
                    m.Codigo,
                    m.Nombre,
                    m.PrecioVenta,
                    m.Stock
                })
                .OrderBy(m => m.Nombre)
                .ToList();

            return Json(medicamentos);
        }

        // POST: Ventas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string carritoData, [Bind("IdCliente,NumeroFactura")] Venta venta)
        {
            if (string.IsNullOrEmpty(carritoData))
            {
                ModelState.AddModelError("", "El carrito está vacío");
                ViewData["IdCliente"] = new SelectList(_context.Clientes.Where(c => c.Estado == 1), "IdCliente", "NombreCompleto", venta.IdCliente);
                return View(venta);
            }

            try
            {
                // Deserializar datos del carrito
                var carrito = System.Text.Json.JsonSerializer.Deserialize<CarritoData>(carritoData);
                
                if (carrito == null || carrito.items == null || !carrito.items.Any())
                {
                    ModelState.AddModelError("", "El carrito está vacío");
                    ViewData["IdCliente"] = new SelectList(_context.Clientes.Where(c => c.Estado == 1), "IdCliente", "NombreCompleto", venta.IdCliente);
                    return View(venta);
                }

                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Obtener el usuario autenticado
                        var usuarioNombre = User.Identity?.Name ?? "Sistema";
                        var usuario = await _context.Usuarios
                            .FirstOrDefaultAsync(u => u.Usuario1 == usuarioNombre && u.Estado == 1);

                        if (usuario == null)
                        {
                            ModelState.AddModelError("", "Usuario no encontrado o inactivo");
                            ViewData["IdCliente"] = new SelectList(_context.Clientes.Where(c => c.Estado == 1), "IdCliente", "NombreCompleto", venta.IdCliente);
                            return View(venta);
                        }

                        // Calcular total
                        decimal total = 0;
                        foreach (var item in carrito.items)
                        {
                            total += item.precioUnitario * item.cantidad;
                        }

                        // Crear la venta
                        var nuevaVenta = new Venta
                        {
                            IdCliente = venta.IdCliente,
                            IdUsuario = usuario.IdUsuario,
                            NumeroFactura = venta.NumeroFactura,
                            Total = total,
                            FechaVenta = DateTime.Now,
                            FechaRegistro = DateTime.Now,
                            UsuarioRegistro = usuarioNombre,
                            Estado = 1
                        };

                        _context.Ventas.Add(nuevaVenta);
                        await _context.SaveChangesAsync();

                        // Procesar los detalles de la venta y actualizar stock
                        foreach (var item in carrito.items)
                        {
                            // Verificar que el medicamento existe
                            var medicamento = await _context.Medicamentos
                                .FirstOrDefaultAsync(m => m.Id == item.medicamentoId);

                            if (medicamento == null)
                            {
                                await transaction.RollbackAsync();
                                ModelState.AddModelError("", $"Medicamento con ID {item.medicamentoId} no encontrado");
                                ViewData["IdCliente"] = new SelectList(_context.Clientes.Where(c => c.Estado == 1), "IdCliente", "NombreCompleto", venta.IdCliente);
                                return View(venta);
                            }

                            // Verificar stock disponible
                            if (medicamento.Stock < item.cantidad)
                            {
                                await transaction.RollbackAsync();
                                ModelState.AddModelError("", $"Stock insuficiente para {medicamento.Nombre}. Disponible: {medicamento.Stock}, Solicitado: {item.cantidad}");
                                ViewData["IdCliente"] = new SelectList(_context.Clientes.Where(c => c.Estado == 1), "IdCliente", "NombreCompleto", venta.IdCliente);
                                return View(venta);
                            }

                            // Crear detalle de venta
                            var detalle = new VentaDetalle
                            {
                                IdVenta = nuevaVenta.IdVenta,
                                IdMedicamento = medicamento.Id,
                                Cantidad = item.cantidad,
                                PrecioUnitario = item.precioUnitario,
                                SubTotal = item.precioUnitario * item.cantidad,
                                FechaRegistro = DateTime.Now,
                                UsuarioRegistro = usuarioNombre,
                                Estado = 1
                            };

                            _context.VentaDetalles.Add(detalle);

                            // Actualizar stock
                            medicamento.Stock -= item.cantidad;
                        }

                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();

                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        ModelState.AddModelError("", "Error al procesar la venta");
                        ViewData["IdCliente"] = new SelectList(_context.Clientes.Where(c => c.Estado == 1), "IdCliente", "NombreCompleto", venta.IdCliente);
                        return View(venta);
                    }
                }
            }
            catch (System.Text.Json.JsonException)
            {
                ModelState.AddModelError("", "Error al procesar los datos del carrito");
                ViewData["IdCliente"] = new SelectList(_context.Clientes.Where(c => c.Estado == 1), "IdCliente", "NombreCompleto", venta.IdCliente);
                return View(venta);
            }
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventum = await _context.Ventas.FindAsync(id);
            if (ventum == null)
            {
                return NotFound();
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Usuario1", ventum.IdUsuario);
            return View(ventum);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVenta,IdUsuario,IdCliente,NumeroFactura,Total,FechaVenta,UsuarioRegistro,FechaRegistro,Estado")] Venta ventum)
        {
            if (id != ventum.IdVenta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Ventas.Update(ventum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(ventum.IdVenta))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Usuario1", ventum.IdUsuario);
            return View(ventum);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventum = await _context.Ventas
                .Include(v => v.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (ventum == null)
            {
                return NotFound();
            }

            return View(ventum);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ventum = await _context.Ventas.FindAsync(id);
            if (ventum != null)
            {
                ventum.UsuarioRegistro = User.Identity?.Name ?? "Sistema";
                ventum.Estado = -1;
                //_context.Venta.Remove(ventum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(int id)
        {
            return _context.Ventas.Any(e => e.IdVenta == id);
        }
    }



}

