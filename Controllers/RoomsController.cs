using Microsoft.AspNetCore.Mvc;
using MVC_SomerenProject.Models;
using MVC_SomerenProject.Repositories.RoomsRepo;

namespace MVC_SomerenProject.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IRoomsRepository _roomsRepository;

        public RoomsController(IRoomsRepository roomsRepository)
        {
            _roomsRepository = roomsRepository; 
        }


        public IActionResult Index()
        {
            List<Rooms> rooms = _roomsRepository.GetAll();
            if (rooms == null || rooms.Count == 0)
            {
                return View(new List<Rooms>()); // Return empty list instead of null
            }
            return View(rooms);
        }

        //Get: StudentsController/Create 
        [HttpGet]
        public ActionResult Add()
        {
            return View("CreateRoom");
        }

        [HttpPost]
        public IActionResult Add(Rooms rooms)
        {
            try
            {
                _roomsRepository.Add(rooms);
                return View("CreateRoom", rooms);
            }
            catch
            {
                return BadRequest(new { message = "Room Is Not added" });
            }

        }

    }
}
