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
            {
                return NotFound();
            }

            var medicamento = await _context.Medicamentos
                .Include(m => m.IdCategoriaNavigation)
                .Include(m => m.IdLaboratorioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (medicamento == null)
            {
                return NotFound();
            }

            return View(medicamento);
        }

        // GET: Medicamentos/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Nombre");
            ViewData["IdLaboratorio"] = new SelectList(_context.Laboratorios, "Id", "Nombre");
            return View();
        }

        // POST: Medicamentos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCategoria,IdLaboratorio,Codigo,Nombre,Descripcion,Composicion,FechaVencimiento,Stock,PrecioVenta,RequiereReceta")] Medicamento medicamento)
        {
            if (ModelState.IsValid)
            {
                medicamento.UsuarioRegistro = User.Identity?.Name ?? "Sistema";
                medicamento.FechaRegistro = DateTime.Now;
                medicamento.Estado = 1;
                _context.Medicamentos.Add(medicamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Nombre", medicamento.IdCategoria);
            ViewData["IdLaboratorio"] = new SelectList(_context.Laboratorios, "Id", "Nombre", medicamento.IdLaboratorio);
            return View(medicamento);
        }

        // GET: Medicamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento == null)
            {
                return NotFound();
            }

            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Nombre", medicamento.IdCategoria);
            ViewData["IdLaboratorio"] = new SelectList(_context.Laboratorios, "Id", "Nombre", medicamento.IdLaboratorio);
            return View(medicamento);
        }

        // POST: Medicamentos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCategoria,IdLaboratorio,Codigo,Nombre,Descripcion,Composicion,FechaVencimiento,Stock,PrecioVenta,RequiereReceta,UsuarioRegistro,FechaRegistro,Estado")] Medicamento medicamento)
        {
            if (id != medicamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var medicamentoDb = await _context.Medicamentos.FindAsync(id);
                    if (medicamentoDb == null)
                    {
                        return NotFound();
                    }

                    medicamentoDb.IdCategoria = medicamento.IdCategoria;
                    medicamentoDb.IdLaboratorio = medicamento.IdLaboratorio;
                    medicamentoDb.Codigo = medicamento.Codigo;
                    medicamentoDb.Nombre = medicamento.Nombre;
                    medicamentoDb.Descripcion = medicamento.Descripcion;
                    medicamentoDb.Composicion = medicamento.Composicion;
                    medicamentoDb.FechaVencimiento = medicamento.FechaVencimiento;
                    medicamentoDb.Stock = medicamento.Stock;
                    medicamentoDb.PrecioVenta = medicamento.PrecioVenta;
                    medicamentoDb.RequiereReceta = medicamento.RequiereReceta;

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
            {
                return NotFound();
            }

            var medicamento = await _context.Medicamentos
                .Include(m => m.IdCategoriaNavigation)
                .Include(m => m.IdLaboratorioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (medicamento == null)
            {
                return NotFound();
            }

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
                medicamento.UsuarioRegistro = User.Identity?.Name ?? "Sistema";
                medicamento.Estado = -1;
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
