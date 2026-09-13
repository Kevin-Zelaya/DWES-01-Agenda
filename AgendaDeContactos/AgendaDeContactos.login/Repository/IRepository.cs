using AgendaDeContactos.login.Entities;

namespace AgendaDeContactos.login.Repository;

public interface IRepository
{
    public void createDatabase();
    public void create(User user);
    public void delete(User user);
    public void update(User user);
    public User getById(int id);
    public User GetByUsername(string username);
}