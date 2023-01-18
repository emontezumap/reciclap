namespace DTOs;

public class RolDTO
{
    public Guid? Id { get; set; } = null;
    public string? Descripcion { get; set; } = null;
    public bool? EsCreador { get; set; } = null;
    public bool? Activo { get; set; } = null;
}