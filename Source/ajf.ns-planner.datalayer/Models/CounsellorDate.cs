using System;
using System.ComponentModel.DataAnnotations;

namespace ajf.ns_planner.datalayer.Models
{
    public class CounsellorDate : BaseEntity
    {
        [Required]
        public virtual Counsellor Counsellor { get; set; }

        [Required]
        public virtual Project Project { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}