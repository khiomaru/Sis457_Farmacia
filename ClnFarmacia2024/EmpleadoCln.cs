using CadFarmacia2024;
using System;
using System.Collections.Generic;
using System.Data.Entity; // Necesario para el método .Include()
using System.Linq;

namespace ClnFarmacia2024
{
    public class EmpleadoCln
    {
        public static int insertar(Empleado empleado, Usuario usuario)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                context.Empleado.Add(empleado);
                context.SaveChanges();

                if (usuario != null)
                {
                    usuario.idEmpleado = empleado.id;
                    usuario.usuarioRegistro = empleado.usuarioRegistro;
                    usuario.fechaRegistro = empleado.fechaRegistro;
                    usuario.estado = empleado.estado;
                    context.Usuario.Add(usuario);
                    context.SaveChanges();
                }

                return empleado.id;
            }
        }

        public static int actualizar(Empleado empleado)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                // CORRECCIÓN: Se usa SingleOrDefault para buscar por clave primaria.
                var existente = context.Empleado.SingleOrDefault(e => e.id == empleado.id);

                if (existente == null)
                {
                    // Se recomienda no lanzar excepciones de EF en la capa CLN, sino devolver un código de error o manejarlo.
                    // Pero manteniendo tu estructura:
                    throw new Exception("Empleado no encontrado");
                }

                // Actualización de campos
                existente.cedulaIdentidad = empleado.cedulaIdentidad;
                existente.nombres = empleado.nombres;
                existente.primerApellido = empleado.primerApellido;
                existente.segundoApellido = empleado.segundoApellido;
                existente.direccion = empleado.direccion;
                existente.celular = empleado.celular;
                existente.cargo = empleado.cargo;
                existente.usuarioRegistro = empleado.usuarioRegistro;

                return context.SaveChanges();
            }
        }

        public static int eliminar(int id, string usuario)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                // CORRECCIÓN: Se usa SingleOrDefault para buscar por clave primaria.
                var empleado = context.Empleado.SingleOrDefault(e => e.id == id);

                if (empleado != null)
                {
                    empleado.estado = -1;
                    empleado.usuarioRegistro = usuario;
                    return context.SaveChanges();
                }
                return 0; // No se encontró el empleado
            }
        }

        public static Empleado obtenerUno(int id)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                // Se usa FirstOrDefault o SingleOrDefault para obtener un registro.
                // .Include(e => e.Usuario) requiere que 'Usuario' sea la propiedad de navegación en la entidad Empleado.
                var empleado = context.Empleado
                    .Include(e => e.Usuario)
                    .FirstOrDefault(e => e.id == id && e.estado != -1);

                return empleado;
            }
        }

        public static List<Empleado> listar()
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.Empleado.Where(x => x.estado != -1).ToList();
            }
        }

        // Implementación pública de listarPa usando el stub en Labsis457farmaciaEntities
        public static List<paEmpleadoListar_Result> listarPa(string parametro)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.paEmpleadoListar(parametro).ToList();
            }
        }

        public static bool ExisteCedulaIdentidad(string cedulaIdentidad)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.Empleado.Any(e => e.cedulaIdentidad.Equals(cedulaIdentidad, StringComparison.OrdinalIgnoreCase) && e.estado != -1);
            }
        }
    }
}