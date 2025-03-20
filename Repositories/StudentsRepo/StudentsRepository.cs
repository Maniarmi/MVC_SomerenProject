using Microsoft.Data.SqlClient;
using MVC_SomerenProject.Models;

namespace MVC_SomerenProject.Repositories.StudentsRepo
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly string? _connectionString;

        public StudentsRepository(IConfiguration configuration)
        {
            // get (database connectionstring from appsetings 
            _connectionString = configuration.GetConnectionString("MvcSomeren");
        }

        public void Add(Students students)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = $"INSERT INTO Students (StudentNumber, FirstName, LastName, PhoneNumber, Class)" +
                       "VALUES (@id, @FirstName, @LastName, @PhoneNumber, @Class); " +
                           "SELECT SCOPE_IDENTITY()";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Use parameters to prevent SQL Injection
                    command.Parameters.AddWithValue("@id", students.StudentNumber);
                    command.Parameters.AddWithValue("@FirstName", students.FirstName);
                    command.Parameters.AddWithValue("@LastName", students.LastName);
                    command.Parameters.AddWithValue("@PhoneNumber", students.PhoneNumber);
                    command.Parameters.AddWithValue("@Class", students.Class);

                    command.Connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && students.StudentNumber <= 0)
                    {
                        students.StudentNumber = Convert.ToInt32(result);
                    }
                    
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message); // Debugging
                throw new Exception($"Error adding user: {ex.Message}");
            }

        }

        public void Update(Students students)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Students SET FirstName = @FirstName, LastName = @LastName, " +
                       "PhoneNumber = @PhoneNumber, Class = @Class WHERE StudentNumber = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", students.StudentNumber);
                command.Parameters.AddWithValue("@FirstName", students.FirstName);
                command.Parameters.AddWithValue("@LastName", students.LastName);
                command.Parameters.AddWithValue("@PhoneNumber", students.PhoneNumber);
                command.Parameters.AddWithValue("@Class", students.Class);

                command.Connection.Open();
                int nrOfRowsAffected = command.ExecuteNonQuery();
                if (nrOfRowsAffected <= 0)
                    throw new Exception("No records updated!");
            }
        }

        public void Delete(int studentNumber)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Students WHERE StudentNumber = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", studentNumber);

                command.Connection.Open();
                int nrOfRowsAffected = command.ExecuteNonQuery();
                if (nrOfRowsAffected <= 0)
                    throw new Exception("No records deleted!");
            }
        }

        private Students ReadStudents(SqlDataReader reader)
        {
            int StudentNumber = (int)reader["StudentNumber"];
            string FirstName = (string)reader["FirstName"];
            string LastName = (string)reader["LastName"];
            string PhoneNumber = (string)reader["PhoneNumber"]; 
            string Class = (string)reader["Class"];

            return new Students(StudentNumber, FirstName, LastName, PhoneNumber, Class);
        }

        public List<Students> GetAll()
        {
            List<Students> students = new List<Students>();

            //the connection string was set in the constructor 
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT StudentNumber, FirstName, LastName, PhoneNumber, Class FROM Students " +
                    "ORDER BY LastName";
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Students student = ReadStudents(reader);
                    students.Add(student);
                }
                reader.Close();

            }
            return students;
        }

        public Students? GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT StudentNumber, FirstName, LastName, PhoneNumber, Class FROM Students WHERE StudentNumber = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return ReadStudents(reader);
                }

                return null;
            }
        }

    }
}
