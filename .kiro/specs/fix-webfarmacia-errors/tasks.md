# Implementation Plan

- [x] 1. Limpiar archivos residuales de WebHamburgueseria





  - Eliminar archivos de configuración que no pertenecen al proyecto WebFarmacia
  - Verificar que no queden referencias a WebHamburgueseria en el proyecto
  - _Requirements: 2.1, 2.2, 2.3_

- [x] 2. Actualizar modelo Medicamento





  - [x] 2.1 Renombrar clase Producto a Medicamento en Models/Medicamento.cs


    - Cambiar el nombre de la clase de `Producto` a `Medicamento`
    - Actualizar la propiedad `IdProducto` a `Id`
    - Verificar que todas las propiedades coincidan con las columnas de la BD
    - _Requirements: 3.2, 3.3, 3.5_

  - [x] 2.2 Actualizar modelo Categoria en Models/Categoria.cs


    - Cambiar propiedad `IdCategoria` a `Id`
    - Agregar propiedad `Nombre` (string, requerido, max 50 caracteres)
    - Hacer propiedad `Descripcion` nullable
    - Actualizar la colección de navegación a `Medicamentos`
    - _Requirements: 3.3, 3.5_

  - [x] 2.3 Actualizar modelo Laboratorio en Models/Laboratorio.cs


    - Verificar que use `Id` como propiedad de clave primaria
    - Actualizar la colección de navegación a `Medicamentos`
    - _Requirements: 3.5_

  - [x] 2.4 Actualizar modelo VentaDetalle en Models/DetalleVenta.cs


    - Cambiar propiedad de navegación `IdProductoNavigation` a `IdMedicamentoNavigation`
    - Verificar que `IdMedicamento` sea la FK correcta
    - _Requirements: 3.5, 4.2_

- [x] 3. Actualizar FarmaciaContext





  - [x] 3.1 Actualizar DbSet de Medicamentos


    - Cambiar `DbSet<Producto> Medicamentos` a `DbSet<Medicamento> Medicamentos`
    - _Requirements: 3.2, 4.1, 4.2_

  - [x] 3.2 Reconfigurar mapeo de Medicamento en OnModelCreating


    - Actualizar configuración para usar `Medicamento` en lugar de `Producto`
    - Cambiar mapeo de `IdProducto` a `Id`
    - Asegurar que todos los nombres de columnas coincidan con la BD
    - Configurar tipo de dato correcto para `FechaVencimiento` (date)
    - Configurar tipo de dato correcto para `FechaRegistro` (datetime)
    - _Requirements: 3.3, 3.5, 4.1, 4.2_

  - [x] 3.3 Reconfigurar mapeo de Categoria en OnModelCreating


    - Cambiar mapeo de `IdCategoria` a `Id`
    - Agregar mapeo para columna `nombre`
    - Configurar `Nombre` como requerido con max length 50
    - Configurar `Descripcion` como nullable con max length 250
    - _Requirements: 3.3, 3.5, 4.1, 4.2_

  - [x] 3.4 Actualizar mapeo de VentaDetalle en OnModelCreating


    - Cambiar configuración de FK para usar `IdMedicamentoNavigation`
    - Verificar que el constraint name sea correcto: "fk_DetalleVenta_Medicamento"
    - _Requirements: 3.5, 4.1, 4.2_

  - [x] 3.5 Verificar y actualizar mapeos de otras entidades


    - Revisar Cliente, Empleado, Usuario, Venta
    - Asegurar que todas usen `Id` para PK y nombres correctos para FKs
    - Verificar tipos de datos (datetime para fechas, etc.)
    - _Requirements: 4.1, 4.2_

- [x] 4. Actualizar MedicamentosController





  - [x] 4.1 Cambiar todas las referencias de Producto a Medicamento


    - Actualizar tipo de parámetros en métodos
    - Actualizar variables locales
    - Actualizar comentarios
    - _Requirements: 3.2, 3.4, 5.2_

  - [x] 4.2 Actualizar propiedades en métodos CRUD

    - Cambiar `IdProducto` a `Id` en Details, Edit, Delete
    - Actualizar Bind attributes en Create y Edit
    - Actualizar método `ProductoExists` a `MedicamentoExists`
    - _Requirements: 3.2, 3.4, 5.2_


  - [x] 4.3 Actualizar SelectList para Categorias




    - Cambiar tercer parámetro de "Descripcion" a "Nombre"
    - Aplicar en métodos Create y Edit
    - _Requirements: 3.3, 5.2_

- [x] 5. Actualizar vistas de Medicamentos




  - [x] 5.1 Actualizar directivas @model en todas las vistas


    - Index.cshtml: `@model IEnumerable<WebFarmacia.Models.Medicamento>`
    - Create.cshtml: `@model WebFarmacia.Models.Medicamento`
    - Edit.cshtml: `@model WebFarmacia.Models.Medicamento`
    - Details.cshtml: `@model WebFarmacia.Models.Medicamento`
    - Delete.cshtml: `@model WebFarmacia.Models.Medicamento`
    - _Requirements: 3.4, 5.3_

  - [x] 5.2 Actualizar referencias de propiedades en vistas


    - Cambiar `model.IdProducto` a `model.Id` en todas las vistas
    - Verificar que todas las propiedades coincidan con el modelo actualizado
    - _Requirements: 3.4, 5.3_

- [x] 6. Actualizar CategoriasController y vistas





  - [x] 6.1 Actualizar CategoriasController


    - Actualizar Bind attributes para incluir "Nombre"
    - Actualizar validaciones si es necesario
    - Asegurar que "Nombre" sea requerido en Create/Edit
    - _Requirements: 3.3, 5.2_

  - [x] 6.2 Actualizar vistas de Categorias


    - Agregar campo "Nombre" en Create.cshtml
    - Agregar campo "Nombre" en Edit.cshtml
    - Mostrar "Nombre" en Index.cshtml, Details.cshtml, Delete.cshtml
    - Actualizar labels y validaciones
    - _Requirements: 3.3, 5.3_

- [x] 7. Actualizar controladores relacionados con Medicamentos





  - [x] 7.1 Actualizar VentasController


    - Verificar referencias a Medicamento en lugar de Producto
    - Actualizar ViewModels si es necesario
    - _Requirements: 3.4, 5.2_

  - [x] 7.2 Actualizar DetalleVentasController


    - Cambiar referencias de Producto a Medicamento
    - Actualizar SelectList para Medicamentos
    - Actualizar propiedades de navegación
    - _Requirements: 3.4, 5.2_

  - [x] 7.3 Revisar y eliminar ProductosController si existe


    - Verificar si existe un controlador separado para Productos
    - Eliminarlo si es redundante con MedicamentosController
    - _Requirements: 2.3, 3.2_

- [x] 8. Verificar y probar la aplicación










  - [x] 8.1 Compilar el proyecto

    - Ejecutar `dotnet build` para verificar que no hay errores de compilación
    - Resolver cualquier error de tipos o referencias
    - _Requirements: 1.3, 5.1, 5.2_


  - [x] 8.2 Verificar conexión a base de datos

    - Confirmar que la BD FinalFarmacia existe
    - Verificar que el connection string en appsettings.json es correcto
    - Probar la conexión al iniciar la aplicación
    - _Requirements: 5.4_


  - [x] 8.3 Probar funcionalidad de Medicamentos



    - Listar medicamentos (Index)
    - Ver detalles de un medicamento (Details)
    - Crear un nuevo medicamento (Create)
    - Editar un medicamento existente (Edit)
    - Eliminar un medicamento (Delete - soft delete)
    - _Requirements: 5.1, 5.2, 5.3_

  - [x] 8.4 Probar funcionalidad de Categorías


    - Listar categorías con campo Nombre visible
    - Crear una nueva categoría con Nombre y Descripción
    - Editar una categoría existente
    - Verificar que el campo Nombre es requerido
    - _Requirements: 5.1, 5.2, 5.3_

  - [x] 8.5 Probar autenticación


    - Hacer login con usuario "adolfo" / "123456"
    - Verificar que la sesión se mantiene
    - Verificar que las páginas protegidas requieren autenticación
    - Hacer logout correctamente
    - _Requirements: 5.1, 5.2_

  - [x] 8.6 Probar navegación general


    - Verificar que todos los enlaces del menú funcionan
    - Verificar que no hay errores 404
    - Verificar que las vistas se renderizan correctamente
    - _Requirements: 5.1, 5.2, 5.3_
