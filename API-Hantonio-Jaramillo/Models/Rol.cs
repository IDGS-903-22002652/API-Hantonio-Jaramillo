using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_Hantonio_Jaramillo.Models
{
    [Table("Rol")]
    public class Rol
    {
        [Key]
        [Column("IdRol")]
        public int IdRol { get; set; }

        [Required]
        [Column("Nombre")]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;
        // Navegación
        [JsonIgnore]
        public ICollection<Usuario> Usuarios { get; set; } = [];
    }
}
