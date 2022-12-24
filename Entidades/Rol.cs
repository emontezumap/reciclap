using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades;

[Table("roles")]
public class Rol
{
    [Column("id")]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Column("descripcion")]
    [MaxLength(200)]
    public string Descripcion { get; set; } = "";
    [Column("creador")]
    public bool Creador { get; set; } = false;

    public virtual ICollection<Personal>? Asignados { get; set; }
}