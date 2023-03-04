namespace DTOs;

public class BitacoraProyectoDTO
{
	public long? Id { get; set; } = null;
	public Guid? IdProyecto { get; set; } = null;
	public long? IdActividadProyecto { get; set; } = null;
	public DateTime? Fecha { get; set; } = null;
	public Guid? IdUsuario { get; set; } = null;
	public int? IdTipoBitacora { get; set; } = null;
	public string? Comentarios { get; set; } = null;
	public bool? Activo { get; set; } = null;
}
