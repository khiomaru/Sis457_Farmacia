# Implementation Plan

- [] 1. Migrar assets estáticos del template pharma-master a wwwroot





  - Copiar todos los archivos CSS de pharma-master/css a Farmacia/WebFarmacia/wwwroot/css
  - Copiar todos los archivos JavaScript de pharma-master/js a Farmacia/WebFarmacia/wwwroot/js
  - Copiar la carpeta de fuentes pharma-master/fonts a Farmacia/WebFarmacia/wwwroot/fonts
  - Copiar las imágenes de pharma-master/images a Farmacia/WebFarmacia/wwwroot/images
  - Verificar que todos los archivos se copiaron correctamente
  - _Requirements: 1.4_

- [] 2. Actualizar _Layout.cshtml con estructura pharma-master





  - Reemplazar el contenido del head con las referencias a CSS de pharma-master
  - Implementar la estructura de navegación pharma-master dentro del body
  - Crear navegación condicional que muestre menú público para usuarios no autenticados
  - Crear navegación condicional que muestre menú administrativo para usuarios autenticados
  - Implementar la sección de user options con saludo al usuario autenticado y botón de logout
  - Reemplazar el footer actual con el footer de pharma-master
  - Agregar referencias a JavaScript de pharma-master antes del cierre de body
  - _Requirements: 1.1, 1.2, 1.3, 3.1, 3.2, 3.3, 3.4, 3.5_

- [ ] 3. Crear vista de catálogo público en Home/Index.cshtml








  - Implementar hero section con imagen de fondo y call-to-action
  - Crear sección de productos destacados con título
  - Implementar grid de productos usando Bootstrap responsive
  - Crear product cards que muestren imagen del medicamento, nombre y precio
  - Agregar enlaces en cada card que dirijan a la vista Details del medicamento
  - _Requirements: 2.1, 2.2, 2.3, 2.4, 2.5, 6.1, 6.2, 6.3, 6.4_

- [ ] 4. Actualizar HomeController para soportar catálogo público
  - Modificar el método Index para cargar medicamentos con stock mayor a 0
  - Incluir la relación con Categoria usando Include
  - Limitar los resultados a 6 medicamentos destacados
  - Pasar la lista de medicamentos a la vista
  - _Requirements: 6.1, 6.2, 6.3, 6.4_

- [ ] 5. Actualizar Medicamentos/Index.cshtml para administradores
  - Implementar estructura site-section con container de pharma-master
  - Crear encabezado con título y botón Nuevo Medicamento
  - Implementar tabla Bootstrap con columnas: Nombre, Precio, Stock, Categoría, Acciones
  - Agregar botones de acción con estilos pharma-master
  - _Requirements: 4.1, 4.2, 4.3, 4.4_

- [ ] 6. Crear vista Medicamentos/Details.cshtml estilo producto
  - Implementar breadcrumb navigation
  - Crear layout de dos columnas: imagen e información
  - Mostrar especificaciones del medicamento
  - Agregar botones de acción solo para usuarios autenticados
  - _Requirements: 2.5, 4.1, 4.4, 6.2, 6.3_

- [ ] 7. Actualizar formularios Create y Edit con estilos pharma-master
  - Actualizar Medicamentos/Create.cshtml con estructura pharma-master
  - Aplicar clases form-group y form-control a todos los campos
  - Implementar validación con estilos pharma-master
  - Replicar cambios en Medicamentos/Edit.cshtml
  - Aplicar el mismo patrón a formularios de Clientes, Empleados y Categorías
  - _Requirements: 4.2, 4.3, 4.4, 4.5_

- [ ] 8. Crear interfaz de carrito para Ventas/Create.cshtml
  - Implementar sección de selección de cliente
  - Crear sección Agregar Medicamentos con dropdown y cantidad
  - Implementar tabla de carrito con columnas apropiadas
  - Crear sección de total con diseño de dos columnas
  - Agregar botones Completar Venta y Cancelar
  - _Requirements: 5.1, 5.2, 5.3, 5.4, 5.5_

- [ ] 9. Implementar JavaScript para funcionalidad de carrito dinámico
  - Crear sección Scripts en Ventas/Create.cshtml
  - Implementar array cartItems para almacenar items
  - Crear función para agregar items al carrito
  - Implementar función para eliminar items
  - Crear función updateTotal para calcular total dinámicamente
  - Agregar validación para items duplicados o cantidad inválida
  - _Requirements: 5.2, 5.3, 5.4_

- [ ] 10. Actualizar VentasController para soportar interfaz de carrito
  - Modificar método GET Create para cargar ViewBag con clientes y medicamentos
  - Modificar método POST Create para recibir datos del carrito
  - Implementar lógica para crear Venta y DetalleVenta
  - Validar stock suficiente antes de guardar
  - Actualizar stock después de completar venta
  - _Requirements: 5.1, 5.2, 5.5, 6.1, 6.2_

- [ ] 11. Actualizar vistas de Clientes con estilos pharma-master
  - Actualizar Clientes/Index.cshtml con tabla estilizada
  - Actualizar Clientes/Create.cshtml y Edit.cshtml con formularios estilizados
  - Mantener funcionalidad CRUD existente
  - _Requirements: 4.1, 4.2, 4.3, 4.4_

- [ ] 12. Actualizar vistas de Empleados con estilos pharma-master
  - Actualizar Empleados/Index.cshtml con tabla estilizada
  - Actualizar Empleados/Create.cshtml y Edit.cshtml con formularios estilizados
  - Mantener funcionalidad CRUD existente
  - _Requirements: 4.1, 4.2, 4.3, 4.4_

- [ ] 13. Actualizar vistas de Categorías con estilos pharma-master
  - Actualizar Categorias/Index.cshtml con tabla estilizada
  - Actualizar Categorias/Create.cshtml y Edit.cshtml con formularios estilizados
  - Mantener funcionalidad CRUD existente
  - _Requirements: 4.1, 4.2, 4.3, 4.4_

- [ ] 14. Actualizar vista de login Account/Login.cshtml
  - Implementar diseño centrado con pharma-master
  - Crear formulario estilizado
  - Estilizar botón de login
  - Mantener funcionalidad de autenticación existente
  - _Requirements: 3.5, 7.2_

- [ ] 15. Crear vista de error 404 personalizada
  - Crear Views/Shared/NotFound.cshtml con diseño pharma-master
  - Implementar mensaje 404 centrado
  - Agregar botón Volver al Inicio
  - Configurar middleware en Program.cs
  - _Requirements: 4.5_

- [ ] 16. Implementar manejo de errores de validación
  - Crear partial view para mensajes de error
  - Aplicar estilos pharma-master a alertas
  - Asegurar que todos los formularios muestren errores correctamente
  - _Requirements: 4.5_

- [ ] 17. Optimizar imágenes y assets para producción
  - Crear carpeta wwwroot/images/products
  - Agregar imagen placeholder default.png
  - Verificar rutas de imágenes en las vistas
  - _Requirements: 1.4, 2.4, 6.3_

- [ ] 18. Realizar pruebas de navegación y responsive design
  - Probar navegación entre todas las páginas
  - Verificar que el menú cambia correctamente según autenticación
  - Probar en diferentes tamaños de pantalla
  - Verificar menú hamburguesa en móvil
  - _Requirements: 1.5, 3.1, 3.2, 3.3, 3.4, 3.5_

- [ ] 19. Realizar pruebas de funcionalidad CRUD
  - Probar crear, editar y eliminar en todas las entidades
  - Verificar validaciones
  - Verificar mensajes de error con estilo correcto
  - _Requirements: 4.1, 4.2, 4.3, 4.5, 7.1, 7.3, 7.4, 7.5_

- [ ] 20. Realizar pruebas de funcionalidad de ventas
  - Probar agregar items al carrito
  - Probar modificar cantidades
  - Probar eliminar items
  - Verificar cálculo de total
  - Probar completar venta y verificar guardado
  - Verificar actualización de stock
  - _Requirements: 5.1, 5.2, 5.3, 5.4, 5.5, 6.1, 6.2, 6.5_

- [ ] 21. Realizar pruebas de autenticación
  - Probar login con credenciales válidas e inválidas
  - Probar logout
  - Verificar redirección a login en páginas protegidas
  - Verificar acceso a páginas públicas sin autenticación
  - _Requirements: 3.1, 3.2, 3.3, 3.4, 3.5, 7.2, 7.5_

- [ ]* 22. Documentar cambios y crear guía de uso
  - Actualizar README.md con información sobre la integración
  - Documentar estructura de carpetas de assets
  - Crear guía de uso para el catálogo público
  - Documentar proceso de creación de ventas
  - _Requirements: 7.4_
