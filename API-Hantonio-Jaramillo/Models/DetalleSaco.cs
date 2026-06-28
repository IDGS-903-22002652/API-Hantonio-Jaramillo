using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_Hantonio_Jaramillo.Models
{
    [Table("DetalleSaco")]
    public class DetalleSaco
    {
        [Key]
        [Column("IdDetalleSaco")]
        public int IdDetalleSaco { get; set; }

        [Column("IdOrden")]
        public int IdOrden { get; set; }
        [Column("NumeroProduccion")][MaxLength(60)] public string? NumeroProduccion { get; set; }

        [Column("CodigoTela")][MaxLength(50)] public string? CodigoTela { get; set; }
        [Column("CodigoForro")][MaxLength(50)] public string? CodigoForro { get; set; }
        [Column("CodigoBoton")][MaxLength(50)] public string? CodigoBoton { get; set; }
        [Column("EstiloBotones")][MaxLength(50)] public string? EstiloBotones { get; set; }
        [Column("EstiloSolapa")][MaxLength(50)] public string? EstiloSolapa { get; set; }
        [Column("TamanoSolapa")][MaxLength(50)] public string? TamanoSolapa { get; set; }
        [Column("EstiloBolsilloPecho")][MaxLength(50)] public string? EstiloBolsilloPecho { get; set; }
        [Column("EstiloBolsilloInf")][MaxLength(50)] public string? EstiloBolsilloInf { get; set; }
        [Column("EstiloBolsilloTicket")][MaxLength(50)] public string? EstiloBolsilloTicket { get; set; }
        [Column("EstiloOjalIzquierdo")][MaxLength(50)] public string? EstiloOjalIzquierdo { get; set; }
        [Column("EstiloOjalDerecho")][MaxLength(50)] public string? EstiloOjalDerecho { get; set; }
        [Column("Monograma")][MaxLength(50)] public string? Monograma { get; set; }

        [Column("Observaciones")]
        public string? Observaciones { get; set; }
        [Column("PrecioSaco")]
        public decimal PrecioSaco { get; set; }

        [ForeignKey("IdOrden")]
        [JsonIgnore]
        public virtual Orden? Orden { get; set; }
    }
}