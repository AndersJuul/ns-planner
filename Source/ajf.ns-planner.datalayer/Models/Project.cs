using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ajf.ns_planner.datalayer.Models
{
    public class Project : BaseEntity
    {
        public Project()
        {
            Requests = new List<Request>();
            CounsellorDates = new List<CounsellorDate>();
            Matches = new List<Match>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime FirstDate { get; set; }

        [Required]
        public DateTime LastDate { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<CounsellorDate> CounsellorDates { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
    }
}