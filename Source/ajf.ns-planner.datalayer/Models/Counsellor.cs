using System.ComponentModel.DataAnnotations;

namespace ajf.ns_planner.datalayer.Models
{
    public class Counsellor : BaseEntity
    {
        [Required]
        public string Initials { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public virtual Project Project { get; set; }
    }
}