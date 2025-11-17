using System;
using System.Collections.Generic;
using System.Linq;
using CadFarmacia2024;

namespace ClnFarmacia2024
{
    public class ClnCategoria
    {
        public static List<Categoria> listar()
        {
            try
            {
                using (var context = new Labsis457farmaciaEntities())
                {
                    return context.Categoria.Where(x => x.estado == 1).OrderBy(x => x.descripcion).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar Categorias: " + ex.Message);
                return new List<Categoria>();
            }
        }
    }
}