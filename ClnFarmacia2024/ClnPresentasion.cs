using System;
using System.Collections.Generic;
using System.Linq;
using CadFarmacia2024;

namespace ClnFarmacia2024
{
    public class ClnPresentacion
    {
        public static List<Presentacion> listar()
        {
            try
            {
                using (var context = new Labsis457farmaciaEntities())
                {
                    return context.Presentacion.Where(x => x.estado == 1).OrderBy(x => x.descripcion).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar Presentaciones: " + ex.Message);

                return new List<Presentacion>();
            }
        }
    }
}