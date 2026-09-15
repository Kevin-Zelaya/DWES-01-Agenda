using AgendaDeContactos.login.Entities;
using AgendaDeContactos.login.Exceptions;

namespace AgendaDeContactos.login.Services;

public class PasswordService
{
    // lo leí en techriders https://techriders.tajamar.es/encriptacion-con-bcrypt-en-c/
    public string PasswordEncoder(string password) // Codificar contraseña con bcrypt
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public void PasswordValidator(string password, string hash) // Validar contraseña
    {
        if (!BCrypt.Net.BCrypt.Verify( password, hash))
            throw new GeneralException<UserExceptions>(UserExceptions.INCORRECT_PASSWORD);
    }
}