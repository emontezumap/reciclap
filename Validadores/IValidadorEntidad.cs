using Herramientas;

namespace Validadores;

public interface IValidadorEntidad
{
    public Task<ResultadoValidacion> Validar();
}