using System.ComponentModel.DataAnnotations;

namespace API_Hantonio_Jaramillo.DTOs
{
    public class FinanzaCreateDTO
    {
        [Required(ErrorMessage = "La descripcion es obligatoria")]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        public decimal GananciaNeta { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El costo debe ser positivo")]
        public decimal CostoInversion { get; set; }

        [Required]
        public decimal ROI { get; set; }
    }
}
