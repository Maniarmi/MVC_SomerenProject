using Microsoft.AspNetCore.Mvc;
using MVC_SomerenProject.Models;
using MVC_SomerenProject.Repositories.ParticipantsRepo;
using MVC_SomerenProject.Repositories.StudentsRepo;

namespace MVC_SomerenProject.Controllers
{
    public class ParticipantsController : Controller
    {
        private readonly IParticipantsRepository _participantsRepository;

        public ParticipantsController(IParticipantsRepository participantRepository)
        {
            _participantsRepository = participantRepository;
        }

        [HttpGet("/Participants/Index/{Name}")]
        public IActionResult Index(string? Name)
        {
            if (Name == null)
            {
                return NotFound();
            }

            ParticipantAndNot participantLists = new ParticipantAndNot();

            List<Participants> participants = _participantsRepository.GetAll(Name);
            participantLists.Participants = participants;
            List<Participants> notParticipants = _participantsRepository.GetAllNotParticipating(Name);
            participantLists.NotParticipants = notParticipants;

            if (participantLists.Participants == null || participantLists.Participants.Count == 0)
            {
                participantLists.Participants = new List<Participants>();
                //return View(new ParticipantAndNot()); // Return empty list instead of null
            }
            if (participantLists.NotParticipants == null || participantLists.NotParticipants.Count == 0)
            {
                participantLists.NotParticipants = new List<Participants>();
                //return View(new ParticipantAndNot()); // Return empty list instead of null
            }
            return View(participantLists);
        }


        [HttpGet]
        public ActionResult Add()
        {
            return View("CreateParticipant");
        }

        [HttpPost]
        public IActionResult Add(Participants participant)
        {
            try
            {
                _participantsRepository.Add(participant);
                return View("CreateParticipant", participant);

            }
            catch
            {
                return BadRequest(new { message = "Participant Is Not added" });
            }

        }


        [HttpGet]
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            Participants? participants = _participantsRepository.GetById((int)Id);
            return View("DeleteParticipant", participants);
        }


        [HttpPost]
        public ActionResult Delete(Participants participant)
        {
            try
            {
                _participantsRepository.Delete(participant.StudentNumber, participant.Name);
                return View("DeleteParticipant", participant);

            }
            catch (Exception ex)
            {
                return View("DeleteParticipant", participant);
            }
        }
    }
}

