using CadFarmacia;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClnFarmacia
{
    public class MedicamentoCln
    {
        public static List<Medicamento> Listar(string parametro)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                return context.Medicamento
                    .Include("Categoria")
                    .Include("Laboratorio")
                    .Where(m => m.estado == 1 && m.nombre.Contains(parametro))
                    .OrderBy(m => m.nombre)
                    .ToList();
            }
        }

        public static List<paMedicamentoListar_Result> ListarPA(string parametro)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                return context.paMedicamentoListar(parametro).ToList();
            }
        }

        public static List<Medicamento> ListarActivos()
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                return context.Medicamento
                    .Include("Categoria")
                    .Include("Laboratorio")
                    .Where(m => m.estado == 1 && m.stock > 0)
                    .OrderBy(m => m.nombre)
                    .ToList();
            }
        }

        public static Medicamento ObtenerPorId(int id)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                return context.Medicamento
                    .Include("Categoria")
                    .Include("Laboratorio")
                    .FirstOrDefault(m => m.id == id);
            }
        }

        public static int Insertar(Medicamento medicamento)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                context.Medicamento.Add(medicamento);
                context.SaveChanges();
                return medicamento.id;
            }
        }

        public static void Actualizar(Medicamento medicamento)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                context.Entry(medicamento).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static void Eliminar(int id)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                var medicamento = context.Medicamento.Find(id);
                if (medicamento != null)
                {
                    medicamento.estado = -1;
                    context.SaveChanges();
                }
            }
        }

        public static void ActualizarStock(int idMedicamento, int cantidadVendida)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                var medicamento = context.Medicamento.Find(idMedicamento);
                if (medicamento != null)
                {
                    medicamento.stock -= cantidadVendida;
                    context.SaveChanges();
                }
            }
        }
    }
}