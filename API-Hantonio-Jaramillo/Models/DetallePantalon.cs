using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_Hantonio_Jaramillo.Models
{
    [Table("DetallePantalon")]
    public class DetallePantalon
    {
        [Key]
        [Column("IdDetallePantalon")]
        public int IdDetallePantalon { get; set; }

        [Column("IdOrden")]
        public int IdOrden { get; set; }
        [Column("NumeroProduccion")][MaxLength(60)] public string? NumeroProduccion { get; set; }

        [Column("CodigoTela")][MaxLength(50)] public string? CodigoTela { get; set; }
        [Column("CodigoBoton")][MaxLength(50)] public string? CodigoBoton { get; set; }
        [Column("EstiloPretina")][MaxLength(50)] public string? EstiloPretina { get; set; }
        [Column("AjusteCintura")][MaxLength(50)] public string? AjusteCintura { get; set; }
        [Column("AlturaPretina")][MaxLength(50)] public string? AlturaPretina { get; set; }
        [Column("EstiloPliegues")][MaxLength(50)] public string? EstiloPliegues { get; set; }
        [Column("EstiloBolsilloReloj")][MaxLength(50)] public string? EstiloBolsilloReloj { get; set; }
        [Column("EstiloBajos")][MaxLength(50)] public string? EstiloBajos { get; set; }

        [Column("Observaciones")]
        public string? Observaciones { get; set; }
        [Column("PrecioPantalon")]
        public decimal PrecioPantalon { get; set; }

        [ForeignKey("IdOrden")]
        [JsonIgnore]
        public virtual Orden? Orden { get; set; }
    }
}