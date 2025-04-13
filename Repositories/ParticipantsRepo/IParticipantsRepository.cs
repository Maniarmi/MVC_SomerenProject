using MVC_SomerenProject.Models;

namespace MVC_SomerenProject.Repositories.ParticipantsRepo
{
    public interface IParticipantsRepository
    {
        void Add(Participants participants);
        void Delete(int studentNumber, string name);
        List<Participants> GetAll(string Name);
        List<Participants> GetAllNotParticipating(string Name);
        Participants? GetById(int id);
    }
}
