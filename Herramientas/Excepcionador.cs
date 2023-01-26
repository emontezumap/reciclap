using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Herramientas;

public class Excepcionador
{
    private readonly ResultadoValidacion? rv;
    private const string MENSAJE_ERROR_POR_DEFECTO = "Error no especificado";
    private const string MENSAJE_ERROR_REGISTRO_ELIMINADO = "El registro fue eliminado";
    private const string MENSAJE_ERROR_DATOS_NO_VALIDOS = "Datos no válidos";
    private const string MENSAJE_ERROR_REGISTRO_DUPLICADO = "";
    public Excepcionador() { }

    public Excepcionador(ResultadoValidacion rv)
    {
        this.rv = rv;
    }

    public GraphQLException ProcesarExcepcionActualizacionDB(Exception? ex)
    {
        return ProcesarExcepcionActualizacionDB(ex, "");
    }

    public GraphQLException ProcesarExcepcionActualizacionDB(Exception? ex, string objeto)
    {
        List<string> listaDuplicados = new List<string>() { "insert", "duplicate key" };
        List<string> listaReferencia = new List<string>() { "delete", "reference" };

        string mensaje = ex!.Message == null ? "" : ex.Message;

        if (listaDuplicados.All(mensaje.Contains))
        {
            var error = ErrorBuilder.New()
               .SetMessage($"{objeto} especificado(a) ya existe en la base de datos")
               .SetCode("REGISTRO_DUPLICADO")
               .Build();

            return new GraphQLException(error);
        }
        else if (listaReferencia.All(mensaje.Contains))
        {
            var error = ErrorBuilder.New()
               .SetMessage($"No se puede eliminar {objeto} especificado(a); existen registros que dependen de éste(a)")
               .SetCode("ELIMINACION_NO_PERMITIDA")
               .Build();

            return new GraphQLException(error);
        }

        var errorIndefinido = ErrorBuilder.New()
           .SetMessage(MENSAJE_ERROR_POR_DEFECTO)
           .SetMessage("Mensaje original:" + ex.Message)
           .SetCode("ERROR_ACTUALIZACION_NO_ESPECIFICADO")
           .Build();

        return new GraphQLException(errorIndefinido);
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