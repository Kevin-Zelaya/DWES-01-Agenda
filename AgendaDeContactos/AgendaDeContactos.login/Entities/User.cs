namespace AgendaDeContactos.login.Entities;

public class User
{
    public User(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
    public User(int id, string username, string password)
    {
        this.id = id;
        this.username = username;
        this.password = password;
    }

    private int id;
    private string username;
    private string password;

    public string getUsername()
    {
        return username;
    }
    public string getPassword()
    {
        return password;
    }
    public void setUsername(string newUsername)
    {
        username = newUsername;
    }
    public void setPassword(string newPassword){
        password = newPassword;
    }

    public int getId()
    {
        return id;
    }

    public void setId(int newId)
    {
        id = newId;
    }
}