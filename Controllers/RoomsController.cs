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
				return View(new List<Rooms>()); 
			}
			return View(rooms);
		}

		 
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

		[HttpGet]
		public ActionResult Edit(int? Id)
		{
			if (Id == null)
			{
				return NotFound();
			}
			
			Rooms? rooms = _roomsRepository.GetById((int)Id);
			return View("EditRoom", rooms);
		}


		[HttpPost]
		public ActionResult Edit(Rooms rooms)
		{
			try
			{ 
				_roomsRepository.Update(rooms); 
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				return View("EditRoom", rooms);
			}
		}

		[HttpGet]
		public ActionResult Delete(int? Id)
		{
			if (Id == null)
			{
				return NotFound();
			}

			Rooms? rooms = _roomsRepository.GetById((int)Id);
			return View("DeleteRoom", rooms);
		}

	
		[HttpPost]
		public ActionResult Delete(Rooms rooms)
		{
			try
			{
				_roomsRepository.Delete(rooms.RoomNumber); 
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				return View("DeleteRoom", rooms);
			}
		}
	}
}

