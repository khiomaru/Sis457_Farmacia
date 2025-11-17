using System;
using System.Collections.Generic;
using System.Linq;
using CadFarmacia2024;

namespace ClnFarmacia2024
{
    public class ClnUnidadMedida
    {
        public static List<UnidadMedida> listar()
        {
            try
            {
                using (var context = new Labsis457farmaciaEntities())
                {
                    return context.UnidadMedida.Where(x => x.estado == 1).OrderBy(x => x.descripcion).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar Unidades de Medida: " + ex.Message);
                return new List<UnidadMedida>();
            }
        }
    }
}