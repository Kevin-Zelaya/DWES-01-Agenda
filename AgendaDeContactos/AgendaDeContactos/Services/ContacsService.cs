using AgendaDeContactos.Data.Entities;
using AgendaDeContactos.Repository;

namespace AgendaDeContactos;

public class ContacsService
{
    public ContacsService(IRepository repository)
    {
        _repository = repository;
    }
    // Crear contacto
    public void CreateContact(int userId,
        string name,
        string lastname,
        string email,
        string phoneNumber,
        string alias)
    {

        Contact contact = new Contact(
            userId,
            name,
            lastname,
            alias,
            email,
            phoneNumber);
        _repository.CreateContact(contact);
        
    }

    public void DeleteContact(Contact contact)
    {
        _repository.DeleteContact(contact);
    }
    private IRepository _repository;

    public void UpdateContact(
        int id,
        int userId,
        string name,
        string lastname,
        string email,
        string phoneNumber,
        string alias)
    {
        Contact contact = new Contact(
            id,
            userId,
            name,
            lastname,
            alias,
            email,
            phoneNumber);
        _repository.UpdateContact(contact);
    }

    public IEnumerable<Contact> GetContacts(int userId)
    {
        return _repository.GetAll(userId);
    }
}