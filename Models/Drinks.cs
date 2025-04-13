namespace MVC_SomerenProject.Models
{
    public class Drinks
    {
        public int DrinkId { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public string? Category { get; set; }

        public int StudentNumber { get; set; }

        public Drinks(int drinkId, string name, int stock, string? category)
        {
            DrinkId = drinkId;
            Name = name;
            Stock = stock;
            Category = category;
        }
        public Drinks()
        {


        }
    }
}
