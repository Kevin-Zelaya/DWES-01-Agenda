using System.Net;

namespace AgendaDeContactos.login.DTO;

public class LoginResponseDto : IDto
{
    public UserDto user { get; private set; }
    
    public LoginResponseDto(string message, HttpStatusCode httpStatus, UserDto user)
    {
        this.message = message;
        this.httpCode = httpStatus;
        this.user = user;
    }
}