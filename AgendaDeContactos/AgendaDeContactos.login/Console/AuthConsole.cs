using AgendaDeContactos.login.DTO;
using AgendaDeContactos.login.Repository;
using AgendaDeContactos.login.Services;
using System;
using System.Net;
using AgendaDeContactos.login.Entities;
using AgendaDeContactos.login.Exceptions;

namespace AgendaDeContactos.login.Console;

public class AuthConsole
{
    public AuthConsole()
    {
        IRepository repository = new SqliteRawRepository();
        this._service = new UserService(repository);
    }

    public CreateResponseDto registerUser()
    {
        string username = "";
        string password = "";
        do
        {
            System.Console.WriteLine("--- Registra tu usuario ---");
            System.Console.WriteLine("");
            
            System.Console.WriteLine("Ingrese el nombre de usuario: (Debe empezar con una letra, usar solo letras y números y tener entre 4 a 10 caracteres)");
            System.Console.WriteLine("O escribe '0' para regresar");
            username = System.Console.ReadLine();

            if (username == "0") return null;
                
            System.Console.WriteLine("Ingrese una contraseña: (Debe contener al menos una letra mayúscula, un número y tener 6 o más caracteres)");
            password = System.Console.ReadLine();
            
            // Crear usuario
            try
            {
                    _service.RegisterUser(
                        username, 
                        password
                    );
                CreateResponseDto response = new CreateResponseDto(
                    "Usuario creado correctamente",
                    HttpStatusCode.Created
                );
                return response;
            }
            catch (GeneralException<UserExceptions> ex)
            {
                // imprime el mensaje de error y el código http
                System.Console.WriteLine($"Error {(int)ex.httpCode}: {ex.Message}");
                System.Console.WriteLine("Press enter para continuar...");
                System.Console.ReadLine();
                System.Console.Clear();
            }
            
        } while (true);
    }

    public LoginResponseDto loginUser()
    {
        string username = "";
        string password = "";
        do
        {
            System.Console.WriteLine(" --- Inicio de sesión ---");
            System.Console.WriteLine("");
            
            System.Console.WriteLine("Ingrese el nombre de usuario (o escriba '0' para regresar)");
            username = System.Console.ReadLine();

            if (username == "0") return null;
                
            System.Console.WriteLine("Ingrese una contraseña:");
            password = System.Console.ReadLine();
            
            try
            {
                User user = _service.LoginUser(
                    username, 
                    password
                );
                return new LoginResponseDto(
                    "Inicio de sesión correcto",
                    HttpStatusCode.OK,
                    new UserDto(
                        user.getUsername(),
                        user.getId()
                        )
                );
            }
            catch (GeneralException<UserExceptions> ex)
            {
                // imprime el mensaje de error y el código http
                System.Console.WriteLine($"Error {(int)ex.httpCode}: {ex.Message}");
                System.Console.WriteLine("Press enter para continuar...");
                System.Console.ReadLine();
                System.Console.Clear();
            }
            
        } while (true);
    }
    private UserService _service;
}