using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Hantonio_Jaramillo.Models;

[Table("USUARIO")]
public class Usuario
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("id_rol")]
    public int? IdRol { get; set; }

    [Column("id_sucursal")]
    public int? IdSucursal { get; set; }

    [Column("nombre_completo")]
    [MaxLength(150)]
    public string? NombreCompleto { get; set; }

    [Column("login")]
    [MaxLength(50)]
    public string Login { get; set; } = string.Empty;

    [Column("password_hash")]
    [MaxLength(255)]
    public string PasswordHash { get; set; } = string.Empty;

    [Column("activo")]
    public bool Activo { get; set; } = true;

    [Column("ultimo_login")]
    public DateTime? UltimoLogin { get; set; }

    [Column("ultimo_logout")]
    public DateTime? UltimoLogout { get; set; }

    // Navegación
    [ForeignKey("IdRol")]
    public Rol? Rol { get; set; }

    [ForeignKey("IdSucursal")]
    public Sucursal? Sucursal { get; set; }

    public ICollection<LogAcceso> LogAccesos { get; set; } = [];
    public ICollection<Orden> OrdenesCreadas { get; set; } = [];
}   