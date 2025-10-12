using CadFarmacia2024;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClnFarmacia2024
{
    public class PacienteCln
    {
        public static int insertar(Paciente paciente)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                context.Paciente.Add(paciente);
                context.SaveChanges();
                return paciente.id;
            }
        }

        public static int actualizar(Paciente paciente)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                var existente = context.Paciente.Find(paciente.id);
                if (existente != null)
                {
                    existente.documento = paciente.documento;
                    existente.nombreCompleto = paciente.nombreCompleto;
                    existente.email = paciente.email;
                    existente.telefono = paciente.telefono;
                    existente.direccion = paciente.direccion;
                    existente.usuarioRegistro = paciente.usuarioRegistro;
                    return context.SaveChanges();
                }
                return 0;
            }
        }

        public static int eliminar(int id, string usuario)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                var paciente = context.Paciente.Find(id);
                if (paciente != null)
                {
                    paciente.estado = -1;
                    paciente.usuarioRegistro = usuario;
                    return context.SaveChanges();
                }
                return 0;
            }
        }

        public static Paciente obtenerUno(int id)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.Paciente.Find(id);
            }
        }

        public static List<Paciente> listar()
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.Paciente.Where(x => x.estado != -1).ToList();
            }
        }

        public static List<paPacienteListar_Result> listarPa(string parametro)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.paPacienteListar(parametro).ToList();
            }
        }

        public static bool ExisteDocumento(string documento)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.Paciente.Any(p => p.documento.Equals(documento, StringComparison.OrdinalIgnoreCase) && p.estado != -1);
            }
        }
    }
}

