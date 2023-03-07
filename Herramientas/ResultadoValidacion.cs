namespace Herramientas;

public class ResultadoValidacion
{
    public bool ValidacionOk { get; set; } = false;
    public Dictionary<string, HashSet<string>>? Mensajes { get; set; }
}