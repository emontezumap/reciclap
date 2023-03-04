namespace DTOs;

public class MonedaDTO
{
	public string? Id { get; set; } = null;
	public string? Nombre { get; set; } = null;
	public decimal? TipoCambio { get; set; } = null;
	public bool? EsLocal { get; set; } = null;
	public bool? Activo { get; set; } = null;
}
