using Microsoft.Data.SqlClient;
using MVC_SomerenProject.Models;

namespace MVC_SomerenProject.Repositories.ActivitiesRepo
{
	public class ActivitiesRepository : IActivitiesRepository
	{
		private readonly string? _connectionString;

		public ActivitiesRepository(IConfiguration configuration)
		{
			// get (database connectionstring from appsetings 
			_connectionString = configuration.GetConnectionString("MvcSomeren");
		}

		public void Add(Activities activities)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = $"INSERT INTO Activities (Name, Date, Description)" +
					   "VALUES (@Name, @Date, @Description);";
				SqlCommand command = new SqlCommand(query, connection);

				command.Parameters.AddWithValue("@Name", activities.Name);
				command.Parameters.AddWithValue("@Date", activities.Date);
				command.Parameters.AddWithValue("@Description", activities.Description);

				command.Connection.Open();
				int nrOfRowsAffected = command.ExecuteNonQuery();
				if (nrOfRowsAffected != 1)
					throw new Exception("Adding activity failed");
			}
		}

		public void Update(Activities activities)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = "UPDATE Activities SET Name = @Name, Date = @Date, " +
					   "Description = @Description WHERE Name = @Name";

				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Name", activities.Name);
				command.Parameters.AddWithValue("@Date", activities.Date);
				command.Parameters.AddWithValue("@Description", activities.Description);

				command.Connection.Open();
				int nrOfRowsAffected = command.ExecuteNonQuery();
				if (nrOfRowsAffected <= 0)
					throw new Exception("No records updated!");
			}
		}

		public void Delete(string Name)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = "DELETE FROM Activities WHERE Name = @Name";
				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Name", Name);

				command.Connection.Open();
				int nrOfRowsAffected = command.ExecuteNonQuery();
				if (nrOfRowsAffected == 0)
					throw new Exception("No records deleted!");
			}
		}

		private Activities ReadActivities(SqlDataReader reader)
		{
			string Description = (string)reader["Description"];
			string Date = (string)reader["Date"];
			string Name = (string)reader["Name"];

			return new Activities(Description, Date, Name);
		}

		public List<Activities> GetAll()
		{
			List<Activities> activities = new List<Activities>();

			//the connection string was set in the constructor 
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = "SELECT Description, Date, Name FROM Activities " +
					"ORDER BY Date";
				SqlCommand command = new SqlCommand(query, connection);

				command.Connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					Activities activity = ReadActivities(reader);
					activities.Add(activity);
				}
				reader.Close();

			}
			return activities;
		}

		public Activities? GetByName(string Name)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = "SELECT Name, Date, Description FROM Activities WHERE Name = @Name";
				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Name", Name);

				command.Connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					return ReadActivities(reader);
				}

				return null;
			}
		}
	}
}
