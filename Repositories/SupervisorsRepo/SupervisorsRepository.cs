using Microsoft.Data.SqlClient;
using MVC_SomerenProject.Models;
using System.Data;

namespace MVC_SomerenProject.Repositories.SupervisorsRepo
{
    public class SupervisorsRepository : ISupervisorsRepository
    {
        private readonly string? _connectionString;

        public SupervisorsRepository(IConfiguration configuration)
        {
            // get (database connectionstring from appsetings 
            _connectionString = configuration.GetConnectionString("MvcSomeren");
        }

        public void Add(string name, int lecturerNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = $"INSERT INTO Supervising (ActivityType, LNumber)" +
                       "VALUES (@Name, @LecturerNumber); " +
                       "SELECT SCOPE_IDENTITY()";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Use parameters to prevent SQL Injection
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@LecturerNumber", lecturerNumber);

                    command.Connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && lecturerNumber <= 0)
                    {
                        lecturerNumber = Convert.ToInt32(result);
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message); // Debugging
                throw new Exception($"Error adding Supervisor: {ex.Message}");
            }
        }

        public void Delete(int lecturerNumber)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Supervising WHERE LNumber = @LecturerNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LecturerNumber", lecturerNumber);

                command.Connection.Open();
                int nrOfRowsAffected = command.ExecuteNonQuery();
                if (nrOfRowsAffected <= 0)
                    throw new Exception("No records deleted!");
            }
        }

        private Supervisors ReadSupervisors(SqlDataReader reader)
        {
            string Name = (string)reader["name"];
            int lecturerNumber = (int)reader["LecturerNumber"];
            string firstName = (string)reader["FirstName"];
            string lastName = (string)reader["LastName"];


            return new Supervisors(Name, lecturerNumber, firstName, lastName);
        }

        public List<Supervisors> GetSupervisors()
        {
            //var x = testColumnName("Supervising");


            List<Supervisors> noSupervisors = new List<Supervisors>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                                SELECT 
                                L.LecturerNumber AS LecturerNumber, 
                                L.FirstName AS FirstName, 
                                L.LastName AS LastName
                                FROM 
                                Lecturers AS L
                                JOIN Supervising AS S 
                                ON 
                                L.LecturerNumber = S.LNumber
                                ";

                // string query = @"Select * from Supervising";

                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int lecturerNumber = (int)reader["LecturerNumber"];
                        string firstName = (string)reader["FirstName"];
                        string lastName = (string)reader["LastName"];

                        noSupervisors.Add(new Supervisors("", lecturerNumber, firstName, lastName));
                    }

                }
                
                reader.Close();
            }
            return noSupervisors;


        }

        public List<Supervisors> GetNoSupervisors()
        {
            List<Supervisors> noSupervisors = new List<Supervisors>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                                SELECT 
                                L.LecturerNumber AS LecturerNumber, 
                                L.FirstName AS FirstName, 
                                L.LastName AS LastName
                                FROM 
                                Lecturers AS L
                                LEFT JOIN Supervising AS S 
                                ON 
                                L.LecturerNumber = S.LNumber
                                WHERE S.LNumber IS NULL";

                // string query = @"Select * from Supervising";

                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int lecturerNumber = (int)reader["LecturerNumber"];
                    string firstName = (string)reader["FirstName"];
                    string lastName = (string)reader["LastName"];

                    noSupervisors.Add(new Supervisors("", lecturerNumber, firstName, lastName));
                }
                reader.Close();
            }
            return noSupervisors;
        }

        public Supervisors? GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT Name, LecturerNumber, FirstName, LastName " +
                    "FROM Supervising AS S " +
                    "JOIN Activities AS A ON A.Name = S.Name " +
                    "JOIN Lecturers AS L ON L.LecturerNumber = S.LNumber " +
                    "WHERE LecturerNumber = @LecturerNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LecturerNumber", id);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return ReadSupervisors(reader);
                }

                return null;
            }
        }

      
    }
}

