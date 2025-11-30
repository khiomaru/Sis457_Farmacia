using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebFarmacia.Models;

namespace WebFarmacia.Controllers
{
    public class DetalleVentasController : Controller
    {
        private readonly FarmaciaContext _context;

        public DetalleVentasController(FarmaciaContext context)
        {
            _context = context;
        }

        // GET: VentaDetalles
        public async Task<IActionResult> Index()
        {
            var ventaDetalles = _context.VentaDetalles.Where(x => x.Estado != -1).Include(v => v.IdMedicamentoNavigation).Include(v => v.IdVentaNavigation);
            return View(await ventaDetalles.ToListAsync());
        }

        // GET: VentaDetalles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.IdUsuarioNavigation)
                .Include(v => v.VentaDetalles) // Incluir detalles de la venta
                .ThenInclude(d => d.IdMedicamentoNavigation) // Incluir información del medicamento
                .FirstOrDefaultAsync(m => m.IdVenta == id);

            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // GET: VentaDetalles/Create
        public IActionResult Create()
        {
            ViewData["IdMedicamento"] = new SelectList(_context.Medicamentos, "Id", "Nombre");
            ViewData["IdVenta"] = new SelectList(_context.Ventas, "IdVenta", "IdVenta");
            return View();
        }

        // POST: VentaDetalles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetalleVenta,IdVenta,IdMedicamento,PrecioUnitario,Cantidad,SubTotal,UsuarioRegistro,FechaRegistro,Estado")] VentaDetalle ventaDetalle)
        {
            if (ModelState.IsValid)
            {
                ventaDetalle.UsuarioRegistro = User.Identity?.Name ?? "Sistema";
                ventaDetalle.FechaRegistro = DateTime.Now;
                ventaDetalle.Estado = 1;
                _context.VentaDetalles.Add(ventaDetalle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMedicamento"] = new SelectList(_context.Medicamentos, "Id", "Nombre", ventaDetalle.IdMedicamento);
            ViewData["IdVenta"] = new SelectList(_context.Ventas, "IdVenta", "IdVenta", ventaDetalle.IdVenta);
            return View(ventaDetalle);
        }

        // GET: VentaDetalles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventaDetalle = await _context.VentaDetalles.FindAsync(id);
            if (ventaDetalle == null)
            {
                return NotFound();
            }
            ViewData["IdMedicamento"] = new SelectList(_context.Medicamentos, "Id", "Nombre", ventaDetalle.IdMedicamento);
            ViewData["IdVenta"] = new SelectList(_context.Ventas, "IdVenta", "IdVenta", ventaDetalle.IdVenta);
            return View(ventaDetalle);
        }

        // POST: VentaDetalles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetalleVenta,IdVenta,IdMedicamento,PrecioUnitario,Cantidad,SubTotal,UsuarioRegistro,FechaRegistro,Estado")] VentaDetalle ventaDetalle)
        {
            if (id != ventaDetalle.IdDetalleVenta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.VentaDetalles.Update(ventaDetalle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaDetalleExists(ventaDetalle.IdDetalleVenta))
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
            ViewData["IdMedicamento"] = new SelectList(_context.Medicamentos, "Id", "Nombre", ventaDetalle.IdMedicamento);
            ViewData["IdVenta"] = new SelectList(_context.Ventas, "IdVenta", "IdVenta", ventaDetalle.IdVenta);
            return View(ventaDetalle);
        }

        // GET: VentaDetalles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventaDetalle = await _context.VentaDetalles
                .Include(v => v.IdMedicamentoNavigation)
                .Include(v => v.IdVentaNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalleVenta == id);
            if (ventaDetalle == null)
            {
                return NotFound();
            }

            return View(ventaDetalle);
        }

        // POST: VentaDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ventaDetalle = await _context.VentaDetalles.FindAsync(id);
            if (ventaDetalle != null)
            {
                _context.VentaDetalles.Remove(ventaDetalle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaDetalleExists(int id)
        {
            return _context.VentaDetalles.Any(e => e.IdDetalleVenta == id);
        }
    }
}
