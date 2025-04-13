using Microsoft.AspNetCore.Mvc;
using MVC_SomerenProject.Models;
using MVC_SomerenProject.Repositories.DrinksRepo;
using MVC_SomerenProject.Repositories.StudentsRepo;
using static MVC_SomerenProject.Models.Drinks;

namespace MVC_SomerenProject.Controllers
{
    public class DrinksController : Controller
    {
        private readonly IDrinksRepository _drinksRepository;
        private readonly IStudentsRepository _studentsRepository;

        public DrinksController(IDrinksRepository drinksRepository, IStudentsRepository studentsRepository)
        {
            _drinksRepository = drinksRepository;
            _studentsRepository = studentsRepository;
        }

        public IActionResult Index()
        {
            var drinks = _drinksRepository.GetAllDrinks();
            var students = _studentsRepository.GetAll();

            ViewBag.Students = students ?? new List<Students>();

            return View(drinks);


        }



        // ADD DRINK
        [HttpGet]
        public IActionResult AddDrink()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddDrink(Drinks drink)
        {
            try
            {
                _drinksRepository.AddDrink(drink);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View(drink);
            }
        }



        // DELETE DRINK



        [HttpGet]
        public IActionResult DeleteDrink(int id)
        {
            var drink = _drinksRepository.GetDrinkById(id);
            if (drink == null)
            {
                return NotFound("Drink not found.");
            }
            return View(drink);
        }

        [HttpPost]
        public IActionResult DeleteDrink(Drinks drink)
        {
            try
            {


                _drinksRepository.DeleteDrink(drink);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View(drink);
            }
        }




        //  Order drink

        [HttpGet]
        public IActionResult OrderDrink(int id)
        {
            var student = _studentsRepository.GetById(id);
            if (student == null)
            {
                Console.WriteLine($"Error: No Student found with ID: {id}");
                return NotFound("Student not found.");
            }
            ViewBag.Drinks = _drinksRepository.GetAllDrinks();
            ViewBag.DrinkOrder = new DrinkOrder();
            return View(student);
        }
        [HttpPost]
        public IActionResult OrderDrink(Students students, int drinkId, int quantity, string confirmation)
        {
            if (confirmation == "no")
            {
                return RedirectToAction("Index");
            }
            var drinkOrder = new DrinkOrder { StudentNumber = students.StudentNumber, DrinkId = drinkId, Quantity = quantity };
            try
            {
                _drinksRepository.OrderDrink(drinkOrder);

                ViewBag.Drinks = _drinksRepository.GetAllDrinks();
                ViewBag.DrinkOrder = new DrinkOrder();
                return View(students);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                ViewBag.Drinks = _drinksRepository.GetAllDrinks();
                ViewBag.DrinkOrder = new DrinkOrder();
                return View(students);
            }
        }
    }

}

