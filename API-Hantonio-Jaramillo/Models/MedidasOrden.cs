using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_Hantonio_Jaramillo.Models
{
    [Table("MedidasOrden")]
    public class MedidasOrden
    {
        [Key]
        [Column("IdMedida")]
        public int IdMedida { get; set; }

        [Column("IdOrden")]
        public int IdOrden { get; set; }

        [Column("Altura")] 
        public decimal? Altura { get; set; }

        [Column("Peso")] 
        public decimal? Peso { get; set; }

        [Column("TallaZapato")]
        [MaxLength(10)] 
        public string? TallaZapato { get; set; }
        [Column("TipoFit")][MaxLength(50)] public string? TipoFit { get; set; }

        [Column("SacoLargoFrente")] 
        public decimal? SacoLargoFrente { get; set; }

        [Column("SacoLargoEspalda")] 
        public decimal? SacoLargoEspalda { get; set; }

        [Column("SacoHombros")] 
        public decimal? SacoHombros { get; set; }

        [Column("SacoPecho")] 
        public decimal? SacoPecho { get; set; }

        [Column("SacoEstomago")] 
        public decimal? SacoEstomago { get; set; }

        [Column("SacoMangaIzq")] 
        public decimal? SacoMangaIzq { get; set; }

        [Column("SacoMangaDer")] 
        public decimal? SacoMangaDer { get; set; }

        [Column("SacoBiceps")] 
        public decimal? SacoBiceps { get; set; }

        [Column("SacoCadera")] 
        public decimal? SacoCadera { get; set; }

        [Column("PantLargoIzq")] 
        public decimal? PantLargoIzq { get; set; }

        [Column("PantLargoDer")] 
        public decimal? PantLargoDer { get; set; }

        [Column("PantCintura")] 
        public decimal? PantCintura { get; set; }

        [Column("PantCadera")] 
        public decimal? PantCadera { get; set; }

        [Column("PantMuslo")] 
        public decimal? PantMuslo { get; set; }

        [Column("PantTiro")] 
        public decimal? PantTiro { get; set; }

        [Column("CamisaCuello")] 
        public decimal? CamisaCuello { get; set; }

        [Column("CamisaManga")] 
        public decimal? CamisaManga { get; set; }

        [ForeignKey("IdOrden")]
        [JsonIgnore]
        public virtual Orden? Orden { get; set; }
    }
}
