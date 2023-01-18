namespace DTOs;

public class CiudadDTO
{
    public Guid? Id { get; set; } = null;
    public string? Nombre { get; set; } = null;
    public Guid? IdEstado { get; set; } = null;
    public bool? Activo { get; set; } = null;
}