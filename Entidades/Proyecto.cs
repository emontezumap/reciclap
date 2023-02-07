using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entidades;

[Table("proyectos")]
public class Proyecto
{
    [Column("id")]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Column("titulo")]
    public string Titulo { get; set; } = "";
    [Column("descripcion")]
    public string Descripcion { get; set; } = "";
    [Column("fecha_inicio")]
    public DateTime FechaInicio { get; set; }
    [Column("id_gerente")]
    public Guid IdGerente { get; set; }
    [Column("id_revisor")]
    public Guid IdRevisor { get; set; }
    [Column("gustan")]
    public int Gustan { get; set; }
    [Column("no_gustan")]
    public int NoGustan { get; set; }
    [Column("id_estatus_publicacion")]
    public Guid IdEstatusPublicacion { get; set; }
    [Column("id_estatus_proyecto")]
    public int IdEstatusProyecto { get; set; }     // 0 = En espera de aprobacion, 10 = Aprobado, 20 = Iniciado, 30 = Con observaciones, 100 = Cerrado, 200 = Vencido, 900 = Rechazado, etc.
    [Column("id_revisada_por")]
    public Guid IdRevisadaPor { get; set; }
    [Column("id_imagen_principal")]
    public Guid IdImagenPrincipal { get; set; }
    [Column("id_tipo_proyecto")]
    public Guid IdTipoProyecto { get; set; }
    [Column("tiempo_estimado")]
    public int TiempoEstimado { get; set; }
    [Column("progreso_estimado")]
    public int ProgresoEstimado { get; set; }
    [Column("progreso_real")]
    public int ProgresoReal { get; set; }
    [Column("evaluacion")]
    public decimal Evaluacion { get; set; }
    [Column("id_ruta_proyecto")]
    public int IdRutaProyecto { get; set; }
    [Column("fase_actual")]
    public int FaseActual { get; set; }
    [Column("fase_anterior")]
    public int FaseAnterior { get; set; }
    [Column("fase_siguiente")]
    public int FaseSiguiente { get; set; }
    [Column("fecha_disponible")]
    public DateTime FechaDisponible { get; set; }
    [Column("total_articulos")]
    public decimal TotalArticulos { get; set; }
    [Column("costo_estimado")]
    public decimal CostoEstimado { get; set; }
    [Column("moneda_costo_estimado")]
    public string MonedaCostoEstimado { get; set; } = "";
    [Column("tipo_cambio_costo_estimado")]
    public decimal TipoCambioCostoEstimado { get; set; }
    [Column("costo_real")]
    public decimal CostoReal { get; set; }
    [Column("moneda_costo_real")]
    public string MonedaCostoReal { get; set; } = "";
    [Column("tipo_cambio_costo_real")]
    public decimal TipoCambioCostoReal { get; set; }
    [Column("id_creador")]
    public Guid IdCreador { get; set; }
    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    [Column("id_modificador")]
    public Guid IdModificador { get; set; }
    [Column("fecha_modificacion")]
    public DateTime FechaModificacion { get; set; } = DateTime.UtcNow;
    [Column("activo")]
    public bool? Activo { get; set; } = true;

    [JsonIgnore]
    public virtual Usuario? Gerente { get; set; }
    [JsonIgnore]
    public virtual Usuario? Revisor { get; set; }
    [JsonIgnore]
    public virtual RegistroGeneral? EstatusPublicacion { get; set; }
    [JsonIgnore]
    public virtual RegistroGeneral? EstatusProyecto { get; set; }
    [JsonIgnore]
    public virtual RegistroGeneral? TipoProyecto { get; set; }
    [JsonIgnore]
    public virtual RegistroGeneral? RutaProyecto { get; set; }
    [JsonIgnore]
    public virtual Usuario? RevisadaPor { get; set; }
    [JsonIgnore]
    public virtual RecursoPublicacion? ImagenPrincipal { get; set; }
    [JsonIgnore]
    public virtual Usuario? Creador { get; set; }
    [JsonIgnore]
    public virtual Usuario? Modificador { get; set; }
    [JsonIgnore]
    public virtual ICollection<ActividadProyecto>? ActividadesProyecto { get; set; }
}