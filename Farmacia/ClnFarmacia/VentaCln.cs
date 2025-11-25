using CadFarmacia;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClnFarmacia
{
    public class VentaCln
    {
        public static List<paVentaListar_Result> Listar(string parametro)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                return context.paVentaListar(parametro).ToList();
            }
        }

        public static Venta ObtenerPorId(int id)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                return context.Venta
                    .Include("Cliente")
                    .Include("Usuario")
                    .Include("DetalleVenta")
                    .Include("DetalleVenta.Medicamento")
                    .FirstOrDefault(v => v.id == id);
            }
        }

        public static int Insertar(Venta venta, List<DetalleVenta> detalles)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        // Insertar venta
                        context.Venta.Add(venta);
                        context.SaveChanges();

                        // Insertar detalles y actualizar stock
                        foreach (var detalle in detalles)
                        {
                            detalle.idVenta = venta.id;
                            context.DetalleVenta.Add(detalle);

                            // Actualizar stock del medicamento
                            var medicamento = context.Medicamento.Find(detalle.idMedicamento);
                            if (medicamento != null)
                            {
                                medicamento.stock -= detalle.cantidad;
                            }
                        }
                        context.SaveChanges();

                        transaction.Commit();
                        return venta.id;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static void Anular(int id)
        {
            using (var context = new Labsis457FarmaciaEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var venta = context.Venta
                            .Include("DetalleVenta")
                            .FirstOrDefault(v => v.id == id);

                        if (venta != null)
                        {
                            // Devolver stock
                            foreach (var detalle in venta.DetalleVenta)
                            {
                                var medicamento = context.Medicamento.Find(detalle.idMedicamento);
                                if (medicamento != null)
                                {
                                    medicamento.stock += detalle.cantidad;
                                }
                                detalle.estado = 0;
                            }

                            venta.estado = 0; // Anulada
                            context.SaveChanges();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}