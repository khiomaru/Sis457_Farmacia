using CadFarmacia2024;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClnFarmacia2024
{
    public class ProveedorCln
    {
        // Insertar un proveedor
        public static int Insertar(Proveedor proveedor)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                context.Proveedor.Add(proveedor);
                context.SaveChanges();
                return proveedor.id;
            }
        }

        // Actualizar un proveedor
        public static int Actualizar(Proveedor proveedor)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                var existente = context.Proveedor.Find(proveedor.id);
                if (existente != null)
                {
                    existente.documento = proveedor.documento;
                    existente.razonSocial = proveedor.razonSocial;
                    existente.email = proveedor.email;
                    existente.telefono = proveedor.telefono;
                    existente.usuarioRegistro = proveedor.usuarioRegistro;
                    return context.SaveChanges();
                }
                return 0;
            }
        }

        // Eliminaci�n l�gica
        public static int Eliminar(int id, string usuario)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                var proveedor = context.Proveedor.Find(id);
                if (proveedor != null)
                {
                    proveedor.estado = -1;
                    proveedor.usuarioRegistro = usuario;
                    return context.SaveChanges();
                }
                return 0;
            }
        }

        // Obtener un proveedor por ID
        public static Proveedor ObtenerUno(int id)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.Proveedor.Find(id);
            }
        }

        // Listar todos los proveedores activos
        public static List<ProveedorDTO> Listar()
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.Proveedor
                              .Where(p => p.estado != -1)
                              .Select(p => new ProveedorDTO
                              {
                                  id = p.id,
                                  documento = p.documento,
                                  razonSocial = p.razonSocial,
                                  email = p.email,
                                  telefono = p.telefono
                              })
                              .ToList();
            }
        }

        // Listar proveedores filtrando por texto (LINQ)
        public static List<ProveedorDTO> ListarPorParametro(string parametro)
        {
            using (var context = new Labsis457farmaciaEntities())
            {
                return context.Proveedor
                              .Where(p => p.estado != -1 &&
                                          (p.razonSocial.Contains(parametro) || p.documento.Contains(parametro)))
                              .Select(p => new ProveedorDTO
                              {
                                  id = p.id,
                                  documento = p.documento,
                                  razonSocial = p.razonSocial,
                                  email = p.email,
                                  telefono = p.telefono
                              })
                              .ToList();
            }
        }
        


    }

}

