using AgendaDeContactos.login.Entities;
using System.Text.RegularExpressions;
using AgendaDeContactos.login.Exceptions;
using AgendaDeContactos.login.Repository;

namespace AgendaDeContactos.login.Services;

public class UserService
{   
    public UserService(IRepository repository)
    {
        _repository = repository;
        _passwordService = new PasswordService();
    }
    
    public void RegisterUser(string username, string password)
    {
        // Comprobar campos
        FieldValidation(username, password);
        
        // Comprobar si existe el usuario
        if (_repository.GetByUsername(username) != null)
            throw new GeneralException<UserExceptions>(UserExceptions.USER_ALREADY_EXISTS);
        
        // Guardar usuario
        _repository.create(
                new User(
                    username,
                    _passwordService.PasswordEncoder(password)
                    )
            );
        
    }
    // validar campos
        private void FieldValidation(string username, string password) 
        {
            if (!Regex.IsMatch(username, USERNAME_PATTERN))
                throw new GeneralException<UserExceptions>(UserExceptions.INVALID_USERNAME);
            if (!Regex.IsMatch(password, PASSWORD_PATTERN))
                throw new GeneralException<UserExceptions>(UserExceptions.INVALID_PASSWORD);
        }
    public User LoginUser(string username, string password)
    {
        // Obtener el usuario
        User user = _repository.GetByUsername(username);
        if (user == null)
            throw new GeneralException<UserExceptions>(UserExceptions.USER_NOT_FOUND);
        _passwordService.PasswordValidator(password, user.getPassword());
        
        return user;
    
    }
    private const string USERNAME_PATTERN = @"^[a-zA-Z][a-zA-Z0-9]{3,9}$";
    private const string PASSWORD_PATTERN = @"^(?=.*[A-Z])(?=.*\d).{6,}$";
    private PasswordService _passwordService;
    private IRepository _repository;
}