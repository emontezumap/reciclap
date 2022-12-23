using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades;

[Table("estatus_publicaciones")]
public class EstatusPublicacion
{
    [Column("id")]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Column("descripcion")]
    [MaxLength(200)]
    public string Descripcion { get; set; } = "";

    public virtual ICollection<Publicacion>? Publicaciones { get; set; }
}
