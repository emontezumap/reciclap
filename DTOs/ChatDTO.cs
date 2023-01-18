namespace DTOs;

public class ChatDTO
{
    public Guid? Id { get; set; } = null;
    public Guid? IdPublicacion { get; set; } = null;
    public string? Titulo { get; set; } = null;
    public DateTime? Fecha { get; set; } = null;
    public bool? Activo { get; set; } = null;
}