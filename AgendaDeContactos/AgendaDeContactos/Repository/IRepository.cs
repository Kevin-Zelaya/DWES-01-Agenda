
using AgendaDeContactos.Data.Entities;

namespace AgendaDeContactos.Repository;

public interface IRepository
{
    public void createDatabase();
    public void CreateContact(Contact user);
    public IEnumerable<Contact> GetAll(int userId);
    public void DeleteAllContacts(Contact contact);
    public void DeleteContact(Contact contact);
    public void UpdateContact(Contact contact);
}