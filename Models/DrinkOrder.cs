namespace MVC_SomerenProject.Models
{
    public class DrinkOrder
    {

        public int ConsumptionId { get; set; }
        public int DrinkId { get; set; }
        public int StudentNumber { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderTime { get; set; }


        public DrinkOrder(int consumptionId, int drinkId, int StudentNumber, int quantity, DateTime orderTime)
        {
            ConsumptionId = consumptionId;
            DrinkId = drinkId;
            this.StudentNumber = StudentNumber;
            Quantity = quantity;
            OrderTime = orderTime;
        }
        public DrinkOrder()
        {

        }


    }
}
