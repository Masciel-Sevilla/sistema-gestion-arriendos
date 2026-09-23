namespace ArriendosApi.Exceptions
{
    public abstract class AppException:Exception
    {
        protected AppException(string message) :base(message) { }
    }
    public class NoEncontradoException : AppException
    {
        public NoEncontradoException(string entidad, int id)
            : base($"No se encontró {entidad} con id {id}.") { }
    }
    public class EstadoInvalidoException : AppException
    {
        public EstadoInvalidoException(string estado)
            : base($"El estado '{estado}' no es válido.") { }
    }

    public class ReferenciaInvalidaException : AppException
    {
        public ReferenciaInvalidaException(string mensaje) : base(mensaje) { }
    }
    public class OperacionNoPermitidaException : AppException
    {
        public OperacionNoPermitidaException(string mensaje) : base(mensaje) { }
    }
}
