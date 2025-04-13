namespace MVC_SomerenProject.Models
{
    public class ParticipantAndNot
    {
        public List<Participants> Participants { get; set; }
        public List<Participants> NotParticipants { get; set; }

        public void AddParticipants(List<Participants> participants)
        {
            Participants = participants;
        }

        public void AddNotParticipants(List<Participants> notParticipants)
        {
            NotParticipants = notParticipants;
        }
    }
}

