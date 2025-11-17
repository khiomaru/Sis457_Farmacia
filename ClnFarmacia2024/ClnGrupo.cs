using CadFarmacia2024;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClnFarmacia2024
{
    public class ClnGrupo
    {
        public static List<Grupo> listar()
        {
            try
            {
               using (var context = new Labsis457farmaciaEntities())
                {
                    return context.Grupo.Where(x => x.estado == 1).OrderBy(x => x.descripcion).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar Grupos: " + ex.Message);
                return new List<Grupo>();
            }
        }
    }
}