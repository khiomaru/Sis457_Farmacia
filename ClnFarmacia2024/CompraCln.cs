using CadFarmacia2024;
using System.Data.Entity;
using System.Linq;

namespace ClnFarmacia2024
{
    public class CompraCln
    {
        public static int RegistrarCompra(Compra compra)
        {
            using (var context = new Labsis457farmaciaEntities()) 
            {
                context.Compra.Add(compra);
                context.SaveChanges();
                return compra.id;
            }
        }

        public static bool ExisteNumeroFactura(string numeroFactura)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.Compra.Any(c => c.numeroDocumento == numeroFactura);
            }
        }

        public static Compra ObtenerCompraPorNumeroDocumento(string numeroDocumento)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.Compra
                    .Include(c => c.Proveedor)
                    .FirstOrDefault(c => c.numeroDocumento == numeroDocumento);
            }
        }
    }
}

