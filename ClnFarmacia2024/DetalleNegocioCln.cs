using CadFarmacia2024;
using System.Linq;

namespace ClnFarmacia2024
{
    public class DetalleNegocioCln
    {
        public static DetalleNegocio Obtener()
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.DetalleNegocio.FirstOrDefault();
            }
        }
    }
}









