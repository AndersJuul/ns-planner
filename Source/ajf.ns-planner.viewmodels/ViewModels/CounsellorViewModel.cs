using ajf.ns_planner.servicelayer.Models;

namespace ajf.ns_planner.viewmodels.ViewModels
{
    public class CounsellorViewModel
    {
        public string FullName { get; set; }
        public string Initials { get; set; }
        public bool IsChecked { get; set; }

        public int CounsellorId { get; set; }

        public CounsellorViewModel(Counsellor counsellor)
        {
            AutoMapper.Mapper.Map(counsellor, this);
        }
    }
}