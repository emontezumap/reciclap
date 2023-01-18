namespace DTOs;

public class EstadoDTO
{
    public Guid? Id { get; set; } = null;
    public string? Nombre { get; set; } = null;
    public Guid? IdPais { get; set; } = null;
    public bool? Activo { get; set; } = null;
}