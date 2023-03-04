namespace DTOs;

public class RecursoPublicacionDTO
{
	public Guid? Id { get; set; } = null;
	public int? IdTipoCatalogo { get; set; } = null;
	public Guid? IdCatalogo { get; set; } = null;
	public int? Secuencia { get; set; } = null;
	public int? IdTipoRecurso { get; set; } = null;
	public DateTime? Fecha { get; set; } = null;
	public Guid? IdUsuario { get; set; } = null;
	public int? Orden { get; set; } = null;
	public string? Nombre { get; set; } = null;
	public int? IdEstatusRecurso { get; set; } = null;
	public DateTime? FechaExpiracion { get; set; } = null;
	public long? Tamano { get; set; } = null;
	public bool? Activo { get; set; } = null;
}
