using System.Net;

namespace AgendaDeContactos.login.Exceptions;

public class UserExceptions : IException
{
    
    public string message { get; }
    public HttpStatusCode httpCode { get; }
    
    UserExceptions(string message, HttpStatusCode httpCode){
        this.message = message;
        this.httpCode = httpCode;
    }
    
    public static readonly UserExceptions USER_ALREADY_EXISTS = 
        new("User already exists", HttpStatusCode.Conflict);
    public static readonly UserExceptions USER_NOT_FOUND = 
        new("User not found", HttpStatusCode.NotFound);
    public static readonly UserExceptions INVALID_USERNAME = 
        new("Invalid username", HttpStatusCode.BadRequest);
    public static readonly UserExceptions INVALID_PASSWORD = 
        new("Invalid password", HttpStatusCode.BadRequest);
    public static readonly UserExceptions INCORRECT_PASSWORD = 
        new("Incorrect password", HttpStatusCode.Unauthorized);
}