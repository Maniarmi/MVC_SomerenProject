using MVC_SomerenProject.Models;

namespace MVC_SomerenProject.Repositories.StudentsRepo
{
    public interface IStudentsRepository
    {
        void Add(Students students);    
        void Update(Students students); 
        void Delete(int studentNumber);    
        List<Students> GetAll();
        Students? GetById(int id);
    }
}
