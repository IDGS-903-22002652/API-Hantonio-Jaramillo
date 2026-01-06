using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Hantonio_Jaramillo.Models;

[Table("CAT_TIPO_TRAJE")]
public class CatTipoTraje
{
    [Key]
    [Column("id_tipo_traje")]
    public int IdTipoTraje { get; set; }

    [Column("descripcion")]
    [MaxLength(50)]
    public string Descripcion { get; set; } = string.Empty;

    // Navegación
    public ICollection<Orden> Ordenes { get; set; } = [];
}