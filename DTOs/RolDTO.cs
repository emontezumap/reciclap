namespace DTOs;

public class Rol
{
    public Guid? Id { get; set; } = null;
    public string? Descripcion { get; set; } = null;
    public bool? EsCreador { get; set; } = null;
    public bool? Activo { get; set; } = null;
}