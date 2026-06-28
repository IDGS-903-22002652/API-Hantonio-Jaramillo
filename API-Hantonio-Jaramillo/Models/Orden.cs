using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Hantonio_Jaramillo.Models
{
    [Table("Orden")]
    public class Orden
    {
        [Key]
        [Column("IdOrden")]
        public int IdOrden { get; set; }

        [Column("IdCliente")]
        public int IdCliente { get; set; }

        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        [Column("IdSucursal")]
        public int IdSucursal { get; set; }

        [Column("IdTipoTraje")]
        public int? IdTipoTraje { get; set; }
        [Column("IncluyeCamisa")]
        public bool IncluyeCamisa { get; set; }
        [Column("IncluyeZapato")]
        public bool IncluyeZapato { get; set; }
        [Column("esSmoking3Piezas")]
        public bool esSmoking3Piezas { get; set; }

        [Column("IdEstatus")]
        public int IdEstatus { get; set; }

        [Column("FechaCreacion")]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [Column("FechaCitaMedidas")]
        public DateTime? FechaCitaMedidas { get; set; }

        [Column("FechaEntrega")]
        public DateTime? FechaEntrega { get; set; }

        [Column("FechaEventoEntrega")]
        public DateTime? FechaEventoEntrega { get; set; }

        [Column("CostoTotal")]
        public decimal CostoTotal { get; set; }

        [Column("MontoAbonado")]
        public decimal MontoAbonado { get; set; }

        [Column("MetodoPago")]
        public string? MetodoPago { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Usuario? Usuario { get; set; }

        [ForeignKey("IdCliente")]
        public virtual Cliente? Cliente { get; set; }

        [ForeignKey("IdSucursal")]
        public virtual Sucursal? Sucursal { get; set; }

        [ForeignKey("IdEstatus")]
        public virtual EstatusOrden? EstatusOrden { get; set; }

        [ForeignKey("IdTipoTraje")]
        public virtual TipoTraje? TipoTraje { get; set; }

        [InverseProperty("Orden")]
        public virtual MedidasOrden? Medidas { get; set; }
        [InverseProperty("Orden")]
        public virtual DetalleSaco? DetalleSaco { get; set; }
        [InverseProperty("Orden")]
        public virtual DetallePantalon? DetallePantalon { get; set; }
        [InverseProperty("Orden")]
        public virtual DetalleChaleco? DetalleChaleco { get; set; }
        [InverseProperty("Orden")]
        public virtual DetalleCamisa? DetalleCamisa { get; set; }
        [InverseProperty("Orden")]
        public virtual DetalleZapato? DetalleZapato { get; set; }
    }
}
