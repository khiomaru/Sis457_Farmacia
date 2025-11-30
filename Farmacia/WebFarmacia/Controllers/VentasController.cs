using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebFarmacia.Models;
using System.Linq;
using System.Threading.Tasks;

namespace WebFarmacia.Controllers
{
    public class VentasController : Controller
    {
        private readonly FarmaciaContext _context;

        public VentasController(FarmaciaContext context)
        {
            _context = context;
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
        public async Task<IActionResult> RegistrarVenta([FromBody] Venta nuevaVenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Los datos enviados no son válidos." });
            }

            if (nuevaVenta.VentaDetalles == null || !nuevaVenta.VentaDetalles.Any())
            {
                return BadRequest(new { success = false, message = "Debe incluir al menos un producto en la venta." });
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Registrar la venta
                    nuevaVenta.FechaRegistro = DateTime.Now;
                    nuevaVenta.UsuarioRegistro = User.Identity?.Name ?? "Sistema";
                    nuevaVenta.Estado = 1;
                    _context.Ventas.Add(nuevaVenta);
                    await _context.SaveChangesAsync();

                    // Procesar los detalles de la venta
                    foreach (var detalle in nuevaVenta.VentaDetalles)
                    {
                        // Convertir Código a IdMedicamento si es necesario
                        var medicamento = await _context.Medicamentos
                            .FirstOrDefaultAsync(p => p.Codigo == detalle.IdMedicamento.ToString());

                        if (medicamento == null)
                        {
                            return BadRequest(new { success = false, message = $"Medicamento con código {detalle.IdMedicamento} no encontrado." });
                        }

                        detalle.IdMedicamento = medicamento.Id; // Asignar el ID real
                        detalle.IdVenta = nuevaVenta.IdVenta;
                        detalle.FechaRegistro = DateTime.Now;
                        detalle.UsuarioRegistro = nuevaVenta.UsuarioRegistro;
                        detalle.Estado = 1;

                        _context.VentaDetalles.Add(detalle);
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
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Usuario1");
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVenta,IdUsuario,IdCliente,NumeroFactura,Total,UsuarioRegistro,FechaRegistro,Estado")] Venta ventum)
        {
            if (ModelState.IsValid)
            {
                ventum.UsuarioRegistro = User.Identity?.Name ?? "Sistema";
                ventum.FechaRegistro = DateTime.Now;
                ventum.Estado = 1;
                _context.Ventas.Add(ventum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Usuario1", ventum.IdUsuario);
            return View(ventum);
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

