using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_Hantonio_Jaramillo.Models
{
    [Table("TipoTraje")]
    public class TipoTraje
    {
        [Key]
        [Column("IdTipoTraje")]
        public int IdTipoTraje { get; set; }

        [Required]
        [Column("Descripcion")]
        [MaxLength(50)]
        public string Descripcion { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Orden> Ordenes { get; set; } = [];
    }
}
