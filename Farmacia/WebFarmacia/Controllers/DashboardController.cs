using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFarmacia.Models;
using WebFarmacia.Services;

namespace WebFarmacia.Controllers
{
    [Authorize]
    [AuthorizeRole(Rol.ADMIN, Rol.EMPLEADO)]
    public class DashboardController : Controller
    {
        private readonly FarmaciaContext _context;

        public DashboardController(FarmaciaContext context)
        {
            _context = context;
        }

        // GET: Dashboard
        public async Task<IActionResult> Index()
        {
            var viewModel = new DashboardViewModel();
            
            try
            {
                // Reservas pendientes
                viewModel.ReservasPendientes = await _context.Reservas
                    .Where(r => r.Estado == "PENDIENTE")
                    .CountAsync();

                // Reservas confirmadas
                viewModel.ReservasConfirmadas = await _context.Reservas
                    .Where(r => r.Estado == "CONFIRMADA")
                    .CountAsync();

                // Reservas canceladas
                viewModel.ReservasCanceladas = await _context.Reservas
                    .Where(r => r.Estado == "CANCELADA")
                    .CountAsync();

                // Medicamentos con bajo stock
                viewModel.MedicamentosBajoStock = await _context.Medicamentos
                    .Where(m => m.Estado == 1 && m.Stock <= 10)
                    .CountAsync();

                // Total de reservas del mes actual
                var currentDate = DateTime.Now;
                var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
                
                viewModel.ReservasMesActual = await _context.Reservas
                    .Where(r => r.FechaRegistro >= firstDayOfMonth && r.FechaRegistro <= currentDate)
                    .CountAsync();

                // Total de ventas del mes actual
                viewModel.VentasMesActual = await _context.Ventas
                    .Where(v => v.FechaVenta >= firstDayOfMonth && v.FechaVenta <= currentDate)
                    .CountAsync();

                // Ingresos totales del mes actual
                viewModel.IngresosMesActual = await _context.Ventas
                    .Where(v => v.FechaVenta >= firstDayOfMonth && v.FechaVenta <= currentDate)
                    .SumAsync(v => v.Total);

                // Clientes activos
                viewModel.ClientesActivos = await _context.Clientes
                    .Where(c => c.Estado == 1)
                    .CountAsync();

                // Top 5 medicamentos más reservados
                viewModel.MedicamentosMasReservados = await _context.Reservas
                    .Include(r => r.IdMedicamentoNavigation)
                    .Where(r => r.FechaRegistro >= DateTime.Now.AddDays(-30))
                    .GroupBy(r => r.IdMedicamentoNavigation)
                    .OrderByDescending(g => g.Count())
                    .Take(5)
                    .Select(g => new MedicamentoReservadoViewModel
                    {
                        Nombre = g.Key.Nombre,
                        CantidadReservada = g.Sum(r => r.Cantidad)
                    })
                    .ToListAsync();

                // Reservas por estado (últimos 7 días)
                viewModel.ReservasUltimos7Dias = await _context.Reservas
                    .Where(r => r.FechaRegistro >= DateTime.Now.AddDays(-7))
                    .GroupBy(r => r.Estado)
                    .Select(g => new ReservaPorEstadoViewModel
                    {
                        Estado = g.Key,
                        Cantidad = g.Count()
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log del error
                ViewBag.ErrorMessage = "Error al cargar el dashboard: " + ex.Message;
            }

            return View(viewModel);
        }
    }

    public class DashboardViewModel
    {
        public int ReservasPendientes { get; set; }
        public int ReservasConfirmadas { get; set; }
        public int ReservasCanceladas { get; set; }
        public int MedicamentosBajoStock { get; set; }
        public int ReservasMesActual { get; set; }
        public int VentasMesActual { get; set; }
        public decimal IngresosMesActual { get; set; }
        public int ClientesActivos { get; set; }
        public System.Collections.Generic.List<MedicamentoReservadoViewModel> MedicamentosMasReservados { get; set; } = new();
        public System.Collections.Generic.List<ReservaPorEstadoViewModel> ReservasUltimos7Dias { get; set; } = new();
    }

    public class MedicamentoReservadoViewModel
    {
        public string Nombre { get; set; } = string.Empty;
        public int CantidadReservada { get; set; }
    }

    public class ReservaPorEstadoViewModel
    {
        public string Estado { get; set; } = string.Empty;
        public int Cantidad { get; set; }
    }
}