namespace MVC_SomerenProject.Models
{
    public class Supervisors
    {
        public string Name { get; set; }    //it refers to the name of the activity 
        public int LecturerNumber { get; set; }  
        public string FirstName { get; set; }   
        public string LastName { get; set; }



        public Supervisors()
        {
            Name = ""; 
            LecturerNumber = 0;
            FirstName = "";
            LastName = ""; 
        }


        public Supervisors (string name, int lecturerNumber,  string firstName, string lastName)
        {
            Name = name;    
            LecturerNumber = lecturerNumber;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
