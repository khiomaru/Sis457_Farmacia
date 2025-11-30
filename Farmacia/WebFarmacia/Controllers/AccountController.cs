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
        [Authorize]
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

                var usuario = _context.Usuarios
                    .Where(x => x.Estado == 1 && x.Usuario1 == model.Usuario && x.Clave == Encrypt(model.Clave))
                    .FirstOrDefault();

                if (usuario != null)
                {
                    TempData["isLogged"] = true;

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
            string EncryptionKey = "SIS457-1nf0!";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                byte[] salt = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };
                byte[] key = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(EncryptionKey), salt, 10000, HashAlgorithmName.SHA256, 32);
                byte[] iv = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(EncryptionKey), salt, 10000, HashAlgorithmName.SHA256, 16);
                encryptor.Key = key;
                encryptor.IV = iv;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
    }
}


