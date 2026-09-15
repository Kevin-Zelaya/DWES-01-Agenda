using AgendaDeContactos.Data.Entities;
using AgendaDeContactos.login.DTO;

namespace AgendaDeContactos.Dto;

public class GetContactResponseDto : IDto
{
    public IEnumerable<Contact> contacts  { get; set; }
    public GetContactResponseDto(IEnumerable<Contact> contacts, ResponseDto response)
    {
        this.contacts = contacts;
        this.httpCode = response.httpCode;
        this.message = response.message;
    }
}