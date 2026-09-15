using System.Net;

namespace AgendaDeContactos.Exceptions;

public interface IException
{
    public string message { get; }
    public HttpStatusCode httpCode { get; }
}