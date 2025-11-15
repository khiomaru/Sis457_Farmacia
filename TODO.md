✔️ FASE 1 — INICIO DEL PROYECTO
1. Creación de la solución

 Crear solución Sis457_Farmacia en Visual Studio Community.

 Configurar estructura en 3 capas:

CadFarmacia → Capa de Datos

ClnFarmacia → Capa de Lógica

CpFarmacia2024 → Capa de Presentación (WinForms)

 Configurar enlace entre proyectos (referencias).

2. Creación del repositorio en GitHub

 Crear repositorio público Sis457_Farmacia.

 Subir estructura inicial del proyecto.

 Configurar .gitignore para C#, bin/, obj/, .vs/

 Realizar primeros commits con mensaje claro.

✔️ FASE 2 — BASE DE DATOS
3. Diseño de la base de datos

 Crear base de datos Farmacia en SQL Server.

 Crear tablas iniciales:

Paciente

Medicamento

Usuario

Rol

Venta

VentaDetalle

 Definir claves primarias y foráneas.

 Crear procedimientos almacenados:

paPacienteListar

paPacienteInsertar

paPacienteEditar

paPacienteEliminar

paMedicamentoListar

...etc.

4. Pruebas desde SQL

 Insertar datos de prueba.

 Ejecutar selects de verificación.

 Validar integridad de claves foráneas.

✔️ FASE 3 — CAPA DE DATOS (CadFarmacia)
5. Clases de acceso a datos

 Crear clase ClsPaciente

 Crear clase ClsMedicamento

 Crear clase ClsUsuario

 Implementar métodos:

Listar()

Insertar()

Editar()

Eliminar()

6. Configuración de conexión

 Crear archivo app.config

 Agregar cadena de conexión SQL Server

 Probar conexión con try/catch.

✔️ FASE 4 — CAPA DE LÓGICA (ClnFarmacia)
7. Clases de lógica de negocio

 ClnPaciente

 ClnMedicamento

 ClnUsuario

8. Reglas de negocio

 Validar campos obligatorios

 Evitar duplicados

 Manejar excepciones con mensajes claros

 Preparación de datos antes de enviarlos a CadFarmacia

✔️ FASE 5 — CAPA DE PRESENTACIÓN (CpFarmacia2024)
9. Pantalla de Login

 Crear formulario FrmLogin

 Agregar controles:

Usuario

Contraseña

Botón Ingresar

 Validación contra SQL Server

 Manejar sesiones básicas.

✔️ FASE 6 — MÓDULO PACIENTE
10. Crear interfaz gráfica

 Crear formulario FrmPaciente

 Agregar controles:

TextBox para Nombre, CI, Dirección, etc.

DataGridView

Botones: Nuevo, Guardar, Editar, Eliminar, Cancelar, Cerrar

11. Conectar frontend con backend

 Evento Load → cargar listado

 Botón Guardar → Insertar / Editar

 Botón Eliminar → eliminación lógica

 Botón Editar → rellenar campos

 Rediseñar DataGridView

 Validaciones visuales (MessageBox)

✔️ FASE 7 — MÓDULO MEDICAMENTOS
12. Crear formulario FrmMedicamento

 Diseño visual

 DataGridView

 Manejo de CRUD completo

 Integración con Cln y Cad

✔️ FASE 8 — VENTAS (INICIO)
13. Configuración inicial

 Crear formulario FrmVenta

 Agregar estructura:

Buscar paciente

Buscar medicamento

Agregar items al detalle

Calcular total

(Pendiente terminar cálculos, validaciones y guardado completo)

✔️ FASE 9 — DOCUMENTACIÓN
14. Documentos del proyecto

 Crear README.md básico

 Crear este TO-DO.md

 Subir capturas de pantalla

 Crear carpeta /docs/

⏳ FASE 10 — TAREAS PENDIENTES
⚠️ PRIORIDAD ALTA

 Añadir control de roles (Administrador / Usuario).

⚠️ PRIORIDAD MEDIA

 Mejorar validaciones en formularios.

 Añadir paginación a los listados.

 Mejorar diseño de los formularios.

⚠️ BAJA PRIORIDAD

 Agregar reportes (Pacientes, Ventas).

 Crear exportación PDF/Excel.

 Implementar logs del sistema.

🌟 FASE FINAL — ENTREGA SIS457

 Completar documentación PDF:

Portada

Introducción

Planteamiento del problema

Objetivos

Diagrama de clases

Modelo entidad-relación

Capturas del sistema

Enlace a GitHub

 Ensayar exposición

 Subir release final en GitHub