# Design Document - WebFarmacia Error Fixes

## Overview

Este documento describe el diseño de las correcciones necesarias para que WebFarmacia funcione correctamente. El proyecto es una aplicación ASP.NET Core MVC que gestiona una farmacia, incluyendo medicamentos, ventas, clientes, empleados y usuarios.

### Problemas Identificados

1. **Archivos residuales**: Existen archivos de configuración de "WebHamburgueseria" que causan confusión
2. **Inconsistencias en el modelo de datos**: El código usa "Producto" pero la BD usa "Medicamento"
3. **Mapeo incorrecto de columnas**: Falta el campo "nombre" en Categoria, entre otros
4. **Relaciones de Entity Framework**: Algunas navegaciones no coinciden con la BD
5. **.NET 10.0**: Verificar que el SDK esté correctamente instalado

## Architecture

### Arquitectura Actual
```
WebFarmacia (ASP.NET Core MVC)
├── Controllers (MVC Controllers)
├── Models (Entity Framework Core Models)
├── Views (Razor Views)
└── wwwroot (Static files)
```

### Patrón de Diseño
- **MVC (Model-View-Controller)**: Separación de responsabilidades
- **Repository Pattern**: A través de DbContext de Entity Framework
- **Cookie Authentication**: Para manejo de sesiones de usuario

## Components and Interfaces

### 1. Corrección de Archivos Residuales

**Archivos a eliminar:**
- `Farmacia/WebFarmacia/WebHamburgueseria.csproj.user`
- `Farmacia/WebFarmacia/obj/WebHamburgueseria.csproj.nuget.dgspec.json`
- `Farmacia/WebFarmacia/obj/WebHamburgueseria.csproj.nuget.g.props`
- `Farmacia/WebFarmacia/obj/WebHamburgueseria.csproj.nuget.g.targets`

### 2. Corrección del Modelo de Datos

#### 2.1 Renombrar Producto a Medicamento

**Archivo actual:** `Models/Medicamento.cs`
```csharp
// CAMBIAR: public partial class Producto
// POR:
public partial class Medicamento
{
    public int Id { get; set; }  // Cambiar de IdProducto a Id
    // ... resto de propiedades
}
```

**Razón:** La tabla en la BD se llama "Medicamento" y la columna PK es "id", no "IdProducto"

#### 2.2 Actualizar Modelo Categoria

**Archivo:** `Models/Categoria.cs`
```csharp
public partial class Categoria
{
    public int Id { get; set; }  // Cambiar de IdCategoria a Id
    public string Nombre { get; set; } = null!;  // AGREGAR este campo
    public string? Descripcion { get; set; }  // Hacer nullable
    // ... resto de propiedades
    
    public virtual ICollection<Medicamento> Medicamentos { get; set; } = [];
}
```

**Razón:** La BD tiene columnas "id", "nombre" y "descripcion"

#### 2.3 Actualizar Modelo Laboratorio

**Archivo:** `Models/Laboratorio.cs`
- Ya está correcto, solo verificar que use `Id` en lugar de otras variantes

#### 2.4 Actualizar Otros Modelos

**Cliente, Empleado, Usuario, Venta, VentaDetalle:**
- Cambiar todas las propiedades de ID para usar nombres consistentes con la BD
- Ejemplo: `IdUsuario` → `Id` para la PK, mantener `IdUsuario` para FKs

### 3. Corrección de FarmaciaContext

**Archivo:** `Models/FarmaciaContext.cs`

#### 3.1 Actualizar DbSets
```csharp
public virtual DbSet<Categoria> Categorias { get; set; }
public virtual DbSet<Cliente> Clientes { get; set; }
public virtual DbSet<Empleado> Empleados { get; set; }
public virtual DbSet<Laboratorio> Laboratorios { get; set; }
public virtual DbSet<Medicamento> Medicamentos { get; set; }  // Cambiar de Producto
public virtual DbSet<Usuario> Usuarios { get; set; }
public virtual DbSet<Venta> Ventas { get; set; }
public virtual DbSet<VentaDetalle> VentaDetalles { get; set; }
```

#### 3.2 Configuración de Medicamento
```csharp
modelBuilder.Entity<Medicamento>(entity =>
{
    entity.ToTable("Medicamento");
    entity.HasKey(e => e.Id);
    
    entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
    entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
    entity.Property(e => e.IdLaboratorio).HasColumnName("idLaboratorio");
    entity.Property(e => e.Codigo).HasColumnName("codigo").HasMaxLength(20);
    entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(100);
    entity.Property(e => e.Descripcion).HasColumnName("descripcion").HasMaxLength(250);
    entity.Property(e => e.Composicion).HasColumnName("composicion").HasMaxLength(250);
    entity.Property(e => e.FechaVencimiento).HasColumnName("fechaVencimiento").HasColumnType("date");
    entity.Property(e => e.Stock).HasColumnName("stock");
    entity.Property(e => e.PrecioVenta).HasColumnName("precioVenta").HasColumnType("decimal(10,2)");
    entity.Property(e => e.RequiereReceta).HasColumnName("requiereReceta");
    entity.Property(e => e.UsuarioRegistro).HasColumnName("usuarioRegistro").HasMaxLength(50);
    entity.Property(e => e.FechaRegistro).HasColumnName("fechaRegistro").HasColumnType("datetime");
    entity.Property(e => e.Estado).HasColumnName("estado");
    
    entity.HasOne(d => d.IdCategoriaNavigation)
        .WithMany(p => p.Medicamentos)
        .HasForeignKey(d => d.IdCategoria)
        .HasConstraintName("FK_Medicamento_Categoria");
        
    entity.HasOne(d => d.IdLaboratorioNavigation)
        .WithMany(p => p.Medicamentos)
        .HasForeignKey(d => d.IdLaboratorio)
        .HasConstraintName("FK_Medicamento_Laboratorio");
});
```

#### 3.3 Configuración de Categoria
```csharp
modelBuilder.Entity<Categoria>(entity =>
{
    entity.ToTable("Categoria");
    entity.HasKey(e => e.Id);
    
    entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
    entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(50).IsRequired();
    entity.Property(e => e.Descripcion).HasColumnName("descripcion").HasMaxLength(250);
    entity.Property(e => e.UsuarioRegistro).HasColumnName("usuarioRegistro").HasMaxLength(50);
    entity.Property(e => e.FechaRegistro).HasColumnName("fechaRegistro").HasColumnType("datetime");
    entity.Property(e => e.Estado).HasColumnName("estado");
});
```

#### 3.4 Configuración de VentaDetalle
```csharp
entity.HasOne(d => d.IdMedicamentoNavigation)  // Cambiar nombre de propiedad
    .WithMany(p => p.VentaDetalles)
    .HasForeignKey(d => d.IdMedicamento)
    .HasConstraintName("fk_DetalleVenta_Medicamento");
```

### 4. Actualización de Controladores

#### 4.1 MedicamentosController

**Cambios necesarios:**
1. Cambiar todas las referencias de `Producto` a `Medicamento`
2. Actualizar propiedades: `IdProducto` → `Id`
3. Actualizar el Bind en Create/Edit para usar las propiedades correctas

```csharp
// Ejemplo en Index:
public async Task<IActionResult> Index()
{
    var medicamentos = _context.Medicamentos
        .Where(x => x.Estado != -1)
        .Include(m => m.IdCategoriaNavigation)
        .Include(m => m.IdLaboratorioNavigation);
    return View(await medicamentos.ToListAsync());
}

// Ejemplo en Create:
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
    // ...
}
```

#### 4.2 CategoriasController

**Actualizar para usar el campo "Nombre":**
```csharp
ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Nombre");
```

#### 4.3 Otros Controladores

Revisar y actualizar:
- `ProductosController` (si existe, renombrar o eliminar)
- `VentasController` (actualizar referencias a Medicamento)
- `DetalleVentasController` (actualizar referencias a Medicamento)

### 5. Actualización de Vistas

#### 5.1 Vistas de Medicamentos

**Directorio:** `Views/Medicamentos/`

Actualizar todas las vistas para usar `Medicamento` en lugar de `Producto`:
- `Index.cshtml`: `@model IEnumerable<WebFarmacia.Models.Medicamento>`
- `Create.cshtml`: `@model WebFarmacia.Models.Medicamento`
- `Edit.cshtml`: `@model WebFarmacia.Models.Medicamento`
- `Details.cshtml`: `@model WebFarmacia.Models.Medicamento`
- `Delete.cshtml`: `@model WebFarmacia.Models.Medicamento`

Actualizar referencias de propiedades:
- `@Html.DisplayFor(model => model.IdProducto)` → `@Html.DisplayFor(model => model.Id)`

#### 5.2 Vistas de Categorías

Actualizar para mostrar el campo "Nombre":
```html
<dt>@Html.DisplayNameFor(model => model.Nombre)</dt>
<dd>@Html.DisplayFor(model => model.Nombre)</dd>
```

#### 5.3 Layout y Navegación

Verificar que los enlaces en `_Layout.cshtml` apunten correctamente a "Medicamentos"

## Data Models

### Diagrama de Relaciones

```
Laboratorio (1) ──────< (N) Medicamento (N) >────── (1) Categoria
                              │
                              │ (1)
                              │
                              ▼
                            (N) DetalleVenta
                              │
                              │ (N)
                              │
                              ▼
                            (1) Venta
                              │
                              ├─< (1) Usuario ──< (1) Empleado
                              │
                              └─< (1) Cliente
```

### Mapeo de Columnas

| Modelo | Propiedad C# | Columna BD | Tipo BD |
|--------|-------------|------------|---------|
| Medicamento | Id | id | INT |
| Medicamento | IdCategoria | idCategoria | INT |
| Medicamento | IdLaboratorio | idLaboratorio | INT |
| Medicamento | Codigo | codigo | VARCHAR(20) |
| Medicamento | Nombre | nombre | VARCHAR(100) |
| Medicamento | FechaVencimiento | fechaVencimiento | DATE |
| Categoria | Id | id | INT |
| Categoria | Nombre | nombre | VARCHAR(50) |
| Categoria | Descripcion | descripcion | VARCHAR(250) |

## Error Handling

### Estrategia de Manejo de Errores

1. **Validación de Modelo**: Usar Data Annotations en los modelos
2. **Try-Catch en Controladores**: Capturar DbUpdateException y otras excepciones
3. **Mensajes Amigables**: Mostrar mensajes claros al usuario
4. **Logging**: Implementar ILogger para registrar errores

```csharp
try
{
    await _context.SaveChangesAsync();
}
catch (DbUpdateException ex)
{
    _logger.LogError(ex, "Error al guardar medicamento");
    ModelState.AddModelError("", "Error al guardar el medicamento. Por favor, intente nuevamente.");
}
```

## Testing Strategy

### Verificación Manual

1. **Compilación**: Verificar que el proyecto compile sin errores
2. **Inicio de Aplicación**: Verificar que la aplicación inicie correctamente
3. **Conexión a BD**: Verificar que se conecte a FinalFarmacia
4. **CRUD de Medicamentos**: 
   - Crear un medicamento
   - Listar medicamentos
   - Editar un medicamento
   - Eliminar (soft delete) un medicamento
5. **Autenticación**: Verificar login con usuario "adolfo" / "123456"
6. **Navegación**: Verificar que todos los enlaces funcionen

### Puntos de Verificación

- [ ] Proyecto compila sin errores
- [ ] No hay archivos de WebHamburgueseria
- [ ] Aplicación inicia sin excepciones
- [ ] Se conecta a la BD FinalFarmacia
- [ ] CRUD de Medicamentos funciona
- [ ] CRUD de Categorías funciona (con campo Nombre)
- [ ] Login funciona correctamente
- [ ] Navegación entre páginas funciona

## Security Improvements

### Mejoras Implementadas

1. **Hash de Contraseñas**: Ya implementado con AES en AccountController
2. **Anti-Forgery Tokens**: Ya implementado en formularios
3. **Authorization**: Ya implementado con [Authorize] attribute
4. **SQL Injection Prevention**: Entity Framework previene esto automáticamente

### Mejoras Recomendadas (Futuras)

1. **HTTPS Enforcement**: Ya configurado en Program.cs
2. **Password Policy**: Implementar requisitos de complejidad
3. **Session Timeout**: Configurar timeout apropiado (actualmente 15 min)
4. **Input Validation**: Agregar más validaciones en modelos
5. **Error Messages**: No revelar información sensible en mensajes de error

## Configuration

### Connection String

**Archivo:** `appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=FinalFarmacia;Integrated Security=true;TrustServerCertificate=true"
  }
}
```

**Nota:** Verificar que la BD "FinalFarmacia" exista y tenga los datos del script DDL_DML_FARMACIA.sql

### .NET SDK Verification

Verificar instalación de .NET 10.0:
```bash
dotnet --list-sdks
```

Si no aparece .NET 10.0, considerar:
1. Descargar e instalar .NET 10.0 Preview desde Microsoft
2. O cambiar el proyecto a .NET 8.0 (LTS) si .NET 10.0 no está disponible

## Implementation Notes

### Orden de Implementación

1. Eliminar archivos residuales de WebHamburgueseria
2. Actualizar modelos (Medicamento, Categoria, etc.)
3. Actualizar FarmaciaContext con configuraciones correctas
4. Actualizar MedicamentosController
5. Actualizar vistas de Medicamentos
6. Actualizar CategoriasController y vistas
7. Verificar otros controladores (Ventas, DetalleVentas)
8. Probar la aplicación completa

### Consideraciones Especiales

- **Backward Compatibility**: No hay datos en producción, por lo que podemos hacer cambios breaking
- **Database First**: El esquema de BD ya existe, debemos adaptarnos a él
- **Naming Conventions**: Seguir las convenciones de la BD (camelCase en columnas)
