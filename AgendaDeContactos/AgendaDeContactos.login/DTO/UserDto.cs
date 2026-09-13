namespace AgendaDeContactos.login.DTO;

public class UserDto
{
    public string username { get; private set; }
    public int id { get; private set; }
    public UserDto(string username, int id)
    {
        this.username = username;
        this.id = id;
    }
}