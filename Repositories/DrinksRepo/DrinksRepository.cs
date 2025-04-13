using Microsoft.Data.SqlClient;
using MVC_SomerenProject.Models;

namespace MVC_SomerenProject.Repositories.DrinksRepo
{
    public class DrinksRepository : IDrinksRepository
    {
        private readonly string _connectionString;

        public DrinksRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MvcSomeren");
        }


        List<Drinks> IDrinksRepository.GetAllDrinks()
        {
            List<Drinks> drinks = new List<Drinks>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM DRINK";
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Drinks drink = new Drinks
                            {
                                DrinkId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Category = reader.GetString(2),
                                Stock = reader.GetInt32(3)
                            };
                            drinks.Add(drink);
                        }
                    }
                }
                return drinks;
            }

        }

        public Drinks? GetDrinkById(int drinkId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM DRINK WHERE drink_id = @DrinkId";
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DrinkId", drinkId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Drinks
                            {
                                DrinkId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Category = reader.GetString(2),
                                Stock = reader.GetInt32(3)
                            };
                        }
                    }
                }
            }
            return null;
        }


        void IDrinksRepository.AddDrink(Drinks drink)
        {
            Drinks newDrink = drink;
            if (newDrink != null)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO DRINK " +
                                   "VALUES (@Name, @Category, @Stock)";
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", newDrink.Name);
                        command.Parameters.AddWithValue("@Category", newDrink.Category);
                        command.Parameters.AddWithValue("@Stock", newDrink.Stock);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }




        void IDrinksRepository.DeleteDrink(Drinks drink)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM DRINK WHERE drink_id = @DrinkId;";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                command.Parameters.AddWithValue("@DrinkId", drink.DrinkId);

                command.ExecuteNonQuery();

            }

        }




        void IDrinksRepository.OrderDrink(DrinkOrder drinkOrder)
        {
            DrinkOrder newDrinkOrder = drinkOrder;
            if (newDrinkOrder != null)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO DrinkConsumption " +
                                   "VALUES (@StudentNumber, @DrinkId, @Quantity); ";
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentNumber", newDrinkOrder.StudentNumber);
                        command.Parameters.AddWithValue("@DrinkId", newDrinkOrder.DrinkId);
                        command.Parameters.AddWithValue("@Quantity", newDrinkOrder.Quantity);
                        command.ExecuteNonQuery();
                    }
                }
            }

        }



    }
}

