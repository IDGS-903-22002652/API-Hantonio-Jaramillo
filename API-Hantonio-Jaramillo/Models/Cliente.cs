using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Hantonio_Jaramillo.Models;

[Table("CLIENTE")]
public class Cliente
{
    [Key]
    [Column("id_cliente")]
    public int IdCliente { get; set; }

    [Column("nombre_completo")]
    [MaxLength(150)]
    public string NombreCompleto { get; set; } = string.Empty;

    [Column("telefono")]
    [MaxLength(20)]
    public string? Telefono { get; set; }

    [Column("email")]
    [MaxLength(100)]
    public string? Email { get; set; }

    [Column("ciudad")]
    [MaxLength(50)]
    public string? Ciudad { get; set; }

    [Column("estado")]
    [MaxLength(50)]
    public string? Estado { get; set; }

    [Column("fecha_registro")]
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    // Navegación
    public ICollection<Orden> Ordenes { get; set; } = [];
}