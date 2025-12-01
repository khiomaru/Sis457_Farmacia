using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebFarmacia.Models;
using WebFarmacia.Services;

namespace WebFarmacia.Controllers
{
    [Authorize]
    [AuthorizeRole(Rol.EMPLEADO, Rol.ADMIN)]
    public class ReservasController : Controller
    {
        private readonly FarmaciaContext _context;
        private readonly IEmailService _emailService;
    
        public ReservasController(FarmaciaContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: Reservas
        public async Task<IActionResult> Index(string searchString, string estadoFilter, string fechaInicio, string fechaFin)
        {
            var query = _context.Reservas
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdMedicamentoNavigation)
                .AsQueryable();

            // Filtro por búsqueda (cliente, medicamento, cédula)
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(r =>
                    r.IdClienteNavigation.Nombres.Contains(searchString) ||
                    r.IdClienteNavigation.Apellidos.Contains(searchString) ||
                    r.IdClienteNavigation.CedulaIdentidad.Contains(searchString) ||
                    r.IdMedicamentoNavigation.Nombre.Contains(searchString) ||
                    r.Motivo.Contains(searchString));
            }

            // Filtro por estado
            if (!string.IsNullOrEmpty(estadoFilter) && estadoFilter != "all")
            {
                query = query.Where(r => r.Estado == estadoFilter);
            }

            // Filtro por fecha de inicio
            if (!string.IsNullOrEmpty(fechaInicio))
            {
                if (DateTime.TryParse(fechaInicio, out DateTime fechaInicioParsed))
                {
                    query = query.Where(r => r.FechaReserva >= fechaInicioParsed);
                }
            }

            // Filtro por fecha de fin
            if (!string.IsNullOrEmpty(fechaFin))
            {
                if (DateTime.TryParse(fechaFin, out DateTime fechaFinParsed))
                {
                    query = query.Where(r => r.FechaReserva <= fechaFinParsed);
                }
            }

            // Excluir canceladas por defecto (a menos que se filtre específicamente)
            if (string.IsNullOrEmpty(estadoFilter) || estadoFilter != "CANCELADA")
            {
                query = query.Where(r => r.Estado != "CANCELADA");
            }

            var reservas = await query.OrderByDescending(r => r.FechaRegistro).ToListAsync();

            // Guardar los filtros en ViewBag para mantenerlos en la vista
            ViewData["SearchString"] = searchString;
            ViewData["EstadoFilter"] = estadoFilter;
            ViewData["FechaInicio"] = fechaInicio;
            ViewData["FechaFin"] = fechaFin;

            return View(reservas);
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdMedicamentoNavigation)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes.Where(c => c.Estado == 1), "Id", "NombreCompleto");
            ViewData["IdMedicamento"] = new SelectList(_context.Medicamentos.Where(m => m.Estado == 1 && m.Stock > 0), "Id", "Nombre");
            return View();
        }

        // POST: Reservas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCliente,IdMedicamento,Cantidad,Motivo,Notas,FechaReserva,FechaVencimientoReserva,TelefonoContacto,EmailContacto,Estado,UsuarioRegistro,FechaRegistro")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                // Validar stock disponible
                var medicamento = await _context.Medicamentos.FindAsync(reserva.IdMedicamento);
                if (medicamento == null || medicamento.Stock < reserva.Cantidad)
                {
                    ModelState.AddModelError("", "No hay suficiente stock disponible para esta reserva");
                    ViewData["IdCliente"] = new SelectList(_context.Clientes.Where(c => c.Estado == 1), "Id", "NombreCompleto", reserva.IdCliente);
                    ViewData["IdMedicamento"] = new SelectList(_context.Medicamentos.Where(m => m.Estado == 1 && m.Stock > 0), "Id", "Nombre", reserva.IdMedicamento);
                    return View(reserva);
                }

                // Validar fechas
                if (reserva.FechaReserva < DateTime.Today)
                {
                    ModelState.AddModelError("", "La fecha de reserva no puede ser anterior a hoy");
                    ViewData["IdCliente"] = new SelectList(_context.Clientes.Where(c => c.Estado == 1), "Id", "NombreCompleto", reserva.IdCliente);
                    ViewData["IdMedicamento"] = new SelectList(_context.Medicamentos.Where(m => m.Estado == 1 && m.Stock > 0), "Id", "Nombre", reserva.IdMedicamento);
                    return View(reserva);
                }

                if (reserva.FechaVencimientoReserva < reserva.FechaReserva)
                {
                    ModelState.AddModelError("", "La fecha de vencimiento de la reserva debe ser posterior a la fecha de reserva");
                    ViewData["IdCliente"] = new SelectList(_context.Clientes.Where(c => c.Estado == 1), "Id", "NombreCompleto", reserva.IdCliente);
                    ViewData["IdMedicamento"] = new SelectList(_context.Medicamentos.Where(m => m.Estado == 1 && m.Stock > 0), "Id", "Nombre", reserva.IdMedicamento);
                    return View(reserva);
                }

                reserva.UsuarioRegistro = User.Identity?.Name ?? "Sistema";
                reserva.FechaRegistro = DateTime.Now;
                reserva.Estado = "PENDIENTE";

                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes.Where(c => c.Estado == 1), "Id", "NombreCompleto", reserva.IdCliente);
            ViewData["IdMedicamento"] = new SelectList(_context.Medicamentos.Where(m => m.Estado == 1 && m.Stock > 0), "Id", "Nombre", reserva.IdMedicamento);
            return View(reserva);
        }

        // GET: Reservas/Confirmar/5
        public async Task<IActionResult> Confirmar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Confirmar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirmar(int id)
        {
            var reserva = await _context.Reservas
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdMedicamentoNavigation)
                .FirstOrDefaultAsync(r => r.IdReserva == id);
            
            if (reserva == null)
            {
                return NotFound();
            }

            // Validar stock disponible nuevamente
            if (reserva.IdMedicamentoNavigation == null || reserva.IdMedicamentoNavigation.Stock < reserva.Cantidad)
            {
                ModelState.AddModelError("", "No hay suficiente stock disponible para confirmar esta reserva");
                return View(reserva);
            }

            reserva.Estado = "CONFIRMADA";
            await _context.SaveChangesAsync();

            // Enviar email de confirmación
            if (!string.IsNullOrEmpty(reserva.EmailContacto))
            {
                try
                {
                    await _emailService.SendReservaConfirmationAsync(reserva, reserva.EmailContacto);
                    TempData["SuccessMessage"] = "Reserva confirmada y email enviado al cliente.";
                }
                catch
                {
                    TempData["SuccessMessage"] = "Reserva confirmada, pero no se pudo enviar el email.";
                }
            }
            else
            {
                TempData["SuccessMessage"] = "Reserva confirmada correctamente.";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Reservas/Cancelar/5
        public async Task<IActionResult> Cancelar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Cancelar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancelar(int id)
        {
            var reserva = await _context.Reservas
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdMedicamentoNavigation)
                .FirstOrDefaultAsync(r => r.IdReserva == id);
            
            if (reserva == null)
            {
                return NotFound();
            }

            reserva.Estado = "CANCELADA";
            await _context.SaveChangesAsync();

            // Enviar email de cancelación
            if (!string.IsNullOrEmpty(reserva.EmailContacto))
            {
                try
                {
                    await _emailService.SendReservaCancelationAsync(reserva, reserva.EmailContacto);
                    TempData["SuccessMessage"] = "Reserva cancelada y email enviado al cliente.";
                }
                catch
                {
                    TempData["SuccessMessage"] = "Reserva cancelada, pero no se pudo enviar el email.";
                }
            }
            else
            {
                TempData["SuccessMessage"] = "Reserva cancelada correctamente.";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdMedicamentoNavigation)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            
            ViewData["IdCliente"] = new SelectList(_context.Clientes.Where(c => c.Estado == 1), "Id", "NombreCompleto", reserva.IdCliente);
            ViewData["IdMedicamento"] = new SelectList(_context.Medicamentos.Where(m => m.Estado == 1 && m.Stock > 0), "Id", "Nombre", reserva.IdMedicamento);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReserva,IdCliente,IdMedicamento,Cantidad,Motivo,Notas,FechaReserva,FechaVencimientoReserva,TelefonoContacto,EmailContacto,Estado,UsuarioRegistro,FechaRegistro,Total")] Reserva reserva)
        {
            if (id != reserva.IdReserva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Validar stock disponible
                    var medicamento = await _context.Medicamentos.FindAsync(reserva.IdMedicamento);
                    if (medicamento == null || medicamento.Stock < reserva.Cantidad)
                    {
                        ModelState.AddModelError("", "No hay suficiente stock disponible para esta reserva");
                        ViewData["IdCliente"] = new SelectList(_context.Clientes.Where(c => c.Estado == 1), "Id", "NombreCompleto", reserva.IdCliente);
                        ViewData["IdMedicamento"] = new SelectList(_context.Medicamentos.Where(m => m.Estado == 1 && m.Stock > 0), "Id", "Nombre", reserva.IdMedicamento);
                        return View(reserva);
                    }

                    // Validar fechas
                    if (reserva.FechaReserva < DateTime.Today)
                    {
                        ModelState.AddModelError("", "La fecha de reserva no puede ser anterior a hoy");
                        ViewData["IdCliente"] = new SelectList(_context.Clientes.Where(c => c.Estado == 1), "Id", "NombreCompleto", reserva.IdCliente);
                        ViewData["IdMedicamento"] = new SelectList(_context.Medicamentos.Where(m => m.Estado == 1 && m.Stock > 0), "Id", "Nombre", reserva.IdMedicamento);
                        return View(reserva);
                    }

                    if (reserva.FechaVencimientoReserva < reserva.FechaReserva)
                    {
                        ModelState.AddModelError("", "La fecha de vencimiento de la reserva debe ser posterior a la fecha de reserva");
                        ViewData["IdCliente"] = new SelectList(_context.Clientes.Where(c => c.Estado == 1), "Id", "NombreCompleto", reserva.IdCliente);
                        ViewData["IdMedicamento"] = new SelectList(_context.Medicamentos.Where(m => m.Estado == 1 && m.Stock > 0), "Id", "Nombre", reserva.IdMedicamento);
                        return View(reserva);
                    }

                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.IdReserva))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes.Where(c => c.Estado == 1), "Id", "NombreCompleto", reserva.IdCliente);
            ViewData["IdMedicamento"] = new SelectList(_context.Medicamentos.Where(m => m.Estado == 1 && m.Stock > 0), "Id", "Nombre", reserva.IdMedicamento);
            return View(reserva);
        }

        // GET: Reservas/GetMedicamentoInfo/5
        [AllowAnonymous]
        public async Task<IActionResult> GetMedicamentoInfo(int id)
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento == null)
            {
                return Json(new { stock = 0 });
            }
            
            return Json(new { stock = medicamento.Stock });
        }

        // GET: Reservas/CalculateTotal
        [AllowAnonymous]
        public async Task<IActionResult> CalculateTotal(int medicamentoId, int cantidad)
        {
            var medicamento = await _context.Medicamentos.FindAsync(medicamentoId);
            if (medicamento == null)
            {
                return Json(new { total = 0 });
            }
            
            var total = medicamento.PrecioVenta * cantidad;
            return Json(new { total = total });
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.IdReserva == id);
        }
    }
}