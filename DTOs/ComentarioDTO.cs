namespace Entidades;

public class ComentarioDTO
{
    public Guid? Id { get; set; } = null;
    public Guid? IdChat { get; set; } = null;
    public Guid? IdUsuario { get; set; } = null;
    public DateTime? Fecha { get; set; } = null;
    public string? Texto { get; set; } = null;
    public Guid? IdComentario { get; set; } = null;
    public bool? Activo { get; set; } = null;
}