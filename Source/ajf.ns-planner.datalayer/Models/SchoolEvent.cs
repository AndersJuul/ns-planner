using System.ComponentModel.DataAnnotations;

namespace ajf.ns_planner.datalayer.Models
{
    public class SchoolEvent : BaseEntity
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public virtual Project Project { get; set; }
    }
}