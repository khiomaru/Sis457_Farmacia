# TODO: Adaptar WebHamburgueseria a WebFarmacia

## Paso 1: Renombrar carpeta del proyecto
- [x] Renombrar "Farmacia/WebHamburgueseria" a "Farmacia/WebFarmacia"

## Paso 2: Renombrar archivo .csproj
- [x] Renombrar "WebHamburgueseria.csproj" a "WebFarmacia.csproj"

## Paso 3: Actualizar espacios de nombres en archivos C#
- [x] Actualizar namespaces de "WebHamburgueseria" a "WebFarmacia" en Program.cs
- [ ] Actualizar namespaces en controladores y modelos restantes

## Paso 4: Renombrar DbContext
- [x] Cambiar clase "LabHamburgueseriaContext" a "FarmaciaContext" en FarmaciaContext.cs
- [ ] Actualizar referencias en controladores y otros archivos

## Paso 5: Actualizar cadena de conexión
- [ ] Cambiar connection string en appsettings.json de "LabHamburgueseria" a "FinalFarmacia"
- [ ] Actualizar connection string en FarmaciaContext.cs

## Paso 6: Renombrar modelos
- [ ] Categorium.cs → Categoria.cs
- [ ] Producto.cs → Medicamento.cs
- [ ] Ventum.cs → Venta.cs
- [ ] VentaDetalle.cs → DetalleVenta.cs
- [ ] Actualizar propiedades y relaciones en los modelos

## Paso 7: Agregar modelo Laboratorio
- [ ] Crear Laboratorio.cs basado en la app de escritorio

## Paso 8: Actualizar controladores
- [ ] ProductosController.cs → MedicamentosController.cs
- [ ] Actualizar referencias a modelos en todos los controladores

## Paso 9: Actualizar vistas
- [ ] Cambiar terminología de hamburguesería a farmacia en vistas .cshtml
- [ ] Actualizar rutas y enlaces

## Paso 10: Asegurar CRUD con eliminación lógica
- [ ] Verificar que Medicamento y Cliente usen eliminación lógica (estado = 0)

## Paso 11: Verificar funcionalidad de login
- [ ] Revisar AccountController y vistas de login

## Paso 12: Mejorar funcionalidad de ventas
- [ ] Actualizar VentasController para mayor valor en calificación

## Paso 13: Construir y probar
- [ ] Construir el proyecto y resolver errores
- [ ] Probar funcionalidades CRUD, login y ventas
