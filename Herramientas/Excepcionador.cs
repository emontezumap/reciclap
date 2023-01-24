using Newtonsoft.Json;

namespace Herramientas;

public class Excepcionador
{
    private readonly ResultadoValidacion? rv;
    private const string MENSAJE_ERROR_POR_DEFECTO = "Error no especificado";
    private const string MENSAJE_ERROR_REGISTRO_ELIMINADO = "El registro fue eliminado";
    private const string MENSAJE_ERROR_DATOS_NO_VALIDOS = "Datos no válidos";

    public Excepcionador() { }

    public Excepcionador(ResultadoValidacion rv)
    {
        this.rv = rv;
    }

    public GraphQLException ExcepcionRegistroEliminado()
    {
        var error = ErrorBuilder.New()
            .SetMessage(MENSAJE_ERROR_REGISTRO_ELIMINADO)
            .SetCode("REGISTRO_ELIMINADO")
            .Build();

        return new GraphQLException(error);
    }

    public GraphQLException ExcepcionDatosNoValidos()
    {
        var error = ErrorBuilder.New();
        error.SetMessage(MENSAJE_ERROR_DATOS_NO_VALIDOS);
        error.SetCode("DATOS_NO_VALIDOS");

        foreach (var men in rv!.Mensajes!)
            if (men.Value.Count > 0)
                error.SetExtension(men.Key, JsonConvert.SerializeObject(men.Value));

        return new GraphQLException(error.Build());
    }
}