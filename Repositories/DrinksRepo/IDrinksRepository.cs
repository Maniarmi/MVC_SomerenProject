using MVC_SomerenProject.Models;

namespace MVC_SomerenProject.Repositories.DrinksRepo
{
    public interface IDrinksRepository
    {
        List<Drinks> GetAllDrinks();
        Drinks? GetDrinkById(int drinkId);
        void AddDrink(Drinks drink);

        void DeleteDrink(Drinks drink);
        void OrderDrink(DrinkOrder drinkOrder);

    }
}
