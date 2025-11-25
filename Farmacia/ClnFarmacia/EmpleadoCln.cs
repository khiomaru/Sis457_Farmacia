using CadFarmacia;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClnFarmacia
{
    public class EmpleadoCln
    {
        public static List<paEmpleadoListar_Result> Listar(string parametro)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                return context.paEmpleadoListar(parametro).ToList();
            }
        }

        public static Empleado ObtenerPorId(int id)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                return context.Empleado.Find(id);
            }
        }

        public static int Insertar(Empleado empleado)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                context.Empleado.Add(empleado);
                context.SaveChanges();
                return empleado.id;
            }
        }

        public static void Actualizar(Empleado empleado)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                context.Entry(empleado).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static void Eliminar(int id)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                var empleado = context.Empleado.Find(id);
                if (empleado != null)
                {
                    empleado.estado = -1;
                    context.SaveChanges();
                }
            }
        }

        public static void CrearUsuario(int idEmpleado, string usuario, string clave)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                var nuevoUsuario = new Usuario
                {
                    idEmpleado = idEmpleado,
                    usuario1 = usuario,
                    clave = UsuarioCln.Encriptar(clave),
                    estado = 1
                };
                context.Usuario.Add(nuevoUsuario);
                context.SaveChanges();
            }
        }
    }
}