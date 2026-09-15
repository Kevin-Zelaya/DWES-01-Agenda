using System.Net;

namespace AgendaDeContactos.login.DTO;

public class CreateResponseDto : IDto
{
    public CreateResponseDto()
    {
        
    }
    public CreateResponseDto(string message, HttpStatusCode httpCode)
    {
        this.message = message;
        this.httpCode = httpCode;
    }
    
}