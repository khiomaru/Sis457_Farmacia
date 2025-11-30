using System.Collections.Generic;

namespace WebFarmacia.Models
{
    public class VentaViewModel
    {
        public string TipoDocumento { get; set; }
        public string DocumentoCliente { get; set; }
        public string NombreCliente { get; set; }
        public decimal MontoPago { get; set; }
        public decimal MontoCambio { get; set; }
        public decimal MontoTotal { get; set; }
        public List<VentaDetalleDTO> VentaDetalles { get; set; }
    }

    public class VentaDetalleDTO
    {
        public int IdMedicamento { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Cantidad { get; set; }
        public decimal SubTotal { get; set; }
    }


}
