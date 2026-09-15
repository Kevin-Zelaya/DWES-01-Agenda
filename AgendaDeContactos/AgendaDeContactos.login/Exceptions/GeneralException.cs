using System.Net;

namespace AgendaDeContactos.login.Exceptions;

public class GeneralException<T> : Exception where T : IException
{
    public HttpStatusCode httpCode { get; private set; }
    public GeneralException(T error)
        : base(error.message)
    {
        httpCode = error.httpCode;
    }
    // Sobrecarga para errores con los campos de los contactos
    public GeneralException(T error, string field)
        : base(error.message+field)
    {
        httpCode = error.httpCode;
    }
}