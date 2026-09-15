using System.Net;

namespace AgendaDeContactos.Exceptions;

public class ContactExceptions : IException
{
    
    public string message { get; }
    public HttpStatusCode httpCode { get; }
    
    ContactExceptions(string message, HttpStatusCode httpCode){
        this.message = message;
        this.httpCode = httpCode;
    }
    
    public static readonly ContactExceptions THERE_IS_A_CONTACT_FOR_THAT_NUMBER = 
        new("There is a contact for that number", HttpStatusCode.Conflict);
    public static readonly ContactExceptions CONTACT_NOT_FOUND = 
        new("There is a contact for that number", HttpStatusCode.Conflict);
    public static readonly ContactExceptions INVALID_CONTACT_NAME =
        new("The contact name is invalid", HttpStatusCode.BadRequest);
    public static readonly ContactExceptions ERROR_DELETING_THE_CONTACT =
        new("Error deleting the contact", HttpStatusCode.InternalServerError);
    public static readonly ContactExceptions ERROR_DELETING_THE_CONTACTS =
        new("Error deleting the contacts", HttpStatusCode.InternalServerError);
    public static readonly ContactExceptions ERROR_UPDATING_THE_CONTACT =
        new("Error updating the contact", HttpStatusCode.InternalServerError);
    public static readonly ContactExceptions ERROR_IN_THE_FIELD =
        new("Validation error in a field: ", HttpStatusCode.BadRequest);
    public static readonly ContactExceptions INCORRECT_USER_LIST_NUMBER =
        new("Incorrect user list number: ", HttpStatusCode.BadRequest);
    

}