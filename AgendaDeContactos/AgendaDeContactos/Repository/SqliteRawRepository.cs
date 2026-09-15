using AgendaDeContactos.Data.Entities;
using AgendaDeContactos.Exceptions;
using Microsoft.Data.Sqlite;
// Imports

namespace AgendaDeContactos.Repository;

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

    public IEnumerable<Contact> GetAll(int userId)
    {
        var list = new List<Contact>();
        using (var conexion = new SqliteConnection(_databasePath))
        {
            conexion.Open();
            string query = $"SELECT * FROM Contacs WHERE user_id = @user_id";
            using (var command = new SqliteCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@user_id", userId);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(
                            new Contact(reader.GetInt32(0),
                                reader.GetInt32(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5),
                                reader.GetString(6))
                        );
                    }
                }
            }
        }

        return list;
    }

    public void CreateContact(Contact contact)
    {
        ValidateConstrain(contact.GetPhone(), contact.GetUserId());
        using (var conexion = new SqliteConnection(_databasePath))
        {
            conexion.Open();
            string query =
                "INSERT INTO Contacs (user_id, name, lastname, alias, email, phone_number) values (@user_id, @name, @lastname, @alias, @email, @phone_number)";
            using (var command = new SqliteCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@user_id", contact.GetUserId());
                command.Parameters.AddWithValue("@name", contact.GetName());
                command.Parameters.AddWithValue("@lastname", contact.GetLastName());
                command.Parameters.AddWithValue("@alias", contact.GetAlias());
                command.Parameters.AddWithValue("@email", contact.GetEmail());
                command.Parameters.AddWithValue("@phone_number", contact.GetPhone());
                command.ExecuteNonQuery();

            }
        }
    }
    // Validar la unicidad de el número de telefono y el id del usuario dueño de la agenda
    // así un mismo usario no tiene agregado dos veces el mismo telefono 
    private void ValidateConstrain(string phoneNumber, int userId)
    {
        using (var conexion = new SqliteConnection(_databasePath))
        {
            conexion.Open();
            string query = $"SELECT COUNT(*) FROM Contacs WHERE user_id = @user_id AND phone_number = @phone_number";
            using (var command = new SqliteCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@user_id", userId);
                command.Parameters.AddWithValue("@phone_number", phoneNumber);
                var result = (long)command.ExecuteScalar();
                long quantity = result != null ? Convert.ToInt64(result) : 0;
                
                if (quantity > 0)
                    throw new GeneralException<ContactExceptions>(ContactExceptions.THERE_IS_A_CONTACT_FOR_THAT_NUMBER);
            }
        }
    }

    public void DeleteContact(Contact contact)
    {
        using (var conexion = new SqliteConnection(_databasePath))
        {
            conexion.Open();
            string query = "DELETE FROM Contacs WHERE user_id = @user_id AND phone_number = @phone_number";
            using (var command = new SqliteCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@user_id", contact.GetUserId());
                command.Parameters.AddWithValue("@phone_number", contact.GetPhone());
                int update = command.ExecuteNonQuery();
                if (update == 0)
                    throw new GeneralException<ContactExceptions>(ContactExceptions.ERROR_DELETING_THE_CONTACT);
            }
        }
    }

    public void UpdateContact(Contact contact)
    {
        using (var conexion = new SqliteConnection(_databasePath))
        {
            conexion.Open();
            string query = $"UPDATE Contacs set name = @name, lastname = @lastname, phone_number = @phone_number, email = @email, alias = @alias WHERE id = @id";
            using (var command = new SqliteCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@id", contact.GetID());
                command.Parameters.AddWithValue("@name", contact.GetName());
                command.Parameters.AddWithValue("@lastname", contact.GetLastName());
                command.Parameters.AddWithValue("@phone_number", contact.GetPhone());
                command.Parameters.AddWithValue("@email", contact.GetEmail());
                command.Parameters.AddWithValue("@alias", contact.GetAlias());
                int update = command.ExecuteNonQuery();
                if (update == 0)
                    throw new GeneralException<ContactExceptions>(ContactExceptions.ERROR_DELETING_THE_CONTACT);
            }
        }
    }
    public void DeleteAllContacts(Contact contact)
    {
        using (var conexion = new SqliteConnection(_databasePath))
        {
            conexion.Open();
            string query = "DELETE FROM Contacs WHERE user_id = @user_id";
            using (var command = new SqliteCommand(query, conexion))
            {
                command.Parameters.AddWithValue("@user_id", contact.GetUserId());
                int update = command.ExecuteNonQuery();
                if (update == 0)
                    throw new GeneralException<ContactExceptions>(ContactExceptions.ERROR_DELETING_THE_CONTACTS);
            }
        }
    }

    private string _databasePath = "";
    private const string CREATE_QUERY = "CREATE TABLE IF NOT EXISTS Contacs (id INTEGER PRIMARY KEY, user_id INTEGER, name TEXT, lastname TEXT, alias TEXT, email TEXT, phone_number TEXT, UNIQUE(phone_number, user_id))";
}