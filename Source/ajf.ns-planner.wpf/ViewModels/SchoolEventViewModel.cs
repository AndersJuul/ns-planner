using ajf.ns_planner.servicelayer.Models;
using AutoMapper;

namespace ajf.ns_planner.wpf.ViewModels
{
    public class SchoolEventViewModel
    {
        public SchoolEventViewModel(SchoolEvent schoolEvent)
        {
            Mapper.Map(schoolEvent, this);
        }

        public string FullName { get; set; }

        public string ShortIdString
        {
            get { return FullName; }
        }
    }
}