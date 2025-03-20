using MVC_SomerenProject.Models;

namespace MVC_SomerenProject.Repositories.LecturersRepo
{
    public interface ILecturersRepository
    {
        void Add(Lecturers lecturers);
        void Update(Lecturers lecturers);
        void Delete(int lecturerNumber);
        List<Lecturers> GetAll();
        Lecturers? GetById(int id);
    }
}
