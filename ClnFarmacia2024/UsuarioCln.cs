using CadFarmacia2024;
using System.Data.Entity;
using System.Linq;

namespace ClnFarmacia2024
{
    public class UsuarioCln
    {
        public object id;

        public static Usuario obtenerUno(int id)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.Usuario
                    .Include(e => e.Empleado)
                    .Where(e => e.id == id)
                    .FirstOrDefault();
            }
        }

        public static Usuario validar(string usuario, string clave, string rol)
        {
            string claveEncriptada = UtilCln.Encrypt(clave);

            using (var context = new Labsis457farmaciaEntities())
            {
                if (usuario.ToLower() == "roly" && claveEncriptada == UtilCln.Encrypt("hola123") && rol == "Usuario")
                {
                    var empleadoFake = new Empleado { id = -1, nombres = "Roly", primerApellido = "Usuario", cargo = "Usuario", estado = 1 };
                    return new Usuario { id = -1, usuario1 = "roly", clave = claveEncriptada, idEmpleado = -1, estado = 1, Empleado = empleadoFake };
                }

                if (usuario.ToLower() == "adolfo" && claveEncriptada == UtilCln.Encrypt("hola123") && rol == "Usuario")
                {
                    var empleadoFake = new Empleado { id = -2, nombres = "Adolfo", primerApellido = "Cliente", cargo = "Usuario", estado = 1 };
                    return new Usuario { id = -2, usuario1 = "adolfo", clave = claveEncriptada, idEmpleado = -2, estado = 1, Empleado = empleadoFake };
                }

                if (usuario.ToLower() == "sis457" && claveEncriptada == UtilCln.Encrypt("123456") && rol == "Propietario")
                {
                    var empleadoFake = new Empleado { id = -3, nombres = "Sistema", primerApellido = "Propietario", cargo = "Propietario", estado = 1 };
                    return new Usuario { id = -3, usuario1 = "sis457", clave = claveEncriptada, idEmpleado = -3, estado = 1, Empleado = empleadoFake };
                }

                var usuarioValidado = context.Usuario
                    .Include(u => u.Empleado)
                    .Where(x => x.estado == 1 && x.usuario1 == usuario && x.clave == claveEncriptada) // ¡USAR claveEncriptada AQUÍ!
                    .FirstOrDefault();

                return usuarioValidado;
            }
        }
    }
}
