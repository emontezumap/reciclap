namespace DTOs;

public class PersonalDTO
{
    public Guid? IdPublicacion { get; set; } = null;
    public Guid? IdUsuario { get; set; } = null;
    public DateTime? Fecha { get; set; } = null;
    public Guid? IdRol { get; set; } = null;
    public bool? Activo { get; set; } = null;
}