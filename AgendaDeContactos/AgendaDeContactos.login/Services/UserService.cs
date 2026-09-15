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
        // crear objeto con valores par validar campos
        User user = new User(username, password);
        
        // Comprobar si existe el usuario
        if (_repository.GetByUsername(username) != null)
            throw new GeneralException<UserExceptions>(UserExceptions.USER_ALREADY_EXISTS);
        // Codificar la contraseña para no guardar en texto plano
        user.setEncodePassword(_passwordService.PasswordEncoder(user.getPassword()));
        // Guardar usuario
        _repository.create(
            user
            );
        
    }
    public User LoginUser(string username, string password)
    {
        // Obtener el usuario
        User user = _repository.GetByUsername(username);
        if (user == null) // igual a null, usuario no existe
            throw new GeneralException<UserExceptions>(UserExceptions.USER_NOT_FOUND);
        _passwordService.PasswordValidator(password, user.getPassword());
        
        return user;
    
    }
    private PasswordService _passwordService;
    private IRepository _repository;
}