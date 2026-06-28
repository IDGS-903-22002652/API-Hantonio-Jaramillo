using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_Hantonio_Jaramillo.Models
{
    [Table("DetalleCamisa")]
    public class DetalleCamisa
    {
        [Key]
        [Column("IdDetalleCamisa")]
        public int IdDetalleCamisa { get; set; }

        [Column("IdOrden")]
        public int IdOrden { get; set; }
        [Column("NumeroProduccion")][MaxLength(60)] public string? NumeroProduccion { get; set; }
        [Column("OpcionCamisa")][MaxLength(50)] public string? OpcionCamisa { get; set; }
        [Column("CodigoTela")][MaxLength(50)] public string? CodigoTela { get; set; }
        [Column("EstiloCuello")][MaxLength(50)] public string? EstiloCuello { get; set; }
        [Column("ContrasteTela")][MaxLength(50)] public string? ContrasteTela { get; set; }
        [Column("EstiloTapeta")][MaxLength(50)] public string? EstiloTapeta { get; set; }
        [Column("EstiloPuno")][MaxLength(50)] public string? EstiloPuno { get; set; }
        [Column("EstiloBolsillo")][MaxLength(50)] public string? EstiloBolsillo { get; set; }
        [Column("PlieguesFrontales")][MaxLength(50)] public string? PlieguesFrontales { get; set; }
        // Agrégalos junto a los demás campos de texto
        [Column("SolapaBolsillo")][MaxLength(50)] public string? SolapaBolsillo { get; set; }
        [Column("PosicionContraste")][MaxLength(50)] public string? PosicionContraste { get; set; }
        [Column("Iniciales")][MaxLength(10)] public string? Iniciales { get; set; }

        [Column("Observaciones")]
        public string? Observaciones { get; set; }
        [Column("PrecioCamisa")]
        public decimal PrecioCamisa { get; set; }

        [ForeignKey("IdOrden")]
        [JsonIgnore]
        public virtual Orden? Orden { get; set; }
    }
}