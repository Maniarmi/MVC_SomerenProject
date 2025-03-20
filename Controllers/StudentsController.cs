using Microsoft.AspNetCore.Mvc;
using MVC_SomerenProject.Models;
using MVC_SomerenProject.Repositories.StudentsRepo;

namespace MVC_SomerenProject.Controllers
{
    //[Route ("Students")]
    public class StudentsController : Controller
    {
        private readonly IStudentsRepository _studentsRepository;

        public StudentsController(IStudentsRepository studentRepository)
        {
            _studentsRepository = studentRepository;
        }

    
        public IActionResult Index()
        {
            List<Students> students = _studentsRepository.GetAll();
            if (students == null || students.Count == 0)
            {
                return View(new List<Students>()); // Return empty list instead of null
            }
            return View(students);
        }

        //Get: StudentsController/Create 
        [HttpGet]
        public ActionResult Add()
        {
            return View("CreateStudent");
        }

       [HttpPost]
        public IActionResult Add(Students student)
        {
            try
            {
                _studentsRepository.Add(student);
                return View("CreateStudent", student);

            }
            catch
            {
                return BadRequest(new { message = "Student Is Not added" });
            }

        }

        //Get: StudentsController/Edit
        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            //Get user via repository 
            Students? students = _studentsRepository.GetById((int)Id);
            return View("EditStudent", students);
        }

        //Post : StudentsController/Edit 
        [HttpPost]
        public ActionResult Edit(Students students)
        {
            try
            {
                //update user via repository 
                _studentsRepository.Update(students);

                //go back to user list (via Index) 
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //something's wrong, go back to view with user 
                return View("EditStudent", students);
            }
        }

        //Get: Studentsontroller/Delete
        [HttpGet]
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            //Get user via repository 
            Students? students = _studentsRepository.GetById((int)Id);
            return View("DeleteStudent", students);
        }

        //Post : UserController/Delete 
        [HttpPost]
        public ActionResult Delete(Students students)
        {
            try
            {
                //Delete user via repository 
                _studentsRepository.Delete(students.StudentNumber);

                //go back to user list (via Index) 
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //something's wrong, go back to view with user 
                return View("DeleteStudent", students);
            }
        }
    }
}
