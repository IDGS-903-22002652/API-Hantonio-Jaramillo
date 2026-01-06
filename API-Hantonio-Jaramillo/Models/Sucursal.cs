using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Hantonio_Jaramillo.Models;

[Table("SUCURSAL")]
public class Sucursal
{
    [Key]
    [Column("id_sucursal")]
    public int IdSucursal { get; set; }

    [Column("nombre")]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Column("direccion")]
    public string? Direccion { get; set; }

    [Column("telefono")]
    [MaxLength(20)]
    public string? Telefono { get; set; }

    [Column("encargado")]
    [MaxLength(100)]
    public string? Encargado { get; set; }

    [Column("activa")]
    public bool Activa { get; set; } = true;

    // Navegación
    public ICollection<Usuario> Usuarios { get; set; } = [];
    public ICollection<Orden> Ordenes { get; set; } = [];
}