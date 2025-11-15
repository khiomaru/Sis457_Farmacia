using System;

namespace CadFarmacia2024
{
    // Partial para añadir campos adicionales sin tocar el archivo generado por EF.
    public partial class Medicamento
    {
        public string marca { get; set; }
        public string presentacion { get; set; }
        public int stockActual { get; set; }
        public int stockMinimo { get; set; }
        public DateTime? fechaCaducidad { get; set; }
        public string codigoBarras { get; set; }

        // Nota: la propiedad 'estado' ya existe en la clase generada (short estado).
        // Estos campos son sólo propiedades de la clase en memoria; para persistirlos
        // debes actualizar el modelo EF / la base de datos (EDMX o migraciones).
    }
}