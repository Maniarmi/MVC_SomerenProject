namespace MVC_SomerenProject.ViewModels
{
    public class LectureWithActiveityViewModel
    {
        public List<LectureWithActiveitySupervisorViewModel> Supervisor { get; set; } = new List<LectureWithActiveitySupervisorViewModel>();
        public List<LectureWithActiveityNonSupervisorViewModel> NonSupervisor { get; set; } = new List<LectureWithActiveityNonSupervisorViewModel>();
    }
}
