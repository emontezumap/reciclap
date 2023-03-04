namespace Herramientas;

public class ResultadoValidacion
{
    public bool ValidacionOk { get; set; } = false;
    public Dictionary<string, HashSet<CodigosError>>? Mensajes { get; set; }
}