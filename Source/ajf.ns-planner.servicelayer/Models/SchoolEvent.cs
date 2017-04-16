using AutoMapper;

namespace ajf.ns_planner.servicelayer.Models
{
    public class SchoolEvent
    {
        public string FullName { get; set; }

        public SchoolEvent(datalayer.Models.SchoolEvent schoolEvent)
        {
            Mapper.Map(schoolEvent, this);
        }
    }
}