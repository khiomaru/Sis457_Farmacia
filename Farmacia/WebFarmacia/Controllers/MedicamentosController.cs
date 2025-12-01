using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebFarmacia.Models;

namespace WebFarmacia.Controllers
{
    [Authorize]
    public class MedicamentosController : Controller
    {
        private readonly FarmaciaContext _context;

        public MedicamentosController(FarmaciaContext context)
        {
            _context = context;
        }

        // GET: Medicamentos
        public async Task<IActionResult> Index()
        {
            var medicamentos = _context.Medicamentos
                .Where(x => x.Estado != -1)
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdLaboratorioNavigation);

            return View(await medicamentos.ToListAsync());
        }

        // GET: Medicamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var medicamento = await _context.Medicamentos
                .Include(m => m.IdCategoriaNavigation)
                .Include(m => m.IdLaboratorioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medicamento == null)
                return NotFound();

            return View(medicamento);
        }

        // GET: Medicamentos/Create
        public IActionResult Create()
        {
            var categorias = _context.Categorias.Where(c => c.Estado == 1).ToList();
            var laboratorios = _context.Laboratorios.Where(l => l.Estado == 1).ToList();
            
            System.Diagnostics.Debug.WriteLine($"Categorías disponibles: {categorias.Count}");
            System.Diagnostics.Debug.WriteLine($"Laboratorios disponibles: {laboratorios.Count}");
            
            ViewBag.IdCategoria = new SelectList(categorias, "Id", "Nombre");
            ViewBag.IdLaboratorio = new SelectList(laboratorios, "Id", "Nombre");
            return View();
        }

        // POST: Medicamentos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCategoria,IdLaboratorio,Codigo,Nombre,Descripcion,Composicion,FechaVencimiento,Stock,PrecioVenta,RequiereReceta")] Medicamento medicamento)
        {
            try
            {
                // Establecer valores automáticos
                medicamento.UsuarioRegistro = User.Identity?.Name ?? "Sistema";
                medicamento.FechaRegistro = DateTime.Now;
                medicamento.Estado = 1;

                // Agregar a la base de datos
                _context.Medicamentos.Add(medicamento);
                await _context.SaveChangesAsync();

                // Redirigir al índice
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Si hay error, mostrar mensaje y recargar la vista
                ModelState.AddModelError("", $"Error al crear el medicamento: {ex.Message}");
                
                // Recargar dropdowns
                ViewBag.IdCategoria = new SelectList(_context.Categorias.Where(c => c.Estado == 1), "Id", "Nombre", medicamento.IdCategoria);
                ViewBag.IdLaboratorio = new SelectList(_context.Laboratorios.Where(l => l.Estado == 1), "Id", "Nombre", medicamento.IdLaboratorio);
                
                return View(medicamento);
            }
        }

        // GET: Medicamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var medicamento = await _context.Medicamentos.FindAsync(id);

            if (medicamento == null)
                return NotFound();

            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Nombre", medicamento.IdCategoria);
            ViewData["IdLaboratorio"] = new SelectList(_context.Laboratorios, "Id", "Nombre", medicamento.IdLaboratorio);

            return View(medicamento);
        }

        // POST: Medicamentos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCategoria,IdLaboratorio,Codigo,Nombre,Descripcion,Composicion,FechaVencimiento,Stock,PrecioVenta,RequiereReceta,Estado")] Medicamento medicamento)
        {
            if (id != medicamento.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var original = await _context.Medicamentos.FindAsync(id);

                    if (original == null)
                        return NotFound();

                    // Campos editables
                    original.IdCategoria = medicamento.IdCategoria;
                    original.IdLaboratorio = medicamento.IdLaboratorio;
                    original.Codigo = medicamento.Codigo;
                    original.Nombre = medicamento.Nombre;
                    original.Descripcion = medicamento.Descripcion;
                    original.Composicion = medicamento.Composicion;
                    original.FechaVencimiento = medicamento.FechaVencimiento;
                    original.Stock = medicamento.Stock;
                    original.PrecioVenta = medicamento.PrecioVenta;
                    original.RequiereReceta = medicamento.RequiereReceta;
                    original.Estado = medicamento.Estado;

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", $"Error al actualizar el medicamento: {ex.Message}");
                }
            }

            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Nombre", medicamento.IdCategoria);
            ViewData["IdLaboratorio"] = new SelectList(_context.Laboratorios, "Id", "Nombre", medicamento.IdLaboratorio);
            return View(medicamento);
        }

        // GET: Medicamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var medicamento = await _context.Medicamentos
                .Include(m => m.IdCategoriaNavigation)
                .Include(m => m.IdLaboratorioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medicamento == null)
                return NotFound();

            return View(medicamento);
        }

        // POST: Medicamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);

            if (medicamento != null)
            {
                medicamento.Estado = -1;
                medicamento.UsuarioRegistro = User.Identity?.Name ?? "Sistema";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicamentoExists(int id)
        {
            return _context.Medicamentos.Any(e => e.Id == id);
        }
    }
}
