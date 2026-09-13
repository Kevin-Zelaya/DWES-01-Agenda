using AgendaDeContactos.login.Entities;

namespace AgendaDeContactos.login.Repository;

// Imports
using Microsoft.Data.Sqlite;
using System.IO;

public class SqliteRawRepository : IRepository
{
    public SqliteRawRepository()
    {
        _databasePath = GetDatabasePath();
        createDatabase();
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
        String repositoryPath = Path.Combine(projectInfo.FullName, "Repository");

        if (!Directory.Exists(repositoryPath)) // Comprobar si repository existe
        {
            System.Console.WriteLine($"No existe el directorio: {repositoryPath}");
        }
        
        String databasePath = Path.Combine(repositoryPath, "database.db"); // crear ruta con el archivo de la base de datos
        
        return $"Data Source={databasePath}"; // retorna algo parecido a ./repository/database.db
    } 

    public void create(User user)
    {
        using (var conexion = new SqliteConnection(_databasePath))
        {
            conexion.Open();
            string query = "INSERT INTO Users (username, password) VALUES (@username, @password)";
            using (var command = new SqliteCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@username", user.getUsername());
                command.Parameters.AddWithValue("@password", user.getPassword());
                command.ExecuteNonQuery();
            }
        }
    }

    public void delete(User user)
    {
        using (var conexion = new SqliteConnection(_databasePath))
        {
            conexion.Open();
            string query = "DELETE FROM Users WHERE Username = @username";
            using (var command = new SqliteCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@Username", user.getUsername());
                command.ExecuteNonQuery();
            }
        }
    }

    public void update(User user)
    {
        using (var conexion = new SqliteConnection(_databasePath))
        {
            conexion.Open();
            string query = "UPDATE Users SET password = @password, username = @username WHERE Id = @id";
            using (var command = new SqliteCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@username", user.getUsername());
                command.Parameters.AddWithValue("@password", user.getPassword());
                command.Parameters.AddWithValue("@password", user.getId());
                command.ExecuteNonQuery();
            }
        }
    }

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
    
    public IEnumerable<User> getAll()
    {
        var lista = new List<User>();
        using (var conexion = new SqliteConnection(_databasePath))
        {
            conexion.Open();
            string query = "SELECT * FROM Users";
            using (var command = new SqliteCommand(query, conexion))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                   lista.Add(new User
                       (reader.GetInt32(0),reader.GetString(1), reader.GetString(2))
                   );
                }
            }
        }
        return lista;
    }

    private string _databasePath = "";
    private const string CREATE_QUERY = "CREATE TABLE IF NOT EXISTS Users (id INTEGER PRIMARY KEY, username TEXT, password TEXT)";
}