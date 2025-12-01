using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using WebFarmacia.Models;

namespace WebFarmacia.Services;

public class AuthorizeRoleAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private readonly Rol[] _roles;

    public AuthorizeRoleAttribute(params Rol[] roles)
    {
        _roles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (!user.Identity?.IsAuthenticated ?? true)
        {
            context.Result = new ChallengeResult();
            return;
        }

        // Si no hay roles especificados, solo verificar que esté autenticado
        if (_roles.Length == 0)
            return;

        // Obtener el rol del usuario desde la base de datos
        var username = user.Identity?.Name;
        if (string.IsNullOrEmpty(username))
        {
            context.Result = new ForbidResult();
            return;
        }

        var dbContext = context.HttpContext.RequestServices.GetService<FarmaciaContext>();
        if (dbContext == null)
        {
            context.Result = new ForbidResult();
            return;
        }

        var usuario = dbContext.Usuarios
            .Include(u => u.IdEmpleadoNavigation)
            .FirstOrDefault(u => u.Usuario1 == username && u.Estado == 1);

        if (usuario == null)
        {
            context.Result = new ForbidResult();
            return;
        }

        // Determinar el rol basado en el cargo del empleado
        Rol? usuarioRol = null;
        if (usuario.IdEmpleadoNavigation != null)
        {
            usuarioRol = usuario.IdEmpleadoNavigation.Cargo?.ToLower() switch
            {
                "administrador" or "gerente" or "director" => Rol.ADMIN,
                "empleado" or "farmaceutico" or "vendedor" or "cajero" => Rol.EMPLEADO,
                _ => Rol.EMPLEADO // Por defecto
            };
        }

        if (usuarioRol == null || !_roles.Contains(usuarioRol.Value))
        {
            context.Result = new ForbidResult();
        }
    }
}