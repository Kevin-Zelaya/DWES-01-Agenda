using System.Net;

namespace AgendaDeContactos.login.DTO;

public class IDto
{
    public string message { get; protected set; }
    public HttpStatusCode httpCode { get; protected set; }
}