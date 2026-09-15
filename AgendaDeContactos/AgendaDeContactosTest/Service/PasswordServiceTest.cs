using AgendaDeContactos.login.Exceptions;
using AgendaDeContactos.login.Services;

namespace AgendaDeContactosTest.Service;

public class PasswordServiceTest
{
    [Fact]
    public void EncryptPlaintext_Correctly_EncryptedPassword()
    {
        String plainText = "Prueba";
        PasswordService service = new PasswordService();
        string encyptedPassword = service.PasswordEncoder(plainText);
        service.PasswordValidator(plainText, encyptedPassword);
    }
    [Fact]
    public void PasswordValidator_IncorrectPassword_ShouldThrowException()
    {
        string plainText = "Prueba";
        string wrongPassword = "Contraseña incorrecta";
        PasswordService service = new PasswordService();
        string encryptedPassword = service.PasswordEncoder(plainText);
        Assert.Throws<GeneralException<UserExceptions>>(
            () => service.PasswordValidator(wrongPassword, encryptedPassword)
        );
    }
}