using System;
using System.Collections.Generic;
using System.Linq;
using CadFarmacia2024; // Asegúrate de que este 'using' apunte a tu proyecto de modelos

namespace ClnFarmacia2024
{
     public class ClnClasificacionATK
    {
        public static List<ClasificacionATK> listar()
        {
            try
            {
                using (var context = new Labsis457farmaciaEntities())
                {
                    return context.ClasificacionATK.Where(x => x.estado == 1).OrderBy(x => x.descripcion).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar Clasificaciones ATK: " + ex.Message);
                return new List<ClasificacionATK>();
            }
        }
    }
}