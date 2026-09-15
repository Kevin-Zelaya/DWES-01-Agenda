using System.Net;
using AgendaDeContactos.Data.Entities;
using AgendaDeContactos.Dto;
using AgendaDeContactos.Exceptions;
using AgendaDeContactos.login.DTO;
using AgendaDeContactos.Repository;

namespace AgendaDeContactos.Console;

public class ContactConsole
{
    public ContactConsole(int userId)
    {
        _userId = userId;
    }
    public void MainContacs()
    {
        GetContacs();
        ShowResponse();
        pageSize = 2;
        pageNumber = 1;
        do
        {
            response = null;
            try
            {
                totalPages = (int)Math.Ceiling(
                    (double)_contacts.Count() / pageSize
                );
                System.Console.WriteLine("=== Lista de Contactos ===\n");
                System.Console.WriteLine($"Total de contactos = {(double)_contacts.Count()}\n");
                // Mostrar contactos páginados
                ShowPaginatedContacs(pageNumber, pageSize);
                System.Console.WriteLine();
                System.Console.WriteLine(
                    $"{(pageNumber == 1 ? "" : "<- Anterior")}   {(pageNumber == totalPages ? "" : " Siguiente ->" )}");
                System.Console.WriteLine("A Agregar");
                if (_contacts.Count() > 0)
                {
                    System.Console.WriteLine("M Modificar");
                    System.Console.WriteLine("E Eliminar");
                }

                System.Console.WriteLine("S Salir");

                ConsoleKeyInfo key = System.Console.ReadKey();
                System.Console.WriteLine("");
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        changePage(-1);
                        break;
                    case ConsoleKey.RightArrow:
                        changePage(1);
                        break;
                    case ConsoleKey.A:
                        response = CreateContact();
                        break;
                    case ConsoleKey.M:
                        response = UpdateContact();
                        break;
                    case ConsoleKey.E:
                        response = DeleteContact();
                        break;
                    case ConsoleKey.S:
                        return;



                }

                if (response != null)
                {
                    ShowResponse();
                    GetContacs();
                }

                System.Console.WriteLine("Press enter para continuar...");
                System.Console.ReadLine();
                System.Console.Clear();
            }
            catch (GeneralException<ContactExceptions> ex)
            {
                System.Console.WriteLine($"Error {(int)ex.httpCode}: {ex.Message}");
                System.Console.WriteLine("Press enter para continuar...");
                System.Console.ReadLine();
                System.Console.Clear();
            }
        } while (true);
    }

    private void changePage(int number)
    {
        if (number < 0 && number == 1) return;
        if (number > 0 && pageNumber == totalPages) return;
        pageNumber += number;
        return;
    }
    private void GetContacs() // Obtener todos los contactos del usuario mediante su id
    {
        _contacts =service.GetContacts(_userId);
        response = new ResponseDto(
            "Contactos cargados correctamente",
            HttpStatusCode.OK);
    }

    private void ShowPaginatedContacs(int pageNumber, int pageSize)
    {
        int inicio = (pageNumber - 1) * pageSize;

        var contactosPagina = _contacts
            .Skip(inicio)
            .Take(pageSize);
        System.Console.WriteLine("--- Télefono - nombre - apellido - Correo - Alias - ");
        if (_contacts.Count() == 0)
        {
            System.Console.WriteLine("No hay contactos por mostrar");
            return;
        }
        foreach (var contact in contactosPagina)
        {
            System.Console.WriteLine($"{inicio+1}. - {contact.GetPhone()} {contact.GetName()} {contact.GetLastName()} {contact.GetEmail()} {contact.GetAlias()} ");
            inicio++;
        }
    }

    private IDto CreateContact()
    {
        string name;
        string lastname;
        string email;
        string alias;
        string phoneNumber;
        
        do
        {
            System.Console.Clear();
            
            System.Console.WriteLine("=== Crear contacto ===");
            System.Console.WriteLine("");
            System.Console.WriteLine("Presione 'S' para salir, cualquier otra tecla para continuar");
            ConsoleKeyInfo key = System.Console.ReadKey();
            if (key.Key == ConsoleKey.S) return null;
            
            System.Console.WriteLine("Ingrese el número de télefono:");
            phoneNumber = System.Console.ReadLine();
            System.Console.WriteLine("Ingrese el nombre:");
            name = System.Console.ReadLine();
            System.Console.WriteLine("Ingrese el apellido:");
            lastname = System.Console.ReadLine();
            System.Console.WriteLine("Ingrese el email:");
            email = System.Console.ReadLine();
            System.Console.WriteLine("Ingrese el alias:");
            alias = System.Console.ReadLine();
            
            repository.CreateContact(
                new Contact(
                    _userId,
                    name,
                    lastname,
                    alias,
                    email,
                    phoneNumber)
                );
            GetContacs();
            return new ResponseDto(
                "Contacto agregado correctamente.",
                HttpStatusCode.Created
                );

        } while (true);
    }

    private IDto UpdateContact()
    {
        do
        {
            System.Console.WriteLine("=== Actualizar contacto ===");
            System.Console.WriteLine("");
            System.Console.WriteLine("Presione 'S' para salir, cualquier otra tecla para continuar");
            ConsoleKeyInfo key = System.Console.ReadKey();
            if (key.Key == ConsoleKey.S) return null;
            System.Console.WriteLine("Ingrese el número de lista actual del contacto:");
            int userNumber;
            try
            {
                userNumber = int.Parse(System.Console.ReadLine());
                if (userNumber > _contacts.Count())
                    throw new GeneralException<ContactExceptions>(ContactExceptions.INCORRECT_USER_LIST_NUMBER);
                int id = ShowSpecificContact(userNumber-1);
                CreateNewDataForContact(id);
                return new ResponseDto(
                    "Contacto actualizado correctamente.",
                    HttpStatusCode.OK
                );
            }
            catch (FormatException e)
            {
                throw new GeneralException<ContactExceptions>(ContactExceptions.INCORRECT_USER_LIST_NUMBER);
            }
            
        } while (true);
    }

    private int ShowSpecificContact(int contactNumber) // retorna el id del contacto
    {
        System.Console.WriteLine($"Contacto: {_contacts.ElementAt(contactNumber).GetPhone()} {_contacts.ElementAt(contactNumber).GetName()} {_contacts.ElementAt(contactNumber).GetLastName()} {_contacts.ElementAt(contactNumber).GetEmail()} {_contacts.ElementAt(contactNumber).GetAlias()} ");
        return _contacts.ElementAt(contactNumber).GetID();
    }

    private void CreateNewDataForContact(int id)
    {
        string name;
        string lastname;
        string email;
        string alias;
        string phoneNumber;
        do
        {
            
            System.Console.WriteLine("Ingrese el nuevo número de télefono del usuario:");
            phoneNumber = System.Console.ReadLine();
            System.Console.WriteLine("Ingrese el nombre:");
            name = System.Console.ReadLine();
            System.Console.WriteLine("Ingrese el apellido:");
            lastname = System.Console.ReadLine();
            System.Console.WriteLine("Ingrese el email:");
            email = System.Console.ReadLine();
            System.Console.WriteLine("Ingrese el alias:");
            alias = System.Console.ReadLine();
            
            repository.UpdateContact(
                new Contact(
                    id,
                    _userId,
                    name,
                    lastname,
                    alias,
                    email,
                    phoneNumber
                    )
                );
            return;
        } while (true);
    }

    private IDto DeleteContact()
    {
        do
        {
            System.Console.WriteLine("=== Eliminar contacto ===");
            System.Console.WriteLine("");
            System.Console.WriteLine("Presione 'S' para salir, cualquier otra tecla para continuar");
            ConsoleKeyInfo key = System.Console.ReadKey();
            if (key.Key == ConsoleKey.S) return null;
            System.Console.WriteLine("Ingrese el número de lista actual del contacto:");
            int userNumber;
            try
            {
                userNumber = int.Parse(System.Console.ReadLine());
                if (userNumber > _contacts.Count())
                    throw new GeneralException<ContactExceptions>(ContactExceptions.INCORRECT_USER_LIST_NUMBER);
                int id = ShowSpecificContact(userNumber-1);
                repository.DeleteContact(
                        _contacts.ElementAt(userNumber-1)
                    );
                System.Console.WriteLine("Contacto eliminado correctamente.");
                GetContacs();
                
                return new ResponseDto(
                    "Contacto eliminado correctamente.",
                    HttpStatusCode.OK
                );
            }
            catch (FormatException e)
            {
                throw new GeneralException<ContactExceptions>(ContactExceptions.INCORRECT_USER_LIST_NUMBER);
            }
            
        } while (true);
    }
    void ShowResponse()
    {
        if (response != null)
        {
            System.Console.WriteLine("");
            System.Console.WriteLine($"Código {(int)response.httpCode}:  {response.message}");
            System.Console.WriteLine("");
        }
    }
    private int pageSize;
    private int pageNumber;
    private int totalPages;
    private IDto response; // Para los códigos http
    private static IRepository repository = new SqliteRawRepository();
    private ContacsService service = new ContacsService(repository);
    private int _userId;
    private IEnumerable<Contact> _contacts;
}