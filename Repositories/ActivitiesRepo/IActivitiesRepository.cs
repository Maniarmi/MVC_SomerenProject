using MVC_SomerenProject.Models;

namespace MVC_SomerenProject.Repositories.ActivitiesRepo
{
	public interface IActivitiesRepository
	{
		void Add(Activities activities);
		void Update(Activities activities);
		void Delete(string Name);
		List<Activities> GetAll();
		Activities? GetByName(string id);
	}
}
