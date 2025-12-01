using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebFarmacia.Models;
using WebFarmacia.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<FarmaciaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = new PathString("/Account/Login"));

builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddRazorPages();

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<FarmaciaContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    
    try
    {
        // Forzar la recreación de la base de datos para usar el nuevo método de encriptación
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        logger.LogInformation("Base de datos creada/verificada correctamente.");

        // Seed initial data if not exists
        if (!context.Categorias.Any())
        {
            context.Categorias.AddRange(
                new Categoria { Nombre = "Antibióticos", Descripcion = "Medicamentos antibacterianos", Estado = 1 },
                new Categoria { Nombre = "Analgésicos", Descripcion = "Medicamentos para el dolor", Estado = 1 },
                new Categoria { Nombre = "Antiinflamatorios", Descripcion = "Medicamentos antiinflamatorios", Estado = 1 },
                new Categoria { Nombre = "Antipiréticos", Descripcion = "Medicamentos para la fiebre", Estado = 1 },
                new Categoria { Nombre = "Vitaminas", Descripcion = "Suplementos vitamínicos", Estado = 1 }
            );
            logger.LogInformation("Categorías iniciales agregadas.");
        }

        if (!context.Laboratorios.Any())
        {
            context.Laboratorios.AddRange(
                new Laboratorio { Nombre = "Bayer", Pais = "Alemania", Estado = 1 },
                new Laboratorio { Nombre = "Pfizer", Pais = "Estados Unidos", Estado = 1 },
                new Laboratorio { Nombre = "Roche", Pais = "Suiza", Estado = 1 },
                new Laboratorio { Nombre = "GSK", Pais = "Reino Unido", Estado = 1 }
            );
            logger.LogInformation("Laboratorios iniciales agregados.");
        }

        if (!context.Empleados.Any())
        {
            var empleado = new Empleado
            {
                CedulaIdentidad = "9876543",
                Nombres = "Adolfo",
                PrimerApellido = "Soto",
                SegundoApellido = "-",
                Direccion = "Av. Los Leones #321",
                Celular = 69876543,
                Cargo = "Farmacéutico",
                UsuarioRegistro = "system",
                FechaRegistro = DateTime.Now,
                Estado = 1
            };
            context.Empleados.Add(empleado);
            logger.LogInformation("Empleado inicial agregado: {Nombre}", empleado.Nombres);
        }

        context.SaveChanges(); // Guardar para obtener el ID del empleado

        if (!context.Usuarios.Any())
        {
            var empleado = context.Empleados.FirstOrDefault(e => e.CedulaIdentidad == "9876543");
            if (empleado != null)
            {
                var encryptedPassword = AccountController.Encrypt("123456");
                logger.LogInformation("Contraseña encriptada generada para el usuario.");
                
                var usuario = new Usuario
                {
                    IdEmpleado = empleado.Id,
                    Usuario1 = "adolfo",
                    Clave = encryptedPassword,
                    UsuarioRegistro = "system",
                    FechaRegistro = DateTime.Now,
                    Estado = 1
                };
                context.Usuarios.Add(usuario);
                logger.LogInformation("Usuario inicial agregado: {Usuario}", usuario.Usuario1);
            }
            else
            {
                logger.LogError("No se encontró el empleado para crear el usuario.");
            }
        }

        context.SaveChanges();
        logger.LogInformation("Datos iniciales guardados correctamente.");
        
        // Verificar datos creados
        var empleadoCreado = context.Empleados.FirstOrDefault(e => e.CedulaIdentidad == "9876543");
        var usuarioCreado = context.Usuarios.FirstOrDefault(u => u.Usuario1 == "adolfo");
        
        if (empleadoCreado != null)
        {
            logger.LogInformation("Empleado verificado: {Nombre} con ID: {Id}", empleadoCreado.Nombres, empleadoCreado.Id);
        }
        
        if (usuarioCreado != null)
        {
            logger.LogInformation("Usuario verificado: {Usuario} con ID: {Id}", usuarioCreado.Usuario1, usuarioCreado.IdUsuario);
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error crítico al inicializar la base de datos: {Message}", ex.Message);
        throw;
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
