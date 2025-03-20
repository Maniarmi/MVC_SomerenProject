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
                return View(new List<Lecturers>()); // Return empty list instead of null
            }
            return View(lecturers);
        }

        //Get: StudentsController/Create 
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

    //    //Get: StudentsController/Edit
    //    //[HttpGet]
    //    public ActionResult Edit(int? StudentNumber)
    //    {
    //        if (StudentNumber == null)
    //        {
    //            return NotFound();
    //        }
    //        //Get user via repository 
    //        Students? students = _studentsRepository.GetById((int)StudentNumber);
    //        return View("EditStudent", students);
    //    }

    //    //Post : StudentsController/Edit 
    //    //[HttpPost]
    //    public ActionResult Edit(Students students)
    //    {
    //        try
    //        {
    //            //update user via repository 
    //            _studentsRepository.Update(students);

    //            //go back to user list (via Index) 
    //            return RedirectToAction("Index");
    //        }
    //        catch (Exception ex)
    //        {
    //            //something's wrong, go back to view with user 
    //            return View("EditStudent", students);
    //        }
    //    }

    //    //Get: Studentsontroller/Delete
    //    [HttpGet]
    //    public ActionResult Delete(int? studentNumber)
    //    {
    //        if (studentNumber == null)
    //        {
    //            return NotFound();
    //        }

    //        //Get user via repository 
    //        Students? students = _studentsRepository.GetById((int)studentNumber);
    //        return View("DeleteStudent", students);
    //    }

    //    //Post : UserController/Delete 
    //    [HttpPost]
    //    public ActionResult Delete(int students)
    //    {
    //        try
    //        {
    //            //Delete user via repository 
    //            _studentsRepository.Delete(students);

    //            //go back to user list (via Index) 
    //            return RedirectToAction("Index");
    //        }
    //        catch (Exception ex)
    //        {
    //            //something's wrong, go back to view with user 
    //            return View("DeleteStudent", students);
    //        }
    //    }
    //}
}
}
