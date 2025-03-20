namespace MVC_SomerenProject.Models
{
    public class Rooms
    {
        public int RoomNumber { get; set; } 
        public int RoomSize { get; set; }    
        public string RoomType { get; set; }      

        public Rooms()
        {
            RoomNumber = 0;
            RoomSize = 0;
            RoomType = "";
             
        }

        public Rooms (int roomNumber, int roomSize, string roomType)
        {
            RoomNumber = roomNumber;
            RoomSize = roomSize;
            RoomType = roomType;
               
        }   
    }
}
