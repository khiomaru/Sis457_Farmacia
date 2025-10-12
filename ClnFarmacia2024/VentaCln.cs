using CadFarmacia2024;
using System.Data.Entity;
using System.Linq;

namespace ClnFarmacia2024
{
    public class VentaCln
    {
        public static int RegistrarVenta(Venta venta)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                context.Venta.Add(venta);
                context.SaveChanges();
                return venta.id;
            }
        }

        public static bool ExisteNumeroDocumento(string numeroDocumento)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.Venta.Any(v => v.numeroDocumento == numeroDocumento);
            }
        }

        public static Venta ObtenerVentaPorNumeroDocumento(string numeroDocumento)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.Venta
                    .Include(v => v.DetalleVenta)
                    .FirstOrDefault(v => v.numeroDocumento == numeroDocumento);
            }
        }
    }
}
