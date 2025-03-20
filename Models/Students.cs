using System.Net.Mail;

namespace MVC_SomerenProject.Models
{
    public class Students
    {
        public int StudentNumber { get; set; }  
        public string FirstName { get; set; }   
        public string LastName { get; set; } 
        public string PhoneNumber { get; set; }    
        public string Class {  get; set; }

        public Students()
        {
            StudentNumber = 0;
            FirstName = "";
            LastName = "";
            PhoneNumber ="0"; 
            Class = "";
      
        }

        public Students (int StudentNumber, string FirstName, string LastName, string PhoneNumber, string Class)
        {
            this.StudentNumber = StudentNumber;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.PhoneNumber = PhoneNumber;
            this.Class = Class;
         
        }   
    }
}
