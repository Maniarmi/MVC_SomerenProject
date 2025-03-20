using Microsoft.AspNetCore.Mvc;
using MVC_SomerenProject.Models;
using MVC_SomerenProject.Repositories.LecturersRepo;

namespace MVC_SomerenProject.Controllers
{
	public class LecturersController : Controller
	{
		private readonly ILecturersRepository _lecturersRepository;

		public LecturersController(ILecturersRepository lecturersRepository)
		{
			_lecturersRepository = lecturersRepository;
		}


		public IActionResult Index()
		{
			List<Lecturers> lecturers = _lecturersRepository.GetAll();
			if (lecturers == null || lecturers.Count == 0)
			{
				return View(new List<Lecturers>()); 
			}
			return View(lecturers);
		}

		
		[HttpGet]
		public ActionResult Add()
		{
			return View("CreateLecturer");
		}

		[HttpPost]
		public IActionResult Add(Lecturers lecturers)
		{
			try
			{
				_lecturersRepository.Add(lecturers);
				return View("CreateLecturer", lecturers);
			}
			catch
			{
				return BadRequest(new { message = "Lecturer Is Not added" });
			}

		}

		[HttpGet]
		public ActionResult Edit(int? Id)
		{
			if (Id == null)
			{
				return NotFound();
			}
			
			Lecturers? lecturers = _lecturersRepository.GetById((int)Id);
			return View("EditLecturer", lecturers);
		}

		 
		[HttpPost]
		public ActionResult Edit(Lecturers lecturers)
		{
			try
			{ 
				_lecturersRepository.Update(lecturers);
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				return View("EditLecturer", lecturers);
			}
		}

		
		[HttpGet]
		public ActionResult Delete(int? Id)
		{
			if (Id == null)
			{
				return NotFound();
			}

			Lecturers? lecturers = _lecturersRepository.GetById((int)Id);
			return View("DeleteLecturer", lecturers);
		}

		 
		[HttpPost]
		public ActionResult Delete(Lecturers lecturers)
		{
			try
			{
				_lecturersRepository.Delete(lecturers.LecturerNumber);
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				return View("DeleteLecturer", lecturers);
			}
		}
	}
}

