using CadFarmacia;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClnFarmacia
{
    public class LaboratorioCln
    {
        public static List<paLaboratorioListar_Result> Listar(string parametro)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                return context.paLaboratorioListar(parametro).ToList();
            }
        }

        public static List<Laboratorio> ListarActivos()
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                return context.Laboratorio
                    .Where(l => l.estado == 1)
                    .OrderBy(l => l.nombre)
                    .ToList();
            }
        }

        public static Laboratorio ObtenerPorId(int id)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                return context.Laboratorio.Find(id);
            }
        }

        public static int Insertar(Laboratorio laboratorio)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                context.Laboratorio.Add(laboratorio);
                context.SaveChanges();
                return laboratorio.id;
            }
        }

        public static void Actualizar(Laboratorio laboratorio)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                context.Entry(laboratorio).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static void Eliminar(int id)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                var laboratorio = context.Laboratorio.Find(id);
                if (laboratorio != null)
                {
                    laboratorio.estado = 0;
                    context.SaveChanges();
                }
            }
        }
    }
}