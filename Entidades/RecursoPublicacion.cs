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
    [Column("id_catalogo")]
    public Guid IdCatalogo { get; set; }        // Id de publicacion o proyecto
    [Column("id_secuencia")]
    public int IdSecuencia { get; set; }
    [Column("id_tipo_recurso")]
    public int IdTipoRecurso { get; set; }
    [Column("fecha")]
    public DateTime Fecha { get; set; } = DateTime.UtcNow;  // Fecha creacion recurso
    [Column("id_usuario")]
    public Guid IdUsuario { get; set; }    // Id del usuario que lo movió a este estado
    [Column("orden")]
    public int Orden { get; set; } = 1;    // Orden del recurso en la vista del usuario
    [Column("nombre")]
    public string Nombre { get; set; } = "";    // Nombre visible del recurso
    [Column("id_estatus_recurso")]
    public int IdEstatusRecurso { get; set; }  // 0 = En proceso de aprobacion, 10 = Aprobado, 20 = No mostrar, etc.
    [Column("fecha_expiracion")]
    public DateTime? FechaExpiracion { get; set; } = null;
    [Column("tamano")]
    public long Tamaño { get; set; } = 0;
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
    public virtual RegistroGeneral? TipoCatalogo { get; set; }
    [JsonIgnore]
    public virtual Secuencia? Secuencia { get; set; }
    [JsonIgnore]
    public virtual RegistroGeneral? TipoRecurso { get; set; }
    [JsonIgnore]
    public virtual Usuario? Usuario { get; set; }
    [JsonIgnore]
    public virtual RegistroGeneral? EstatusRecurso { get; set; }
    [JsonIgnore]
    public virtual Usuario? Creador { get; set; }
    [JsonIgnore]
    public virtual Usuario? Modificador { get; set; }
}
