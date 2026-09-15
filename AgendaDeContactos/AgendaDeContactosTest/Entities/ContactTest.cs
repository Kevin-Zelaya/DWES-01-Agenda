using AgendaDeContactos.Data.Entities;
using AgendaDeContactos.Exceptions;

namespace AgendaDeContactosTest.Service;

public class ContactTest
{
    [Fact]
    public void CreateContact_CorrectData_UserCreated()
    {
        // Creamos usuario
        Contact contact = new Contact(
            1, 1, "Kevin", "Zelaya", "kevo", "email@email.com", "642593455"
        );
        // Comprobamos los campos
        Assert.Equal(1, contact.GetID());
        Assert.Equal(1, contact.GetUserId());
        Assert.Equal("Kevin", contact.GetName());
        Assert.Equal("Zelaya", contact.GetLastName());
        Assert.Equal("kevo", contact.GetAlias());
        Assert.Equal("email@email.com", contact.GetEmail());
        Assert.Equal("642593455", contact.GetPhone());
    }
    [Fact]
    public void CreateContact_IncorrectData_UserCreated()
    {
        // Creamos usuario introduciendo datos incorrectos
        Assert.Throws<GeneralException<ContactExceptions>>(
            () => new Contact(
                1, 1, "Kev", "Zelaya3", "kevo", "email@email.com", "64259345"
            )
        );

    }
    
}