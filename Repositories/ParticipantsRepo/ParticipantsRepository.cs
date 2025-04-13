using Microsoft.Data.SqlClient;
using MVC_SomerenProject.Models;

namespace MVC_SomerenProject.Repositories.ParticipantsRepo
{
    public class ParticipantsRepository : IParticipantsRepository
    {
        private readonly string? _connectionString;

        public ParticipantsRepository(IConfiguration configuration)
        {
            // get (database connectionstring from appsetings 
            _connectionString = configuration.GetConnectionString("MvcSomeren");
        }

        public void Add(Participants participants)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = $"INSERT INTO Participation (ActivityType, SNumber)" +
                       "VALUES (@Name, @StudentNumber); " +
                           "SELECT SCOPE_IDENTITY()";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Use parameters to prevent SQL Injection
                    command.Parameters.AddWithValue("@Name", participants.Name);
                    command.Parameters.AddWithValue("@StudentNumber", participants.StudentNumber);

                    command.Connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && participants.StudentNumber <= 0)
                    {
                        participants.StudentNumber = Convert.ToInt32(result);
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message); // Debugging
                throw new Exception($"Error adding participant: {ex.Message}");
            }

        }

        public void Delete(int studentNumber, string name)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Participation WHERE SNumber = @StudentNumber AND ActivityType = @Name";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentNumber", studentNumber);
                command.Parameters.AddWithValue("@Name", name);

                command.Connection.Open();
                int nrOfRowsAffected = command.ExecuteNonQuery();
                if (nrOfRowsAffected <= 0)
                    throw new Exception("No records deleted!");
            }
        }

        private Participants ReadParticipants(SqlDataReader reader)
        {
            string Name = (string)reader["Name"];
            string Description = (string)reader["Description"];
            int StudentNumber = (int)reader["StudentNumber"];
            string FirstName = (string)reader["FirstName"];
            string LastName = (string)reader["LastName"];

            return new Participants(Name, Description, StudentNumber, FirstName, LastName);
        }

        public List<Participants> GetAll(string Name)
        {
            List<Participants> participants = new List<Participants>();

            //the connection string was set in the constructor 
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT DISTINCT [Name], [Description], StudentNumber, FirstName, LastName " +
                    "FROM Participation AS P " +
                    "JOIN Activities AS A ON A.[Name] = P.ActivityType " +
                    "JOIN Students AS S ON S.StudentNumber = P.SNumber " +
                    "WHERE [Name] = @Name " +
                    "ORDER BY LastName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", Name);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Participants participant = ReadParticipants(reader);
                    participants.Add(participant);
                }
                reader.Close();

            }
            return participants;
        }

        public List<Participants> GetAllNotParticipating(string Name)
        {
            List<Participants> participants = new List<Participants>();

            //the connection string was set in the constructor 
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT DISTINCT [Name], [Description], StudentNumber, FirstName, LastName " +
                    "FROM Participation AS P " +
                    "JOIN Activities AS A ON A.[Name] = P.ActivityType " +
                    "JOIN Students AS S ON S.StudentNumber = P.SNumber " +
                    "WHERE [Name] != @Name OR [Name] IS NULL " +
                    "ORDER BY LastName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", Name);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Participants participant = ReadParticipants(reader);
                    participants.Add(participant);
                }
                reader.Close();

            }
            return participants;
        }

        public Participants? GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT DISTINCT [Name], [Description], StudentNumber, FirstName, LastName " +
                    "FROM Participation AS P " +
                    "JOIN Activities AS A ON A.[Name] = P.ActivityType " +
                    "JOIN Students AS S ON S.StudentNumber = P.SNumber " +
                    "WHERE StudentNumber = @StudentNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentNumber", id);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return ReadParticipants(reader);
                }

                return null;
            }
        }
    }
}

