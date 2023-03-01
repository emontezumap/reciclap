namespace Herramientas;

public class ResultadoValidacion
{
    public bool ValidacionOk { get; set; } = false;
    public Dictionary<string, HashSet<Error>>? Mensajes { get; set; }
}