using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entidades;

[Table("actividades_proyectos")]
public class ActividadProyecto
{
    [Column("id")]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Column("id_proyecto")]
    public Guid IdProyecto { get; set; }
    [Column("id_ruta_proyecto")]
    public int IdRutaProyecto { get; set; }
    [Column("secuencia")]
    public int Secuencia { get; set; } = 1;
    [Column("id_actividad_ruta")]
    public Guid IdActividadRuta { get; set; }
    [Column("descripcion")]
    public string Descripcion { get; set; } = "";
    [Column("fecha_inicio")]
    public DateTime FechaInicio { get; set; }
    [Column("fecha_finalizacion")]
    public DateTime FechaFinalizacion { get; set; }
    [Column("id_ejecutor")]
    public Guid IdEjecutor { get; set; }
    [Column("id_revisor")]
    public Guid IdRevisor { get; set; }
    [Column("id_estatus_publicacion")]
    public Guid IdEstatusPublicacion { get; set; }
    [Column("id_estatus_proyecto")]
    public int IdEstatusProyecto { get; set; }
    [Column("id_revisada_por")]
    public Guid IdRevisadaPor { get; set; }
    [Column("id_tipo_actividad")]
    public int IdTipoActividad { get; set; }
    [Column("tiempo_estimado")]
    public int TiempoEstimado { get; set; }
    [Column("progreso_estimado")]
    public int ProgresoEstimado { get; set; }
    [Column("evaluacion")]
    public decimal Evaluacion { get; set; }
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
    public virtual RegistroGeneral? RutaProyecto { get; set; }
    [JsonIgnore]
    public virtual ActividadRutaProyecto? ActividadRuta { get; set; }
    [JsonIgnore]
    public virtual RegistroGeneral? EstatusPublicacion { get; set; }
    [JsonIgnore]
    public virtual RegistroGeneral? EstatusProyecto { get; set; }
    [JsonIgnore]
    public virtual Proyecto? Proyecto { get; set; }
    [JsonIgnore]
    public virtual Usuario? Ejecutor { get; set; }
    [JsonIgnore]
    public virtual Usuario? Revisor { get; set; }
    [JsonIgnore]
    public virtual Usuario? RevisadaPor { get; set; }
    [JsonIgnore]
    public virtual RegistroGeneral? TipoActividad { get; set; }
    [JsonIgnore]
    public virtual Usuario? Creador { get; set; }
    [JsonIgnore]
    public virtual Usuario? Modificador { get; set; }
}
