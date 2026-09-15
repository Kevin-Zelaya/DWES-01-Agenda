using System.Text.RegularExpressions;
using AgendaDeContactos.Exceptions;

namespace AgendaDeContactos.Data.Entities;

public class Contact
{
    private int _id;
    private int _userId;
    private string _name;
    private string _lastName;
    private string _email;
    private string _phone;
    private string _alias;
    
    public int GetID() => _id;
    public int GetUserId() => _userId;
    public string GetName() => _name;
    public string GetLastName() => _lastName;
    public string GetEmail() => _email;
    public string GetPhone() => _phone;
    public string GetAlias() => _alias;
    
    public void SetID(int id)  => _id = id;
    public void SetUserID(int userID) => _userId = userID;

    public void SetName(string name)
    {
        string pattern = "^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\\s'-]+$";
        string message = "El nombre solo puede contener letras (incluyendo tildes y la ñ), espacios, guiones o apóstrofes.";
        
        ValidateFields(name, pattern, message); 
        _name = name;
    }

    public void SetLastName(string lastName)
    {
        string pattern = "^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\\s'-]+$";
        string message = "El apellido solo puede contener letras (incluyendo tildes y la ñ), espacios, guiones o apóstrofes.";
        
        ValidateFields(lastName, pattern, message);
        _lastName = lastName;
    }

    public void SetEmail(string email)
    {
        string pattern = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";
        string message = "El correo electrónico debe tener un formato válido (ejemplo: usuario@dominio.com).";
        
        ValidateFields(email, pattern, message); // <-- ¡AHORA SÍ VALIDA!
        _email = email;
    }

    public void SetAlias(string alias)
    {
        string pattern = "^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ0-9\\W_]{1,10}$";
        string message = "El alias puede contener letras, números y símbolos, y no debe superar los 10 caracteres de longitud.";
        
        ValidateFields(alias, pattern, message); 
        _alias = alias;
    }

    public void SetPhoneNumber(string phoneNumber)
    {
        string pattern = "^(?:\\+34|34)?\\d{9}$"; 
        string message = "El número de teléfono debe ser un número español válido de 9 dígitos (puede incluir el prefijo +34 o 34).";
        
        ValidateFields(phoneNumber, pattern, message); 
        _phone = phoneNumber;
    }

    private void ValidateFields(string field, string pattern, string message)
    {
        if (string.IsNullOrEmpty(field) || !Regex.IsMatch(field, pattern))
            throw new GeneralException<ContactExceptions>(ContactExceptions.ERROR_IN_THE_FIELD, message);
    }

    public Contact(int id, int userId, string name, string lastname, string alias, string email, string phone_number)
    {
        _id = id;
        _userId = userId;
        SetName(name);
        SetLastName(lastname);
        SetEmail(email);
        SetPhoneNumber(phone_number);
        SetAlias(alias);
    }

    // Sobrecarga sin id
    public Contact(int userId, string name, string lastname, string alias, string email, string phone_number)
    {
        _userId = userId;
        SetName(name);
        SetLastName(lastname);
        SetEmail(email);
        SetPhoneNumber(phone_number);
        SetAlias(alias);
    }
}