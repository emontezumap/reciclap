namespace DTOs;

public class VersionApiDTO
{
	public int? Id { get; set; } = null;
	public string? Version { get; set; } = null;
	public DateTime? VigenteDesde { get; set; } = null;
	public bool? Activo { get; set; } = null;
}
