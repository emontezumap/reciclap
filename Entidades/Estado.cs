using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades;

[Table("estados")]
public class Estado
{
    [Column("id")]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Column("nombre")]
    [MaxLength(100)]
    public string Nombre { get; set; } = "";
    [Column("id_pais")]
    public Guid IdPais { get; set; }

    public virtual ICollection<Ciudad>? Ciudades { get; set; }
}