namespace DTOs;

public class GrupoDTO
{
    public Guid? Id { get; set; } = null;
    public string? Descripcion { get; set; } = null;
    public bool? EsAdministrador { get; set; } = null;
    public bool? Activo { get; set; } = null;
}