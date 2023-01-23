namespace Herramientas;

public class ResultadoValidacion
{
    public bool ValidacionOk { get; set; } = false;
    public Dictionary<string, List<string>>? Mensajes { get; set; }
}