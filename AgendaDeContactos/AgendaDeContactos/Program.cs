
using AgendaDeContactos;
using AgendaDeContactos.Console;
using AgendaDeContactos.Data.Entities;
using AgendaDeContactos.Exceptions;
using AgendaDeContactos.Repository;
using AgendaDeContactos.login.Console;
using AgendaDeContactos.login.DTO;
using SQLitePCL;





AuthConsole auth = new AuthConsole();
ContactConsole console;
IDto response;
UserDto user;
SelectAuth();
void SelectAuth()
{
    do
    {
        Console.WriteLine("=== Acceso ===");
        Console.WriteLine("");
        Console.WriteLine("Presione:");
        Console.WriteLine("R Registrar  -  I Inicio de sesión   - S Salir");
        ConsoleKeyInfo key = Console.ReadKey();
        if (key.Key == ConsoleKey.R)
        {
            response = auth.registerUser();
            showAuth();
        } else if (key.Key == ConsoleKey.I)
        {
            response = auth.loginUser();
            
            if (response == null)
                continue;
            
            user = ((LoginResponseDto)response).user;
            showAuth();
            Console.WriteLine();
            console = new ContactConsole(user.id);
            console.MainContacs();
        }
        else if (key.Key == ConsoleKey.S)
        {
            Console.WriteLine("Saliendo de la aplicación ...");
            return;
        }
        else
        {
            Console.Clear();
        }
    } while (true);
}

void showAuth()
{
    if (response != null)
    {
        Console.WriteLine("");
        Console.WriteLine($"Código {(int)response.httpCode}:  {response.message}");
    }
}





    

