using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades;

[Table("tablas_internas")]
public class Interna
{
    [Column("id")]
    [Key]
    [MaxLength(10)]
    public string Id { get; set; } = "";
    [Column("descripcion")]
    public string Descripcion { get; set; } = "";
    [Column("referencia")]
    public string Referencia { get; set; } = "";
}