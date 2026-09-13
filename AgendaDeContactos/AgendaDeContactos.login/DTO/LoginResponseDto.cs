using System.Net;

namespace AgendaDeContactos.login.DTO;

public class LoginResponseDto
{
    public string message { get; private set; }
    public HttpStatusCode statusCode { get; private set; }
    public UserDto user { get; private set; }
    
    public LoginResponseDto(string message, HttpStatusCode statusCode, UserDto user)
    {
        this.message = message;
        this.statusCode = statusCode;
        this.user = user;
    }
}