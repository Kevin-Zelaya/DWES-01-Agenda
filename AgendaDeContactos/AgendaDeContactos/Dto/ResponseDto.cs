using System.Net;
using AgendaDeContactos.login.DTO;

namespace AgendaDeContactos.Dto;

public class ResponseDto : IDto
{
    public ResponseDto(string message, HttpStatusCode httpCode)
    {
        this.message = message;
        this.httpCode = httpCode;
    }
}