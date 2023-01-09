namespace Contenedores;

public class Respuesta<T>
{
    public T? Datos { get; set; }
    public bool DatosOk { get; set; }
    public string[]? Errores { get; set; }
    public string Mensaje { get; set; }

    public Respuesta()
    {
        Mensaje = string.Empty;
        Errores = null;
        Datos = default(T);
    }

    public Respuesta(T datos) : this()
    {
        DatosOk = true;
        Datos = datos;
    }
}