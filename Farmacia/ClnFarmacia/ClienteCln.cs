using CadFarmacia;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClnFarmacia
{
    public class ClienteCln
    {
        public static List<paClienteListar_Result> Listar(string parametro)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                return context.paClienteListar(parametro).ToList();
            }
        }

        public static List<Cliente> ListarActivos()
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                return context.Cliente
                    .Where(c => c.estado == 1)
                    .OrderBy(c => c.nombres)
                    .ThenBy(c => c.apellidos)
                    .ToList();
            }
        }

        public static Cliente ObtenerPorId(int id)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                return context.Cliente.Find(id);
            }
        }

        public static int Insertar(Cliente cliente)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                context.Cliente.Add(cliente);
                context.SaveChanges();
                return cliente.id;
            }
        }

        public static void Actualizar(Cliente cliente)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                context.Entry(cliente).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static void Eliminar(int id)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                var cliente = context.Cliente.Find(id);
                if (cliente != null)
                {
                    cliente.estado = -1;
                    context.SaveChanges();
                }
            }
        }
    }
}