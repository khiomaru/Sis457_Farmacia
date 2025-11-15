# TODO: Corrección de Errores en CpFarmacia2024

## Información Recopilada
- Analizados archivos principales: Program.cs, FrmLogin.cs, FrmPrincipal.cs, Util.cs, FrmCategoria.cs, FrmMedicamento.cs, FrmCompra.cs, FrmVenta.cs, FrmPequeProducto.cs, FrmPequeProveedor.cs, FrmProveedor.cs, FrmPaciente.cs.
- Issues identificados:
  - Errores de escritura: "visebersa" -> "viceversa", "evalua" -> "evalúa", etc.
  - Lógica errónea: Validación de precios en compras/ventas invertida.
  - Duplicación: FrmVenta.cs es copia de FrmCompra.cs sin adaptación.
  - Validaciones incorrectas: nudCantidad.Value < 0 en lugar de <=0.
  - Nombres inconsistentes: Mensajes y variables con errores.
  - Lógica de stock: En ventas, debe restar stock, no sumar.

## Plan de Corrección
1. Corregir errores de escritura en todos los archivos (comentarios, mensajes, etc.).
2. Corregir lógica de validación de precios en FrmCompra.cs y FrmVenta.cs.
3. Adaptar FrmVenta.cs para lógica de ventas: restar stock, ajustar precios y validaciones.
4. Corregir validaciones de cantidad (nudCantidad.Value <= 0).
5. Asegurar consistencia en nombres y mensajes.
6. Verificar y corregir cualquier otro error lógico identificado.

## Archivos a Editar
- CpFarmacia2024/FrmCategoria.cs
- CpFarmacia2024/FrmCompra.cs
- CpFarmacia2024/FrmVenta.cs
- CpFarmacia2024/FrmProveedor.cs
- CpFarmacia2024/FrmPequeProducto.cs
- CpFarmacia2024/FrmPequeProveedor.cs

## Pasos de Ejecución
1. Editar FrmCategoria.cs: Corregir errores de escritura.
2. Editar FrmCompra.cs: Corregir validaciones y lógica.
3. Editar FrmVenta.cs: Adaptar para ventas, corregir lógica.
4. Editar FrmProveedor.cs: Corregir errores de escritura.
5. Editar FrmPequeProducto.cs: Corregir errores de escritura.
6. Editar FrmPequeProveedor.cs: Corregir errores de escritura.
7. Verificar cambios y probar lógica.

## Seguimiento de Progreso
- [x] Paso 1 completado
- [x] Paso 2 completado
- [x] Paso 3 completado
- [x] Paso 4 completado
- [x] Paso 5 completado
- [x] Paso 6 completado
- [x] Verificación final completada
