using CadFarmacia2024;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClnFarmacia2024
{
    public class DetalleVentaCln
    {
        public static void RegistrarDetalleVenta(DetalleVenta detalle)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                try
                {
                    context.DetalleVenta.Add(detalle);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al registrar detalle de venta: " + ex.Message);
                }
            }
        }

        public static List<DetalleVentaDTO> ObtenerDetallesPorIdVenta(int idVenta)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.DetalleVenta
                    .Where(d => d.idVenta == idVenta)
                    .Select(d => new DetalleVentaDTO
                    {
                        idMedicamento = d.idMedicamento,
                        codigo = d.Medicamento.codigo,
                        nombre = d.Medicamento.nombre,
                        precioVenta = (decimal)d.precioVenta,
                        cantidad = d.cantidad,
                        subTotal = (decimal)d.subTotal
                    }).ToList();
            }
        }
    }
}
