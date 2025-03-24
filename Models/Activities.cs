namespace MVC_SomerenProject.Models
{
	public class Activities
	{
		public string Description { get; set; }
		public string Date { get; set; }
		public string Name { get; set; }

		public Activities()
		{
			Description = "";
			Date = "";
			Name = "";
		}

		public Activities(string description, string date, string name)
		{
			Description = description;
			Date = date;
			Name = name;
		}
	}
}
