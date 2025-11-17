using CadFarmacia2024;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClnFarmacia2024
{
    public class ClnMarca
    {
        public static List<Marca> listar()
        {
            try
            {
                using (var context = new Labsis457farmaciaEntities())
                {
                    return context.Marca.Where(x => x.estado == 1).OrderBy(x => x.descripcion).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar Marcas: " + ex.Message);
                return new List<Marca>();
            }
        }
    }
}