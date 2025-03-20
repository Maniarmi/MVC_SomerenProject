using MVC_SomerenProject.Models;

namespace MVC_SomerenProject.Repositories.RoomsRepo
{
    public interface IRoomsRepository
    {
        void Add(Rooms rooms);
        void Update(Rooms rooms);
        void Delete(int roomNumber);
        List<Rooms> GetAll();
        Rooms? GetById(int id);
    }
}
