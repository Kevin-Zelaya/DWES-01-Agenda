
using AgendaDeContactos.login.Entities;
using AgendaDeContactos.login.Exceptions;
using UserExceptions = AgendaDeContactos.login.Exceptions.UserExceptions;

namespace AgendaDeContactosTest.Service;

public class UserTest
{
    [Fact]
    public void CreateUser_CorrectData_UserCreated()
    {
        User user = new User(
            1, "Kevin", "ContraseñaCorrecta3"
        );
        // Comprobamos los campos
        Assert.Equal(1, user.getId());
        Assert.Equal("Kevin", user.getUsername());
        Assert.Equal("ContraseñaCorrecta3", user.getPassword());
        
    }
    [Fact]
    public void CreateUser_IncorrectData_UserCreated()
    {
        // Comprobamos con datos incorrectos para que salte "General exception"
        Assert.Throws<GeneralException<UserExceptions>>(
            ()=> new User(
                    1, "Kevin33", ""
                )
            );
    }
}