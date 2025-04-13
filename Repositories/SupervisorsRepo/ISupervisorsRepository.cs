using MVC_SomerenProject.Models;

namespace MVC_SomerenProject.Repositories.SupervisorsRepo
{
    public interface ISupervisorsRepository
    {
        void Add(string id, int lecturerNumber);
        void Delete(int lecturerNumber);
        List<Supervisors> GetSupervisors();
        List<Supervisors> GetNoSupervisors();
        Supervisors? GetById(int id);
    }
}
