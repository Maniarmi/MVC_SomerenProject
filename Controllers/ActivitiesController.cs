using Microsoft.AspNetCore.Mvc;
using MVC_SomerenProject.Models;
using MVC_SomerenProject.Repositories.ActivitiesRepo;

namespace MVC_SomerenProject.Controllers
{
	public class ActivitiesController : Controller
	{
		private readonly IActivitiesRepository _activitiesRepository;

		public ActivitiesController(IActivitiesRepository activitiesRepository)
		{
			_activitiesRepository = activitiesRepository;
		}

		public IActionResult Index()
		{
			List<Activities> activities = _activitiesRepository.GetAll();
			if (activities == null || activities.Count == 0)
			{
				return View(new List<Activities>()); // Return empty list instead of null
			}
			return View(activities);
		}

		[HttpGet]
		public ActionResult Add()
		{
			return View("CreateActivity");
		}

		[HttpPost]
		public IActionResult Add(Activities activities)
		{
			try
			{
				_activitiesRepository.Add(activities);
				return View("CreateActivity", activities);
			}
			catch
			{
				return BadRequest(new { message = "Activity Is Not added" });
			}

		}

		[HttpGet]
		public ActionResult Edit(string? Id)
		{
			if (Id == null)
			{
				return NotFound();
			}

			Activities? activities = _activitiesRepository.GetByName((string)Id);
			return View("EditActivity", activities);
		}


		[HttpPost]
		public ActionResult Edit(Activities activities)
		{
			try
			{
				_activitiesRepository.Update(activities);
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				return View("EditActivity", activities);
			}
		}


		[HttpGet]
		public ActionResult Delete(string? Id)
		{
			if (Id == null)
			{
				return NotFound();
			}

			Activities? activities = _activitiesRepository.GetByName((string)Id);
			return View("DeleteActivity", activities);
		}


		[HttpPost]
		public ActionResult Delete(Activities activities)
		{
			try
			{
				_activitiesRepository.Delete(activities.Name);
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				return View("DeleteActivity", activities);
			}
		}

	}
}
