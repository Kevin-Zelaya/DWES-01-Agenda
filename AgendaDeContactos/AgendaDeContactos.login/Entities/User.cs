using System.Text.RegularExpressions;
using AgendaDeContactos.login.Exceptions;

namespace AgendaDeContactos.login.Entities;

public class User
{
    public User(string username, string password)
    {
        setUsername(username);
        setPassword(password);
    }
    public User(int id, string username, string password)
    {
        this.id = id;
        setUsername(username);
        setPassword(password);
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
        string pattern = @"^[a-zA-Z][a-zA-Z0-9]{3,9}$";
        string message =
            "Ingrese el nombre de usuario: (Debe empezar con una letra, contener solo letras y números y tener entre 4 y 10 caracteres)";
        ValidateFields(newUsername, pattern, message); 
        username = newUsername;
    }
    public void setPassword(string newPassword){
        string pattern = @"^(?=.*[A-Z])(?=.*\d).{6,}$";
        string message =
            "Ingrese una contraseña: (Debe contener al menos una letra mayúscula, un número y tener 6 o más caracteres)";
        ValidateFields(newPassword, pattern, message); 
        password = newPassword;
    }

    public void setEncodePassword(string newEncodePassword)
    {
        password = newEncodePassword;
    }

    public int getId()
    {
        return id;
    }

    public void setId(int newId)
    {
        id = newId;
    }
    
    private void ValidateFields(string field, string pattern, string message)
    {
        if (string.IsNullOrEmpty(field) || !Regex.IsMatch(field, pattern))
            throw new GeneralException<UserExceptions>(UserExceptions.ERROR_IN_THE_FIELD, message);
    }
}