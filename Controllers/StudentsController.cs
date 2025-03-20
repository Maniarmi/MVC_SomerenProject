using Microsoft.AspNetCore.Mvc;
using MVC_SomerenProject.Models;
using MVC_SomerenProject.Repositories.StudentsRepo;

namespace MVC_SomerenProject.Controllers
{
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

        
        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            //Get students via repository 
            Students? students = _studentsRepository.GetById((int)Id);
            return View("EditStudent", students);
        }

        
        [HttpPost]
        public ActionResult Edit(Students students)
        {
            try
            {
                //update via repository 
                _studentsRepository.Update(students);

                //go back to list (via Index) 
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //something's wrong, go back to view 
                return View("EditStudent", students);
            }
        }

        
        [HttpGet]
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            
            Students? students = _studentsRepository.GetById((int)Id);
            return View("DeleteStudent", students);
        }

        
        [HttpPost]
        public ActionResult Delete(Students students)
        {
            try
            {
                _studentsRepository.Delete(students.StudentNumber); 
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            { 
                return View("DeleteStudent", students);
            }
        }
    }
}
