using Newtonsoft.Json;

namespace Herramientas;

public class Excepcionador
{
    private readonly ResultadoValidacion rv;

    public Excepcionador(ResultadoValidacion rv)
    {
        this.rv = rv;
    }

    public GraphQLException ExcepcionDatosNoValidos()
    {
        var error = ErrorBuilder.New()
            .SetMessage("Datos no válidos")
            .SetCode("DATOS_NO_VALIDOS")
            .SetExtension("validacion", JsonConvert.SerializeObject(rv, Formatting.Indented))
            .Build();

        return new GraphQLException(error);
    }
}