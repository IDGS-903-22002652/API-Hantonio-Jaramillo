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
        [Column("TipoFit")][MaxLength(50)] public string? TipoFit { get; set; }
        [Column("CollarSaco")]
        public decimal? CollarSaco { get; set; }
        [Column("LongitudFrontalSaco")]
        public decimal? LongitudFrontalSaco { get; set; }
        [Column("LongitudEspaldaSaco")]
        public decimal? LongitudEspaldaSaco { get; set; }
        [Column("HombrosSaco")]
        public decimal? HombrosSaco { get; set; }
        [Column("PechoSaco")]
        public decimal? PechoSaco { get; set; }
        [Column("PechoDelanteroSaco")]
        public decimal? PechoDelanteroSaco { get; set; }
        [Column("EstomagoSaco")]
        public decimal? EstomagoSaco { get; set; }
        [Column("VientreSaco")]
        public decimal? VientreSaco { get; set; }
        [Column("CaderasSaco")]
        public decimal? CaderasSaco { get; set; }
        [Column("LongitudMangaISaco")]
        public decimal? LongitudMangaISaco { get; set; }
        [Column("LongitudMangaDSaco")]
        public decimal? LongitudMangaDSaco { get; set; }
        [Column("BicepsSaco")]
        public decimal? BicepsSaco { get; set; }
        [Column("AntebrazoSaco")]
        public decimal? AntebrazoSaco { get; set; }
        [Column("MuñecaSaco")]
        public decimal? MuñecaSaco { get; set; }
        [Column("HombroDelanteroSaco")]
        public decimal? HombroDelanteroSaco { get; set; }
        [Column("AnchoTraseroSaco")]
        public decimal? AnchoTraseroSaco { get; set; }
        [Column("NucaCinturaSaco")]
        public decimal? NucaCinturaSaco { get; set; }
        [Column("LongitudCinturaDelanteraSaco")]
        public decimal? LongitudCinturaDelantera { get; set; }
        [Column("PosicionPrimerBSaco")]
        public decimal? PosicionPrimerBSaco { get; set; }
        [Column("CollarCamisa")]
        public decimal? CollarCamisa { get; set; }
        [Column("LongitudFrontalCamisa")]
        public decimal? LongitudFrontalCamisa { get; set; }
        [Column("LongitudEspaldaCamisa")]
        public decimal? LongitudEspaldaCamisa { get; set; }
        [Column("HombrosCamisa")]
        public decimal? HombrosCamisa { get; set; }
        [Column("PechoCamisa")]
        public decimal? PechoCamisa { get; set; }
        [Column("PechoDelanteroCamisa")]
        public decimal? PechoDelanteroCamisa { get; set; }
        [Column("EstomagoCamisa")]
        public decimal? EstomagoCamisa { get; set; }
        [Column("VientreCamisa")]
        public decimal? VientreCamisa { get; set; }
        [Column("CaderasCamisa")]
        public decimal? CaderasCamisa { get; set; }
        [Column("LongitudMangaICamisa")]
        public decimal? LongitudMangaICamisa { get; set; }
        [Column("LongitudMangaDCamisa")]
        public decimal? LongitudMangaDCamisa { get; set; }
        [Column("BicepsCamisa")]
        public decimal? BicepsCamisa { get; set; }
        [Column("AntebrazoCamisa")]
        public decimal? AntebrazoCamisa { get; set; }
        [Column("MuñecaCamisa")]
        public decimal? MuñecaCamisa { get; set; }
        [Column("HombroDelanteroCamisa")]
        public decimal? HombroDelanteroCamisa { get; set; }
        [Column("AnchoTraseroCamisa")]
        public decimal? AnchoTraseroCamisa { get; set; }
        [Column("NucaCinturaCamisa")]
        public decimal? NucaCinturaCamisa { get; set; }
        [Column("LongitudCinturaDelanteraCamisa")]
        public decimal? LongitudCinturaDelanteraCamisa { get; set; }
        [Column("PosicionPrimerBCamisa")]
        public decimal? PosicionPrimerBCamisa { get; set; }
        [Column("LongitudIPantalon")]
        public decimal? LongitudIPantalon { get; set; }
        [Column("LongitudDPantalon")]
        public decimal? LongitudDPantalon { get; set; }
        [Column("CinturaPantalon")]
        public decimal? CinturaPantalon { get; set; }
        [Column("CaderaPantalon")]
        public decimal? CaderaPantalon { get; set; }
        [Column("MusloPantalon")]
        public decimal? MusloPantalon { get; set; }
        [Column("RodillaPantalon")]
        public decimal? RodillaPantalon { get; set; }
        [Column("AlTerrillaPantalon")]
        public decimal? AlTerrillaPantalon { get; set; }
        [Column("BrazaletePantalon")]
        public decimal? BrazaletePantalon { get; set; }
        [Column("EntrepiernaPantalon")]
        public decimal? EntrepiernaPantalon { get; set; }
        [Column("AlturaCinturaTPantalon")]
        public decimal? AlturaCinturaTPantalon { get; set; }
        [Column("AlturaCinturaDPantalon")]
        public decimal? AlturaCinturaDPantalon { get; set; }
        [Column("CollarChaleco")]
        public decimal? CollarChaleco { get; set; }
        [Column("LongitudFrontalChaleco")]
        public decimal? LongitudFrontalChaleco { get; set; }
        [Column("LongitudEspaldaChaleco")]
        public decimal? LongitudEspaldaChaleco { get; set; }
        [Column("PechoChaleco")]
        public decimal? PechoChaleco { get; set; }
        [Column("PechoDelanteroChaleco")]
        public decimal? PechoDelanteroChaleco { get; set; }
        [Column("EstomagoChaleco")]
        public decimal? EstomagoChaleco { get; set; }
        [Column("VientreChaleco")]
        public decimal? VientreChaleco { get; set; }
        [Column("CaderasChaleco")]
        public decimal? CaderasChaleco { get; set; }
        [Column("TamañoInferiorChaleco")]
        public decimal? TamañoInferiorChaleco { get; set; }
        [Column("LongitudCinturaDChaleco")]
        public decimal? LongitudCinturaDChaleco { get; set; }
        [Column("NucaCinturaChaleco")]
        public decimal? NucaCinturaChaleco { get; set; }
        [Column("PosicionPrimerBChaleco")]
        public decimal? PosicionPrimerBChaleco { get; set; }
        [Column("TallaZapato")]
        public decimal? TallaZapato { get; set; }
        [Column("AnchoEmpeineZapato")]
        public decimal? AnchoEmpeineZapato { get; set; }
        [Column("LargoPieZapato")]
        public decimal? LargoPieZapato { get; set; }
        [ForeignKey("IdOrden")]
        [JsonIgnore]
        public virtual Orden? Orden { get; set; }
    }
}
