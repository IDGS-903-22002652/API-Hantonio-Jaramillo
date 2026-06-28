using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_Hantonio_Jaramillo.Models
{
    public class DetalleZapato
    {
        [Key]
        [Column("IdDetalleZapato")]
        public int IdDetalleZapato { get; set; }
        [Column("IdOrden")]
        public int IdOrden { get; set; }
        [Column("NumeroProduccion")][MaxLength(60)] public string? NumeroProduccion { get; set; }

        [Column("EstiloZapato")]
        [MaxLength(50)] 
        public string? EstiloZapato { get; set; }
        [Column("Observaciones")]
        public string? Observaciones { get; set; }
        [Column("PrecioZapato")]
        public decimal PrecioZapato { get; set; }
        [ForeignKey("IdOrden")]
        [JsonIgnore]
        public virtual Orden? Orden { get; set; }

    }
}
