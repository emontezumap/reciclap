using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades;

[Table("paises")]
public class Pais
{
    [Column("id")]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Column("nombre")]
    [MaxLength(50)]
    public string Nombre { get; set; } = "";

    public virtual ICollection<Estado>? Estados { get; set; }
}