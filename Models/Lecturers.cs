namespace MVC_SomerenProject.Models
{
    public class Lecturers
    {
        public int LecturerNumber { get; set; } 
        public string FirstName { get; set; }   
        public string LastName { get; set; } 
        public string PhoneNumber { get; set; }    
        public int Age { get; set; }   
         

        public Lecturers()
        {
            LecturerNumber = 0; 
            FirstName = "";
            LastName = ""; 
            PhoneNumber = "";    
            Age = 0;   
            
        }

        public Lecturers (int lecturerNumber, string firstName, string lastName, string phoneNumber, int age)
        {
            LecturerNumber = lecturerNumber;    
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Age = age;
              
        }   
    }
}
