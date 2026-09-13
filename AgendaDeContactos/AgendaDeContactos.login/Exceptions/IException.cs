using System.Net;

namespace AgendaDeContactos.login.Exceptions;

public interface IException
{
    public string message { get; }
    public HttpStatusCode httpCode { get; }
}