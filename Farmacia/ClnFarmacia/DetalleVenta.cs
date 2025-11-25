using CadFarmacia;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClnFarmacia
{
    public class DetalleVentaCln
    {
        public static List<DetalleVenta> ObtenerPorVenta(int idVenta)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                return context.DetalleVenta
                    .Include("Medicamento")
                    .Where(d => d.idVenta == idVenta && d.estado == 1)
                    .ToList();
            }
        }
    }
}