using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_Hantonio_Jaramillo.Models;

[Table("Sucursal")]
public class Sucursal
{
    [Key]
    [Column("IdSucursal")]
    public int IdSucursal { get; set; }

    [Required] 
    [Column("Nombre")]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Column("Direccion")]
    public string? Direccion { get; set; }

    [Column("Telefono")]
    [MaxLength(20)]
    public string? Telefono { get; set; }

    [Column("IdUsuario")]
    public int? IdUsuario { get; set; }

    [ForeignKey("IdUsuario")]
    public virtual Usuario? Usuario { get; set; }

    [Column("Estatus")]
    public bool Estatus { get; set; } = true;

    [JsonIgnore] 
    public ICollection<Usuario> Usuarios { get; set; } = [];

    [JsonIgnore] 
    public ICollection<Orden> Ordenes { get; set; } = [];
}