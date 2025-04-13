using Microsoft.AspNetCore.Mvc;
using MVC_SomerenProject.Models;
using MVC_SomerenProject.Repositories.ActivitiesRepo;
using MVC_SomerenProject.Repositories.LecturersRepo;
using MVC_SomerenProject.Repositories.SupervisorsRepo;
using MVC_SomerenProject.ViewModels;

namespace MVC_SomerenProject.Controllers
{
	public class ActivitiesController : Controller
	{
		private readonly IActivitiesRepository _activitiesRepository;
		private readonly ILecturersRepository _lecturersRepository;
		private readonly ISupervisorsRepository _supervisorsRepository;	

		public ActivitiesController(IActivitiesRepository activitiesRepository, ILecturersRepository lecturersRepository, ISupervisorsRepository supervisorsRepository)
		{
			_activitiesRepository = activitiesRepository;
			_lecturersRepository = lecturersRepository;
			_supervisorsRepository = supervisorsRepository;	
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

		// Manage activity supervisors 

		public IActionResult ManageSupervisors(string name)
		{
			var activities = _activitiesRepository.GetByName(name);	
			if (activities == null)
			{
				return NotFound();	
			}
            ViewBag.Activity = activities;
            var queryResult = new LectureWithActiveityViewModel();
            var supervisorsList = _supervisorsRepository.GetSupervisors();
			if(supervisorsList != null)
			{
				var supervisors = supervisorsList.Select(x => new LectureWithActiveitySupervisorViewModel
                {
                    FirstName = x.FirstName ?? "",
                    LastName = x.LastName ?? "",
                    LecturerNumber = x.LecturerNumber,
                }).ToList();
				queryResult.Supervisor = supervisors;
            }



			var nonSupervisorsList = _supervisorsRepository.GetNoSupervisors();
			if(nonSupervisorsList != null)
			{
				var nonSupervisors = nonSupervisorsList.Select(x => new LectureWithActiveityNonSupervisorViewModel
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    LecturerNumber = x.LecturerNumber
                }).ToList();
				queryResult.NonSupervisor = nonSupervisors;
            }


			return View("ManageSupervisor" , queryResult == null ? "" : queryResult);	
		}

		// Add Supervisor to an activity 
		[HttpPost]
		//[ValidateAntiForgeryToken]
		public IActionResult AddSupervisor(string name, int lecturerNumber)
		{
			try
			{
				_supervisorsRepository.Add(name, lecturerNumber); 
				return RedirectToAction("ManageSupervisors" , new {name = name});	
			}
			catch (Exception ex)
			{
				ViewBag.ErrorMessage = "Could Not Add Supervisors"; 
				return BadRequest(ex.Message);	
			}
		}


        [HttpPost]
        public IActionResult RemoveSupervisor(string name, int lecturerNumber)
		{
            try
            {
                _supervisorsRepository.Delete(lecturerNumber);
                return RedirectToAction("ManageSupervisors", new { name = name });
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Could Not Remove Supervisors";
                return View("Error");
            }
        }

	}
}
