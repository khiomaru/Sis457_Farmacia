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
