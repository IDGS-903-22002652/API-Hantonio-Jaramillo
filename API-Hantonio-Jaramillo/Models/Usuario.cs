using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_Hantonio_Jaramillo.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        [Column("IdRol")]
        public int IdRol { get; set; }

        [Column("IdSucursal")]
        public int? IdSucursal { get; set; }

        [Required]
        [Column("NombreCompleto")]
        [MaxLength(150)]
        public string NombreCompleto { get; set; } = string.Empty;

        [Column("NombreUsuario")]
        [MaxLength(50)]
        public string NombreUsuario { get; set; } = string.Empty;

        [Column("Email")]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Column("PasswordHash")]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Column("Estatus")]
        public bool Estatus { get; set; } = true;

        [Column("UltimoLogin")]
        public DateTime? UltimoLogin { get; set; }

        [Column("UltimoLogout")]
        public DateTime? UltimoLogout { get; set; }

        [ForeignKey("IdRol")]
        [JsonIgnore]
        public virtual Rol? Rol { get; set; }

        [ForeignKey("IdSucursal")]
        [JsonIgnore] 
        public virtual Sucursal? Sucursal { get; set; }
    }
}
