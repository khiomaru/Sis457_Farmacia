using System;

namespace ClnFarmacia2024
{
    public class RegistroCompraDTO
    {
        public DateTime FechaRegistro { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public decimal MontoTotal { get; set; }
        public string UsuarioRegistro { get; set; }
        public string DocumentoProveedor { get; set; }
        public string RazonSocialProveedor { get; set; }
        public string CodigoMedicamento { get; set; }
        public string NombreMedicamento { get; set; }
        public string Categoria { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }


    }
}









