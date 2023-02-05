using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades;

public class Secuencia
{
    [Column("id")]
    [Key]
    [MaxLength(10)]
    public string Id { get; set; } = "";
    [Column("prefijo")]
    [MaxLength(10)]
    public string Prefijo { get; set; } = "";
    [Column("serie")]
    public long Serie { get; set; } = 1;
    [Column("incremento")]
    public int Incremento { get; set; } = 1;
}