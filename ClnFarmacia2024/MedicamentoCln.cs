using CadFarmacia2024;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClnFarmacia2024
{
    public class MedicamentoCln
    {
        // ====================================================================
        // MÉTODOS CRUD PRINCIPALES (MEDICAMENTO)
        // ====================================================================
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
                    // Actualizar stockActual (campo adicional en el partial)
                    existente.stockActual = medicamento.stockActual;
                    // Se agregan campos de marca y presentación para la actualización
                    existente.marca = medicamento.marca;
                    existente.presentacion = medicamento.presentacion;
                    existente.stockMinimo = medicamento.stockMinimo;
                    existente.fechaCaducidad = medicamento.fechaCaducidad;

                    existente.precioCompra = medicamento.precioCompra;
                    existente.precioVenta = medicamento.precioVenta;
                    existente.requiereReceta = medicamento.requiereReceta;
                    // Asegúrate de actualizar el estado si es necesario
                    existente.estado = medicamento.estado;
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
                    // Eliminación lógica: marcar estado como -1
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
                var query = context.Medicamento.Where(x => x.estado != -1);

                if (!string.IsNullOrWhiteSpace(text))
                {
                    var filtro = text.Trim().ToLower();
                    query = query.Where(x =>
                        (x.nombre != null && x.nombre.ToLower().Contains(filtro)) ||
                        (x.codigo != null && x.codigo.ToLower().Contains(filtro)) ||
                        (x.descripcion != null && x.descripcion.ToLower().Contains(filtro)) ||
                        (x.marca != null && x.marca.ToLower().Contains(filtro)) ||
                        (x.presentacion != null && x.presentacion.ToLower().Contains(filtro))
                    );
                }

                return query.ToList();
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
                if (string.IsNullOrWhiteSpace(codigo))
                    return false;

                var c = codigo.Trim().ToLower();
                return context.Medicamento.Any(m => m.estado != -1
                                                    && m.codigo != null
                                                    && m.codigo.ToLower() == c);
            }
        }

        public static bool ActualizarStock(int idMedicamento, decimal precioCompra, decimal precioVenta, int cantidad)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                var medicamento = context.Medicamento.Find(idMedicamento);
                if (medicamento != null)
                {
                    // Actualizar stockActual (suma) y precios
                    medicamento.stockActual = (medicamento.stockActual) + cantidad;
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
                if (medicamento != null && medicamento.stockActual >= cantidad)
                {
                    // Resta la cantidad del stockActual para una venta
                    medicamento.stockActual -= cantidad;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        // ====================================================================
        // MÉTODOS DE LISTADO PARA TABLAS DE SOPORTE (LOOKUP TABLES)
        // ====================================================================

        /// <summary>
        /// Obtiene la lista de Categorías (para cbxCategoria).
        /// </summary>
        public static List<Categoria> listarCategorias()
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.Categoria.Where(x => x.estado == 1).ToList();
            }
        }

        /// <summary>
        /// Obtiene la lista de Marcas (para cboMarca).
        /// </summary>
        public static List<Marca> listarMarcas()
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.Marca.ToList();
            }
        }


        /// <summary>
        /// Obtiene la lista de Presentaciones (para cboPresentacion).
        /// </summary>
        public static List<Presentacion> listarPresentaciones()
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.Presentacion.ToList();
            }
        }

        /// <summary>
        /// Obtiene la lista de Grupos.
        /// </summary>
        public static List<Grupo> listarGrupos()
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.Grupo.ToList();
            }
        }

        /// <summary>
        /// Obtiene la lista de Unidades de Medida.
        /// </summary>
        public static List<UnidadMedida> listarUnidadesMedida()
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.UnidadMedida.ToList();
            }
        }

        /// <summary>
        /// Obtiene la lista de Clasificaciones ATK.
        /// </summary>
        public static List<ClasificacionATK> listarClasificacionesATK()
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.ClasificacionATK.ToList();
            }
        }
    }
}