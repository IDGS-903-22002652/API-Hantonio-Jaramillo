using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_Hantonio_Jaramillo.Models
{
    [Table("EstatusOrden")]
    public class EstatusOrden
    {
        [Key]
        [Column("IdEstatus")]
        public int IdEstatus { get; set; }

        [Required]
        [Column("Descripcion")]
        [MaxLength(50)]
        public string Descripcion { get; set; } = string.Empty;

        // Navegación
        [JsonIgnore]
        public ICollection<Orden> Ordenes { get; set; } = [];
    }
}
