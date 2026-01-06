using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Hantonio_Jaramillo.Models;

[Table("CAT_RECURSOS_DISENO")]
public class CatRecursosDiseno
{
    [Key]
    [Column("id_recurso")]
    public int IdRecurso { get; set; }

    [Column("prenda")]
    [MaxLength(50)]
    public string Prenda { get; set; } = string.Empty;

    [Column("atributo")]
    [MaxLength(50)]
    public string Atributo { get; set; } = string.Empty;

    [Column("codigo_valor")]
    [MaxLength(50)]
    public string CodigoValor { get; set; } = string.Empty;

    [Column("nombre_mostrar")]
    [MaxLength(100)]
    public string? NombreMostrar { get; set; }

    [Column("url_imagen")]
    [MaxLength(255)]
    public string? UrlImagen { get; set; }
}