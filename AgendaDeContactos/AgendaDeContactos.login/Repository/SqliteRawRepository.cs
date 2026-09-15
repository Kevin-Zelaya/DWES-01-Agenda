using AgendaDeContactos.login.Entities;

namespace AgendaDeContactos.login.Repository;

// Imports
using Microsoft.Data.Sqlite;
using System.IO;

public class SqliteRawRepository : IRepository
{
    public SqliteRawRepository()
    {
        _databasePath = GetDatabasePath(); // Obtener la ruta del archivo .db
        createDatabase(); // Si no existe, crea la base de datos
    }
    public void createDatabase() 
    {
        using (var conexion = new SqliteConnection(_databasePath)) // Abrir o crear fichero
        {
            conexion.Open();

            using (var command = new SqliteCommand(CREATE_QUERY, conexion))
            {
                command.ExecuteNonQuery(); // Crear tabla
            }
        }
    }

    private string GetDatabasePath()
    {
        String projectPath = AppDomain.CurrentDomain.BaseDirectory; // Ruta del archivo actual
        DirectoryInfo? projectInfo = Directory.GetParent(projectPath)? // Obtener direcotrios padre
            .Parent?
            .Parent?
            .Parent;

        if (projectInfo == null) 
        {
            System.Console.WriteLine($"Error en la ruta contenedora: {projectInfo.FullName}");
        }
        // Buscar directorio repository
        String repositoryPath = Path.Combine(projectInfo.FullName, "Repository"); // úne la ruta agregandole el directorio "Repository al final"

        if (!Directory.Exists(repositoryPath)) // Comprobar si repository existe
        {
            System.Console.WriteLine($"No existe el directorio: {repositoryPath}");
        }
        
        String databasePath = Path.Combine(repositoryPath, "database.db"); // crear ruta con el archivo de la base de datos
        
        return $"Data Source={databasePath}"; // retorna algo parecido a ./repository/database.db
    } 
    // Crear usuario
    public void create(User user)
    {
        using (var conexion = new SqliteConnection(_databasePath))
        {
            conexion.Open();
            string query = "INSERT INTO Users (username, password) VALUES (@username, @password)";
            using (var command = new SqliteCommand(query, conexion))
            {
                // Agrega parametros al comando
                command.Parameters.AddWithValue("@username", user.getUsername());
                command.Parameters.AddWithValue("@password", user.getPassword());
                command.ExecuteNonQuery(); // ejecuta la consulta
            }
        }
    }
    // Eliminar usuario
    public void delete(User user)
    {
        using (var conexion = new SqliteConnection(_databasePath))
        {
            conexion.Open();
            string query = "DELETE FROM Users WHERE Username = @username";
            using (var command = new SqliteCommand(query, conexion))
            {
                // Agrega parametros al comando
                command.Parameters.AddWithValue("@Username", user.getUsername());
                command.ExecuteNonQuery();
            }
        }
    }
    // Actualizar usuario
    public void update(User user)
    {
        using (var conexion = new SqliteConnection(_databasePath))
        {
            conexion.Open();
            string query = "UPDATE Users SET password = @password, username = @username WHERE Id = @id";
            using (var command = new SqliteCommand(query, conexion))
            {
                // Agrega parametros al comando
                command.Parameters.AddWithValue("@username", user.getUsername());
                command.Parameters.AddWithValue("@password", user.getPassword());
                command.Parameters.AddWithValue("@password", user.getId());
                command.ExecuteNonQuery();
            }
        }
    }
    // Obtener por id
    public User getById(int id)
    {
        using (var conexion = new SqliteConnection(_databasePath))
        {
            conexion.Open();
            string query = "SELECT * FROM Users WHERE Id = @id";
            using (var command = new SqliteCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@id", id);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                    }
                }
            }
        }

        return null;
    }
    // Obtener por username
    public User GetByUsername(string username)
    {
        using (var conexion = new SqliteConnection(_databasePath))
        {
            conexion.Open();
            string query = "SELECT * FROM Users WHERE username = @username";
            using (var command = new SqliteCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@username", username);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                    }
                }
            }
        }

        return null;
    }
    // obtener todos los usuarios, no se utiliza durante la ejecución
    public IEnumerable<User> getAll()
    {
        var list = new List<User>();
        using (var conexion = new SqliteConnection(_databasePath))
        {
            conexion.Open();
            string query = "SELECT * FROM Users";
            using (var command = new SqliteCommand(query, conexion))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                   list.Add(new User
                       (reader.GetInt32(0),reader.GetString(1), reader.GetString(2))
                   );
                }
            }
        }
        return list;
    }

    private string _databasePath = "";
    private const string CREATE_QUERY = "CREATE TABLE IF NOT EXISTS Users (id INTEGER PRIMARY KEY, username TEXT, password TEXT)";
}