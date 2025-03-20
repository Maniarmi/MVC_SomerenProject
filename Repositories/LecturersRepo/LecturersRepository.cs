using Microsoft.Data.SqlClient;
using MVC_SomerenProject.Models;

namespace MVC_SomerenProject.Repositories.LecturersRepo
{
    public class LecturersRepository : ILecturersRepository
    {
        private readonly string? _connectionString;

        public LecturersRepository(IConfiguration configuration)
        {
            // get (database connectionstring from appsetings 
            _connectionString = configuration.GetConnectionString("MvcSomeren");
        }

        public void Add(Lecturers lecturers)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = $"INSERT INTO Lecturers (LecturerNumber, FirstName, LastName, PhoneNumber, Age)" +
                       "VALUES (@id, @FirstName, @LastName, @PhoneNumber, @Age); " +
                           "SELECT SCOPE_IDENTITY()";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Use parameters to prevent SQL Injection
                    command.Parameters.AddWithValue("@id", lecturers.LecturerNumber);
                    command.Parameters.AddWithValue("@FirstName", lecturers.FirstName);
                    command.Parameters.AddWithValue("@LastName", lecturers.LastName);
                    command.Parameters.AddWithValue("@PhoneNumber", lecturers.PhoneNumber);
                    command.Parameters.AddWithValue("@Age", lecturers.Age);

                    command.Connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && lecturers.LecturerNumber <= 0)
                    {
                        lecturers.LecturerNumber = Convert.ToInt32(result);
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message); // Debugging
                throw new Exception($"Error adding user: {ex.Message}");
            }

        }

        public void Update(Lecturers lecturers)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Lecturers SET FirstName = @FirstName, LastName = @LastName, " +
                       "PhoneNumber = @PhoneNumber, Age = @Age WHERE LecturerNumber = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", lecturers.LecturerNumber);
                command.Parameters.AddWithValue("@FirstName", lecturers.FirstName);
                command.Parameters.AddWithValue("@LastName", lecturers.LastName);
                command.Parameters.AddWithValue("@PhoneNumber", lecturers.PhoneNumber);
                command.Parameters.AddWithValue("@Age", lecturers.Age);

                command.Connection.Open();
                int nrOfRowsAffected = command.ExecuteNonQuery();
                if (nrOfRowsAffected <= 0)
                    throw new Exception("No records updated!");
            }
        }

        public void Delete(int lecturerNumber)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Lecturers WHERE LecturerNumber = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", lecturerNumber);

                command.Connection.Open();
                int nrOfRowsAffected = command.ExecuteNonQuery();
                if (nrOfRowsAffected <= 0)
                    throw new Exception("No records deleted!");
            }
        }

        private Lecturers ReadLecturers(SqlDataReader reader)
        {
            int lecturerNumber = (int)reader["LecturerNumber"];
            string firstName = (string)reader["FirstName"];
            string lastName = (string)reader["LastName"];
            string phoneNumber = (string)reader["PhoneNumber"];
            int age = (int)reader["Age"];

            return new Lecturers(lecturerNumber, firstName, lastName, phoneNumber, age);
        }

        public List<Lecturers> GetAll()
        {
            List<Lecturers> lecturers = new List<Lecturers>();

            //the connection string was set in the constructor 
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT LecturerNumber, FirstName, LastName, PhoneNumber, Age FROM Lecturers " +
                    "ORDER BY LastName";
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Lecturers lecturer = ReadLecturers(reader);
                    lecturers.Add(lecturer);
                }
                reader.Close();

            }
            return lecturers;
        }

        public Lecturers? GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT LecturerNumber, FirstName, LastName, PhoneNumber, Age FROM Lecturers WHERE LecturerNumber = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return ReadLecturers(reader);
                }

                return null;
            }
        }
    }
}
