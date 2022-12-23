using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades;

[Table("profesiones")]
public class Profesion
{
    [Column("id")]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Column("descripcion")]
    [MaxLength(100)]
    public string Descripcion { get; set; } = "";

    public virtual ICollection<Usuario>? Usuarios { get; set; }
}