namespace DTOs;

public class PublicacionDTO
{
    public Guid? Id { get; set; } = null;
    public string? Titulo { get; set; } = null;
    public string? Descripcion { get; set; } = null;
    public DateTime? Fecha { get; set; } = null;
    public int? Gustan { get; set; } = null;
    public int? NoGustan { get; set; } = null;
    public Guid? IdEstatus { get; set; } = null;
    public Guid? IdTipoPublicacion { get; set; } = null;
    public bool? Activo { get; set; } = null;
}
