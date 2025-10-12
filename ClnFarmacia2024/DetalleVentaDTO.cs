namespace ClnFarmacia2024
{
    public class DetalleVentaDTO
    {
        public int idMedicamento { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public decimal precioVenta { get; set; }
        public int cantidad { get; set; }
        public decimal subTotal { get; set; }
    }
}
