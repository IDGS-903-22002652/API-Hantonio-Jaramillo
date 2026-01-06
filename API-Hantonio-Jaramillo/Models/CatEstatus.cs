using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Hantonio_Jaramillo.Models;

[Table("CAT_ESTATUS")]
public class CatEstatus
{
    [Key]
    [Column("id_estatus")]
    public int IdEstatus { get; set; }

    [Column("descripcion")]
    [MaxLength(50)]
    public string Descripcion { get; set; } = string.Empty;

    // Navegación
    public ICollection<Orden> Ordenes { get; set; } = [];
}