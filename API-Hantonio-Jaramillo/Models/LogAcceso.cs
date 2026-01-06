using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Hantonio_Jaramillo.Models;

[Table("LOG_ACCESOS")]
public class LogAcceso
{
    [Key]
    [Column("id_log")]
    public int IdLog { get; set; }

    [Column("id_usuario")]
    public int? IdUsuario { get; set; }

    [Column("fecha_ingreso")]
    public DateTime FechaIngreso { get; set; } = DateTime.Now;

    [Column("fecha_salida")]
    public DateTime? FechaSalida { get; set; }

    // Navegación
    [ForeignKey("IdUsuario")]
    public Usuario? Usuario { get; set; }
}