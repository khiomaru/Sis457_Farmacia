using CadFarmacia;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClnFarmacia
{
    public class CategoriaCln
    {
        public static List<Categoria> Listar()
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                return context.Categoria
                    .Where(c => c.estado == 1)
                    .OrderBy(c => c.nombre)
                    .ToList();
            }
        }

        public static Categoria ObtenerPorId(int id)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                return context.Categoria.Find(id);
            }
        }

        public static int Insertar(Categoria categoria)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                context.Categoria.Add(categoria);
                context.SaveChanges();
                return categoria.id;
            }
        }

        public static void Actualizar(Categoria categoria)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                context.Entry(categoria).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static void Eliminar(int id)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                var categoria = context.Categoria.Find(id);
                if (categoria != null)
                {
                    categoria.estado = -1;
                    context.SaveChanges();
                }
            }
        }
    }
}