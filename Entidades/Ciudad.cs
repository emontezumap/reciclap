using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades;

[Table("ciudades")]
public class Ciudad
{
    [Column("id")]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Column("nombre")]
    [MaxLength(100)]
    public string Nombre { get; set; } = "";
    [Column("id_estado")]
    public Guid IdEstado { get; set; }

    public virtual ICollection<Usuario>? Usuarios { get; set; }
}