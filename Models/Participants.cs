namespace MVC_SomerenProject.Models
{
    public class Participants
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Participants()
        {
            Name = "";
            Description = "";
            StudentNumber = 0;
            FirstName = "";
            LastName = "";
        }

        public Participants(string name, string description, int studentNumber, string firstName, string lastName)
        {
            Name = name;
            Description = description;
            StudentNumber = studentNumber;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
