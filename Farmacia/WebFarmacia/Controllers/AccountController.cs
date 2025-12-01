using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebFarmacia.Models;

namespace WebFarmacia.Controllers
{
    public class AccountController : Controller
        {
            private readonly FarmaciaContext _context;

            public AccountController(FarmaciaContext context)
            {
                _context = context;
            }

            [HttpGet]
            [AllowAnonymous]
            public IActionResult Login(string? returnUrl = null)
            {
                ViewData["ReturnUrl"] = returnUrl;
                return View();
            }

            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
            {
                ViewData["ReturnUrl"] = returnUrl;

                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Intentos de inicio de sesión no válidos.");
                    return View(model);
                }

                // Depuración: Verificar qué usuarios existen en la base de datos
                var todosUsuarios = _context.Usuarios.ToList();
                var usuarioEncontrado = _context.Usuarios.FirstOrDefault(x => x.Usuario1.ToLower() == model.Usuario.ToLower().Trim());
                
                // Depuración: Log de información
                System.Diagnostics.Debug.WriteLine($"Total de usuarios en BD: {todosUsuarios.Count}");
                System.Diagnostics.Debug.WriteLine($"Usuario buscado: {model.Usuario.ToLower().Trim()}");
                
                if (usuarioEncontrado != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Usuario encontrado en BD: {usuarioEncontrado.Usuario1}");
                    System.Diagnostics.Debug.WriteLine($"Estado del usuario: {usuarioEncontrado.Estado}");
                    System.Diagnostics.Debug.WriteLine($"Clave almacenada: {usuarioEncontrado.Clave}");
                    
                    var claveIngresadaEncriptada = Encrypt(model.Clave.Trim());
                    System.Diagnostics.Debug.WriteLine($"Clave ingresada encriptada: {claveIngresadaEncriptada}");
                    System.Diagnostics.Debug.WriteLine($"Las claves coinciden: {usuarioEncontrado.Clave == claveIngresadaEncriptada}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Usuario NO encontrado en la base de datos");
                }

                var usuario = _context.Usuarios
                    .Where(x => x.Estado == 1
                        && x.Usuario1.ToLower() == model.Usuario.ToLower().Trim()
                        && x.Clave == Encrypt(model.Clave.Trim()))
                    .FirstOrDefault();

                if (usuario != null)
                {
                    TempData["isLogged"] = true;
                    System.Diagnostics.Debug.WriteLine($"Inicio de sesión exitoso para: {usuario.Usuario1}");

                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Usuario1),
                    new Claim(ClaimTypes.Email, "vaca.noel@usfx.bo") // Puedes usar el email del usuario en lugar de un valor estático
                };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(15),
                        IsPersistent = model.Recordarme
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    if (returnUrl == null) returnUrl = ViewData["ReturnUrl"]?.ToString();
                    if (returnUrl != null) return Redirect(returnUrl);
                    else return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                {
                    ViewBag.ReturnUrl = returnUrl;
                    ModelState.AddModelError("", "Intentos de inicio de sesión no válidos.");
                    System.Diagnostics.Debug.WriteLine("Fallo en el inicio de sesión - Credenciales incorrectas");
                    return View(model);
                }
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Logout()
            {
                TempData["isLogged"] = false;
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        public static string Encrypt(string clearText)
        {
            // Usar un hash SHA256 en lugar de AES para mayor consistencia
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(clearText + "SIS457-1nf0!"));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}


