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
    public class ProductosController : Controller
    {
        private readonly FarmaciaContext _context;


        public ProductosController(FarmaciaContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            var farmaciaContext = _context.Medicamentos.Where(x => x.Estado != -1).Include(p => p.IdCategoriaNavigation);
            return View(await farmaciaContext.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

          


            var producto = await _context.Medicamentos
                .Include(p => p.IdCategoriaNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "Descripcion");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducto,Codigo,Nombre,Descripcion,IdCategoria,Stock,PrecioVenta,UsuarioRegistro,FechaRegistro,Estado")] Producto producto)
        {
            if (!string.IsNullOrEmpty(producto.Codigo) && !string.IsNullOrEmpty(producto.Descripcion))
            {
                producto.UsuarioRegistro = User.Identity.Name;
                producto.FechaRegistro = DateTime.Now;
                producto.Estado = 1;
                _context.Medicamentos.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "Descripcion", producto.IdCategoria);
            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Solo recuperamos el producto sin incluir la navegación
            var producto = await _context.Medicamentos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            // Cargamos la lista de categorías, si es necesario
            ViewData["IdCategoria"] = new SelectList(_context.Categorias.Select(c => new { c.IdCategoria, c.Descripcion }), "IdCategoria", "Descripcion", producto.IdCategoria);
            return View(producto);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProducto,Codigo,Nombre,Descripcion,IdCategoria,Stock,PrecioVenta,UsuarioRegistro,FechaRegistro,Estado")] Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Obtenemos el producto actual de la base de datos
                    var productoDb = await _context.Medicamentos.FindAsync(id);
                    if (productoDb == null)
                    {
                        return NotFound();
                    }

                    // Actualizamos solo los campos relevantes del producto
                    productoDb.Codigo = producto.Codigo;
                    productoDb.Nombre = producto.Nombre;
                    productoDb.Descripcion = producto.Descripcion;
                    productoDb.IdCategoria = producto.IdCategoria;
                    productoDb.Stock = producto.Stock;
                    productoDb.PrecioVenta = producto.PrecioVenta;

                    // Guardamos los cambios en la base de datos
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", $"Error al actualizar el producto: {ex.Message}");
                }
            }

            // Recargamos las categorías si hay un error
            ViewData["IdCategoria"] = new SelectList(_context.Categorias.Select(c => new { c.IdCategoria, c.Descripcion }), "IdCategoria", "Descripcion", producto.IdCategoria);
            return View(producto);
        }


        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Medicamentos
                .Include(p => p.IdCategoriaNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Medicamentos.FindAsync(id);
            if (producto != null)
            {
                producto.UsuarioRegistro = User.Identity.Name; ;
                producto.Estado = -1;
                //_context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Medicamentos.Any(e => e.IdProducto == id);
        }
    }
}
