using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class GeneralRegistros
{
    [Column("Id")]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Column("tabla")]
    [MaxLength(50)]
    public string Tabla { get; set; } = "";
    [Column("descripcion")]
    public string Descripcion { get; set; } = "";
    [Column("referencia")]
    [MaxLength(50)]
    public string Referencia { get; set; } = "";
}
