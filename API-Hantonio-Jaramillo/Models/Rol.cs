using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Hantonio_Jaramillo.Models;

[Table("ROL")]
public class Rol
{
    [Key]
    [Column("id_rol")]
    public int IdRol { get; set; }

    [Column("nombre")]
    [MaxLength(50)]
    public string Nombre { get; set; } = string.Empty;

    // Navegación
    public ICollection<Usuario> Usuarios { get; set; } = [];
}