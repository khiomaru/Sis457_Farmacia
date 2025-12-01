using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFarmacia.Models;

namespace WebFarmacia.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly FarmaciaContext _context;

    public HomeController(ILogger<HomeController> logger, FarmaciaContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Para usuarios no autenticados, mostrar catálogo público
        if (User.Identity?.IsAuthenticated != true)
        {
            var medicamentos = await _context.Medicamentos
                .Include(m => m.IdCategoriaNavigation)
                .Include(m => m.IdLaboratorioNavigation)
                .Where(m => m.Stock > 0 && m.Estado == 1)
                .OrderByDescending(m => m.FechaRegistro)
                .Take(6)
                .ToListAsync();
            
            return View(medicamentos);
        }
        
        // Para usuarios autenticados, mostrar dashboard
        return View(new List<Medicamento>());
    }

    public async Task<IActionResult> Catalogo(string? categoria, string? busqueda, string? laboratorio, int pagina = 1)
    {
        // Tamaño de página para paginación
        int pageSize = 12;
        
        // Consulta base de medicamentos disponibles
        var query = _context.Medicamentos
            .Include(m => m.IdCategoriaNavigation)
            .Include(m => m.IdLaboratorioNavigation)
            .Where(m => m.Stock > 0 && m.Estado == 1);

        // Aplicar filtros
        if (!string.IsNullOrEmpty(categoria))
        {
            query = query.Where(m => m.IdCategoriaNavigation != null &&
                                    m.IdCategoriaNavigation.Nombre.Contains(categoria));
        }

        if (!string.IsNullOrEmpty(laboratorio))
        {
            query = query.Where(m => m.IdLaboratorioNavigation != null &&
                                    m.IdLaboratorioNavigation.Nombre.Contains(laboratorio));
        }

        if (!string.IsNullOrEmpty(busqueda))
        {
            query = query.Where(m => m.Nombre.Contains(busqueda) ||
                                    m.Descripcion.Contains(busqueda) ||
                                    m.Codigo.Contains(busqueda));
        }

        // Obtener categorías y laboratorios para los filtros
        ViewBag.Categorias = await _context.Categorias.Where(c => c.Estado == 1).ToListAsync();
        ViewBag.Laboratorios = await _context.Laboratorios.Where(l => l.Estado == 1).ToListAsync();
        
        // Guardar valores de filtros para mantenerlos en la vista
        ViewBag.CategoriaActual = categoria;
        ViewBag.LaboratorioActual = laboratorio;
        ViewBag.BusquedaActual = busqueda;

        // Total de resultados para paginación
        int totalItems = await query.CountAsync();
        int totalPages = totalItems > 0 ? (int)Math.Ceiling((double)totalItems / pageSize) : 1;

        // Aplicar paginación
        var medicamentos = await query
            .OrderByDescending(m => m.FechaRegistro)
            .Skip((pagina - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        // Pasar información de paginación a la vista
        ViewBag.PaginaActual = pagina;
        ViewBag.TotalPaginas = totalPages;
        ViewBag.TotalItems = totalItems;

        return View(medicamentos);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
