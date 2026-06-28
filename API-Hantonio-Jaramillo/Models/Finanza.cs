using System;
using System.ComponentModel.DataAnnotations;

namespace API_Hantonio_Jaramillo.Models
{
    public class Finanza
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Descripcion { get; set; } = string.Empty;

        public decimal GananciaNeta { get; set; }

        public decimal CostoInversion { get; set; }

        public decimal ROI { get; set; }

        // El servidor asignará esta fecha automáticamente
        public DateTime Fecha { get; set; }
    }
}
