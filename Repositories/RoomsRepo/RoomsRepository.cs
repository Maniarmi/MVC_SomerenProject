using Microsoft.Data.SqlClient;
using MVC_SomerenProject.Controllers;
using MVC_SomerenProject.Models;

namespace MVC_SomerenProject.Repositories.RoomsRepo
{
    public class RoomsRepository: IRoomsRepository
    {
        private readonly string? _connectionString;

        public RoomsRepository(IConfiguration configuration)
        {
            // get (database connectionstring from appsetings 
            _connectionString = configuration.GetConnectionString("MvcSomeren");
        }

        public void Add(Rooms rooms)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = $"INSERT INTO Rooms (RoomNumber, RoomSize, RoomType)" +
                       "VALUES (@id, @RoomSize, @RoomType); " +
                           "SELECT SCOPE_IDENTITY()";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Use parameters to prevent SQL Injection
                    command.Parameters.AddWithValue("@id", rooms.RoomNumber);
                    command.Parameters.AddWithValue("@RoomSize", rooms.RoomSize);
                    command.Parameters.AddWithValue("@RoomType", rooms.RoomType);
                  

                    command.Connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && rooms.RoomNumber <= 0)
                    {
                        rooms.RoomNumber = Convert.ToInt32(result);
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message); // Debugging
                throw new Exception($"Error adding room: {ex.Message}");
            }

        }

        public void Update(Rooms rooms)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Rooms SET RoomSize = @RoomSize, RoomType = @RoomType WHERE RoomNumber = @id "; 
                       

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", rooms.RoomNumber);
                command.Parameters.AddWithValue("@RoomSize", rooms.RoomSize);
                command.Parameters.AddWithValue("@RoomType", rooms.RoomType);
  

                command.Connection.Open();
                int nrOfRowsAffected = command.ExecuteNonQuery();
                if (nrOfRowsAffected <= 0)
                    throw new Exception("No records updated!");
            }
        }

        public void Delete(int roomNumber)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Rooms WHERE RoomNumber = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", roomNumber);

                command.Connection.Open();
                int nrOfRowsAffected = command.ExecuteNonQuery();
                if (nrOfRowsAffected <= 0)
                    throw new Exception("No records deleted!");
            }
        }

        private Rooms ReadRooms(SqlDataReader reader)
        {
            int roomNumber = (int)reader["RoomNumber"];
            int roomSize = (int)reader["RoomSize"];
            string roomType = (string)reader["RoomType"];
       

            return new Rooms(roomNumber, roomSize, roomType);
        }

        public List<Rooms> GetAll()
        {
            List<Rooms> rooms = new List<Rooms>();

            //the connection string was set in the constructor 
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT RoomNumber, RoomSize, RoomType FROM Rooms " +
                    "ORDER BY RoomNumber";
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Rooms room = ReadRooms(reader);
                    rooms.Add(room);
                }
                reader.Close();

            }
            return rooms;
        }

        public Rooms? GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT RoomNumber, RoomSize, RoomType FROM Rooms WHERE RoomNumber = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return ReadRooms(reader);
                }

                return null;
            }
        }
    }
}

