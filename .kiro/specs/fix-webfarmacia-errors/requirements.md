# Requirements Document

## Introduction

Este documento define los requisitos para corregir los errores críticos en el proyecto WebFarmacia que impiden su correcto funcionamiento. El sistema WebFarmacia es una aplicación web ASP.NET Core MVC para la gestión de una farmacia, incluyendo medicamentos, ventas, clientes y empleados.

## Glossary

- **WebFarmacia**: Sistema web de gestión de farmacia desarrollado en ASP.NET Core MVC
- **Entity Framework Core**: Framework ORM utilizado para el acceso a datos
- **DbContext**: Clase que representa la sesión con la base de datos en Entity Framework
- **Target Framework**: Versión del framework .NET que el proyecto utiliza para compilar y ejecutar
- **Residual Files**: Archivos que pertenecen a otro proyecto y no deberían estar presentes

## Requirements

### Requirement 1

**User Story:** Como desarrollador, quiero que el proyecto compile correctamente con .NET 10.0 preview, para que la aplicación pueda ejecutarse sin errores de framework.

#### Acceptance Criteria

1. THE WebFarmacia SHALL verify that .NET 10.0 SDK is properly installed on the system
2. THE WebFarmacia SHALL use Entity Framework Core packages version 9.0.0 compatible with .NET 10.0
3. WHEN the project is built, THE WebFarmacia SHALL compile without framework version errors
4. IF .NET 10.0 is not recognized, THE WebFarmacia SHALL provide clear error messages indicating SDK installation issues

### Requirement 2

**User Story:** Como desarrollador, quiero eliminar todos los archivos residuales de otros proyectos, para que no haya conflictos ni confusión en la configuración del proyecto.

#### Acceptance Criteria

1. THE WebFarmacia SHALL NOT contain any files with "WebHamburgueseria" in their name
2. THE WebFarmacia SHALL NOT contain any configuration references to "WebHamburgueseria"
3. WHEN the project structure is reviewed, THE WebFarmacia SHALL only contain files related to the pharmacy domain

### Requirement 3

**User Story:** Como desarrollador, quiero que los modelos de Entity Framework coincidan exactamente con el esquema de la base de datos, para que las operaciones CRUD funcionen correctamente.

#### Acceptance Criteria

1. THE WebFarmacia SHALL map model properties to database columns with exact name matching
2. THE WebFarmacia SHALL use "Medicamento" as the table name and model class name (not "Producto")
3. THE WebFarmacia SHALL configure Categoria table with "nombre" column (not just "descripcion")
4. THE WebFarmacia SHALL update all controllers to use "Medicamento" instead of "Producto"
5. THE WebFarmacia SHALL ensure all navigation properties match the database foreign key relationships

### Requirement 4

**User Story:** Como desarrollador, quiero que todas las relaciones de Entity Framework estén correctamente configuradas, para que las operaciones de base de datos funcionen sin errores.

#### Acceptance Criteria

1. THE WebFarmacia SHALL configure all foreign key relationships in the DbContext
2. THE WebFarmacia SHALL use correct navigation property names that match the model definitions
3. WHEN database queries include related entities, THE WebFarmacia SHALL load them without errors

### Requirement 5

**User Story:** Como usuario del sistema, quiero que la aplicación se ejecute sin errores, para que pueda gestionar los medicamentos y ventas de la farmacia.

#### Acceptance Criteria

1. WHEN the application starts, THE WebFarmacia SHALL initialize the database context successfully
2. WHEN accessing any controller action, THE WebFarmacia SHALL respond without runtime errors
3. THE WebFarmacia SHALL display views correctly with proper model binding
4. THE WebFarmacia SHALL connect to the FinalFarmacia database using the correct connection string

### Requirement 6

**User Story:** Como desarrollador, quiero implementar mejoras de seguridad y mejores prácticas, para que la aplicación sea más robusta y mantenible.

#### Acceptance Criteria

1. THE WebFarmacia SHALL hash passwords using a secure algorithm (not plain text)
2. THE WebFarmacia SHALL validate user input to prevent SQL injection and XSS attacks
3. THE WebFarmacia SHALL implement proper error handling with user-friendly messages
4. THE WebFarmacia SHALL use async/await patterns consistently for database operations
5. THE WebFarmacia SHALL implement proper logging for debugging and monitoring
