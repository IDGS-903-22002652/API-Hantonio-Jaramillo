using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_Hantonio_Jaramillo.Models
{
    [Table("Cliente")]

    public class Cliente
    {
        [Key]
        [Column("IdCliente")]
        public int IdCliente { get; set; }

        [Required]
        [Column("NombreCompleto")]
        [MaxLength(150)]
        public string NombreCompleto { get; set; } = string.Empty;

        [Column("Telefono")]
        [MaxLength(20)]
        public string? Telefono { get; set; }

        [Column("Email")]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Column("Ciudad")]
        [MaxLength(50)]
        public string? Ciudad { get; set; }

        [Column("Estado")]
        [MaxLength(50)]
        public string? Estado { get; set; }

        [Column("FechaNacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        [Column("FechaRegistro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Column("Estatus")]
        public bool Estatus { get; set; } = true;

        [JsonIgnore]
        public ICollection<Orden> Ordenes { get; set; } = [];
    }
}
