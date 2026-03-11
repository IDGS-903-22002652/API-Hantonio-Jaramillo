using System.Text.Json.Serialization; // Importante para que funcionen los nombres

namespace API_Hantonio_Jaramillo.DTOs
{
    public class OrdenMasterDTOs
    {
        public int IdCliente { get; set; }

        // --- CAMPOS QUE FALTABAN Y EL FRONTEND ESTÁ ENVIANDO ---
        public int IdSucursal { get; set; }
        public int IdEstatus { get; set; }
        public bool IncluyeCamisa { get; set; }
        // -------------------------------------------------------

        public int IdTipoTraje { get; set; }

        // Estos pueden ser opcionales si no los mandas en el form simplificado
        public decimal CostoTotal { get; set; } = 0;
        public decimal MontoAbonado { get; set; } = 0;

        public string? MetodoPago { get; set; }

        public DateTime? FechaCitaMedidas { get; set; }
        public DateTime? FechaEventoEntrega { get; set; }

        // --- MAPEO DE JSON (PUENTE ENTRE REACT Y C#) ---
        // React envía "MedidasOrden", tu DTO se llama "Medidas" -> Esto los une.

        [JsonPropertyName("MedidasOrden")]
        public MedidasDto? Medidas { get; set; }

        [JsonPropertyName("DetalleSaco")]
        public DetalleSacoDTOs? Saco { get; set; }

        [JsonPropertyName("DetallePantalon")]
        public DetallePantalonDTOs? Pantalon { get; set; }

        [JsonPropertyName("DetalleChaleco")]
        public DetalleChalecoDTOs? Chaleco { get; set; }

        [JsonPropertyName("DetalleCamisa")] // React puede enviarlo como "DetalleCamisa" si activas el check
        public DetalleCamisaDTOs? Camisa { get; set; }
    }
}