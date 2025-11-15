using CadFarmacia2024;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClnFarmacia2024
{
    public class MedicamentoCln
    {
        public static int insertar(Medicamento medicamento)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                context.Medicamento.Add(medicamento);
                context.SaveChanges();
                return medicamento.id;
            }
        }

        public static int actualizar(Medicamento medicamento)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                var existente = context.Medicamento.Find(medicamento.id);
                if (existente != null)
                {
                    existente.idCategoria = medicamento.idCategoria;
                    existente.codigo = medicamento.codigo;
                    existente.nombre = medicamento.nombre;
                    existente.descripcion = medicamento.descripcion;
                    existente.tipoUnidad = medicamento.tipoUnidad;
                    existente.stock = medicamento.stock;
                    existente.precioCompra = medicamento.precioCompra;
                    existente.precioVenta = medicamento.precioVenta;
                    existente.requiereReceta = medicamento.requiereReceta;
                    existente.usuarioRegistro = medicamento.usuarioRegistro;
                    return context.SaveChanges();
                }
                return 0;
            }
        }

        public static int eliminar(int id, string usuario)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                var medicamento = context.Medicamento.Find(id);
                if (medicamento != null)
                {
                    medicamento.estado = -1;
                    medicamento.usuarioRegistro = usuario;
                    return context.SaveChanges();
                }
                return 0;
            }
        }

        public static Medicamento obtenerUno(int id)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.Medicamento.Find(id);
            }
        }

        public static List<Medicamento> listar(string text)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.Medicamento.Where(x => x.estado != -1).ToList();
            }
        }

        public static List<paMedicamentoListar_Result> listaaPa(string parametro)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.paMedicamentoListar(parametro).ToList();
            }
        }

        public static bool ExisteCodigo(string codigo)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.Medicamento.Any(m => m.codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase) && m.estado != -1);
            }
        }

        public static bool ActualizarStock(int idMedicamento, decimal precioCompra, decimal precioVenta, int cantidad)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                var medicamento = context.Medicamento.Find(idMedicamento);
                if (medicamento != null)
                {
                    medicamento.stock += cantidad;
                    medicamento.precioCompra = precioCompra;
                    medicamento.precioVenta = precioVenta;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public static bool ActualizarStockVenta(int idMedicamento, int cantidad)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                var medicamento = context.Medicamento.Find(idMedicamento);
                if (medicamento != null && medicamento.stock >= cantidad)
                {
                    medicamento.stock -= cantidad;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}

