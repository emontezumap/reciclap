using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entidades;

[Table("administradores")]
public class Administrador
{
    [Column("id")]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Column("nombre")]
    [MaxLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres")]
    public string Nombre { get; set; } = "";
    [Column("telefono")]
    [Phone]
    [MaxLength(100, ErrorMessage = "El número telefónico no debe exceder los 100 caracteres")]
    public string Telefono { get; set; } = "";
    [Column("email")]
    [EmailAddress]
    [MaxLength(300, ErrorMessage = "La dirección de correo no debe exceder los 300 caracteres")]
    public string Email { get; set; } = "";
    [Column("clave")]
    public string Clave { get; set; } = "";
    [Column("fecha_registro")]
    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    [Column("id_grupo")]
    public Guid IdGrupo { get; set; }
    [Column("activo")]
    public bool? Activo { get; set; } = true;

    [JsonIgnore]
    public virtual Grupo? Grupo { get; set; }
}
