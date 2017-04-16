using System.ComponentModel.DataAnnotations;

namespace ajf.ns_planner.datalayer.Models
{
    public class Match : BaseEntity
    {
        [Required]
        public virtual Project Project { get; set; }

        public int RequestId { get; set; }
        public int SchoolEventId { get; set; }
        public int CounsellorDateId { get; set; }
        public virtual Request Request { get; set; }
        public virtual Request SchoolEvent { get; set; }
        public virtual Request CounsellorDate { get; set; }
    }
}