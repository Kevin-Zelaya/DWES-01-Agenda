using System.Net;

namespace AgendaDeContactos.login.DTO;

public class CreateResponseDto
{
    public CreateResponseDto(string message, HttpStatusCode httpCode)
    {
        this.message = message;
        this.httpCode = httpCode;
    }
    public string message { get; set; }
    public HttpStatusCode httpCode { get; set; }
    
}