using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entidades;

public class RecursoPublicacion
{
    [Column("id")]
    [Key]
    public Guid Id { get; set; }
    [Column("id_tipo_catalogo")]
    public Guid IdTipoCatalogo { get; set; }   // Publicacion o Proyecto
    [Column("id_padre")]
    public Guid IdPadre { get; set; }
    [Column("secuencia")]
    public int IdSecuencia { get; set; }
    [Column("id_tipo_recurso")]
    public Guid IdTipoRecurso { get; set; }
    [Column("fecha")]
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    [Column("ruta")]
    public Guid Ruta { get; set; }    // Id del usuario que lo movió a este estado
    [Column("orden")]
    public int Orden { get; set; }    // Orden del recurso en la vista del usuario
    [Column("nombre")]
    public string Nombre { get; set; } = "";
    [Column("id_estatus_recurso")]
    public Guid IdEstatusRecurso { get; set; }  // 0 = En proceso de aprobacion, 10 = Aprobado, 20 = No mostrar, etc.
    [Column("fecha_expiracion")]
    public DateTime FechaExpiracion { get; set; }
    [Column("tamano")]
    public int Tamaño { get; set; } = 0;

    [JsonIgnore]
    public virtual TipoCatalogo? TipoCatalogo { get; set; }

    [JsonIgnore]
    public virtual Secuencia? Secuencia { get; set; }

    [JsonIgnore]
    public virtual TipoRecurso? TipoRecurso { get; set; }

    [JsonIgnore]
    public virtual EstatusRecurso? EstatusRecurso { get; set; }

    [JsonIgnore]
    public virtual Usuario? Padre { get; set; }

    [JsonIgnore]
    public virtual Secuencia? Serie { get; set; }
}
