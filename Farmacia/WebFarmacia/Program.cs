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
        // Recrear la base de datos para asegurar que todos los datos estén correctos
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
                new Categoria { Nombre = "Vitaminas", Descripcion = "Suplementos vitamínicos", Estado = 1 },
                new Categoria { Nombre = "Antihistamínicos", Descripcion = "Medicamentos para alergias", Estado = 1 },
                new Categoria { Nombre = "Antidepresivos", Descripcion = "Medicamentos para depresión", Estado = 1 },
                new Categoria { Nombre = "Antidiabéticos", Descripcion = "Medicamentos para diabetes", Estado = 1 },
                new Categoria { Nombre = "Antihipertensivos", Descripcion = "Medicamentos para la hipertensión", Estado = 1 }
            );
            logger.LogInformation("Categorías iniciales agregadas.");
        }

        if (!context.Laboratorios.Any())
        {
            context.Laboratorios.AddRange(
                new Laboratorio { Nombre = "Bayer", Pais = "Alemania", Estado = 1 },
                new Laboratorio { Nombre = "Pfizer", Pais = "Estados Unidos", Estado = 1 },
                new Laboratorio { Nombre = "Roche", Pais = "Suiza", Estado = 1 },
                new Laboratorio { Nombre = "GSK", Pais = "Reino Unido", Estado = 1 },
                new Laboratorio { Nombre = "Abbott", Pais = "Estados Unidos", Estado = 1 },
                new Laboratorio { Nombre = "Novartis", Pais = "Suiza", Estado = 1 }
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

        // Seed medicamentos if not exists
        if (!context.Medicamentos.Any())
        {
            context.Medicamentos.AddRange(
                new Medicamento { IdCategoria = 6, IdLaboratorio = 2, Codigo = "MED005", Nombre = "Loratadina 10mg", Descripcion = "Antihistamínico para alergias", Composicion = "Loratadina", FechaVencimiento = DateTime.Parse("2025-10-10"), Stock = 120, PrecioVenta = 10.00m, RequiereReceta = false, UsuarioRegistro = "system", FechaRegistro = DateTime.Now, Estado = 1 },
                new Medicamento { IdCategoria = 7, IdLaboratorio = 3, Codigo = "MED006", Nombre = "Sertralina 50mg", Descripcion = "Antidepresivo", Composicion = "Sertralina", FechaVencimiento = DateTime.Parse("2026-01-15"), Stock = 80, PrecioVenta = 20.00m, RequiereReceta = true, UsuarioRegistro = "system", FechaRegistro = DateTime.Now, Estado = 1 },
                new Medicamento { IdCategoria = 8, IdLaboratorio = 5, Codigo = "MED007", Nombre = "Metformina 500mg", Descripcion = "Antidiabético", Composicion = "Metformina", FechaVencimiento = DateTime.Parse("2026-05-20"), Stock = 150, PrecioVenta = 18.50m, RequiereReceta = true, UsuarioRegistro = "system", FechaRegistro = DateTime.Now, Estado = 1 },
                new Medicamento { IdCategoria = 1, IdLaboratorio = 4, Codigo = "MED008", Nombre = "Ciprofloxacino 500mg", Descripcion = "Antibiótico fluoroquinolona", Composicion = "Ciprofloxacino", FechaVencimiento = DateTime.Parse("2025-11-30"), Stock = 90, PrecioVenta = 22.00m, RequiereReceta = true, UsuarioRegistro = "system", FechaRegistro = DateTime.Now, Estado = 1 },
                new Medicamento { IdCategoria = 2, IdLaboratorio = 1, Codigo = "MED009", Nombre = "Aspirina 100mg", Descripcion = "Analgésico y antiplaquetario", Composicion = "Ácido acetilsalicílico", FechaVencimiento = DateTime.Parse("2025-09-25"), Stock = 250, PrecioVenta = 5.50m, RequiereReceta = false, UsuarioRegistro = "system", FechaRegistro = DateTime.Now, Estado = 1 },
                new Medicamento { IdCategoria = 5, IdLaboratorio = 6, Codigo = "MED010", Nombre = "Vitamina D 1000 UI", Descripcion = "Suplemento de vitamina D", Composicion = "Colecalciferol", FechaVencimiento = DateTime.Parse("2026-07-10"), Stock = 200, PrecioVenta = 12.00m, RequiereReceta = false, UsuarioRegistro = "system", FechaRegistro = DateTime.Now, Estado = 1 },
                new Medicamento { IdCategoria = 9, IdLaboratorio = 1, Codigo = "MED011", Nombre = "Enalapril 10mg", Descripcion = "Antihipertensivo", Composicion = "Enalapril", FechaVencimiento = DateTime.Parse("2026-02-28"), Stock = 100, PrecioVenta = 15.00m, RequiereReceta = true, UsuarioRegistro = "system", FechaRegistro = DateTime.Now, Estado = 1 }
            );
            logger.LogInformation("Medicamentos iniciales agregados.");
        }

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
