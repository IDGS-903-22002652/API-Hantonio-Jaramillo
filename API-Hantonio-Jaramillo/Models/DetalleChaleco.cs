using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_Hantonio_Jaramillo.Models
{
    [Table("DetalleChaleco")]
    public class DetalleChaleco
    {
        [Key]
        [Column("IdDetalleChaleco")]
        public int IdDetalleChaleco { get; set; }

        [Column("IdOrden")]
        public int IdOrden { get; set; }
        [Column("NumeroProduccion")][MaxLength(60)] public string? NumeroProduccion { get; set; }

        [Column("CodigoTela")][MaxLength(50)] public string? CodigoTela { get; set; }
        [Column("CodigoBoton")][MaxLength(50)] public string? CodigoBoton { get; set; }
        [Column("EstiloCuello")][MaxLength(50)] public string? EstiloCuello { get; set; }
        [Column("EstiloBotones")][MaxLength(50)] public string? EstiloBotones { get; set; }
        [Column("EstiloBolsilloPecho")][MaxLength(50)] public string? EstiloBolsilloPecho { get; set; }
        [Column("EstiloBolsilloInf")][MaxLength(50)] public string? EstiloBolsilloInf { get; set; }
        [Column("TerminacionInf")][MaxLength(50)] public string? TerminacionInf { get; set; }

        [Column("Observaciones")]
        public string? Observaciones { get; set; }
        [Column("PrecioChaleco")]
        public decimal PrecioChaleco { get; set; }

        [ForeignKey("IdOrden")]
        [JsonIgnore]
        public virtual Orden? Orden { get; set; }
    }
}