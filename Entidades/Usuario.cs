using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades;

[Table("usuarios")]
public class Usuario
{
    [Column("id")]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Column("nombre")]
    [MaxLength(50)]
    public string Nombre { get; set; } = "";
    [Column("segundo_nombre")]
    [MaxLength(50)]
    public string Nombre2 { get; set; } = "";
    [Column("apellido")]
    [MaxLength(50)]
    public string Apellido { get; set; } = "";
    [Column("segundo_apellido")]
    [MaxLength(50)]
    public string Apellido2 { get; set; } = "";
    [Column("perfil")]
    [MaxLength(200)]
    public string Perfil { get; set; } = "";
    [Column("direccion")]
    [MaxLength(300)]
    public string Direccion { get; set; } = "";
    [Column("id_ciudad")]
    public Guid IdCiudad { get; set; }
    [Column("telefono")]
    [MaxLength(20)]
    public string Telefono { get; set; } = "";
    [Column("telefono2")]
    [MaxLength(20)]
    public string Telefono2 { get; set; } = "";
    [Column("email")]
    [EmailAddress]
    [MaxLength(250)]
    public string Email { get; set; } = "";
    [Column("email2")]
    [MaxLength(250)]
    public string Email2 { get; set; } = "";
    [Column("id_profesion")]
    public Guid IdProfesion { get; set; }
    [Column("max_publicaciones")]
    public int MaximoPublicaciones { get; set; } = 0;
    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Comentario>? Comentarios { get; set; }
    public virtual ICollection<Personal>? UsuariosLink { get; set; }
}
