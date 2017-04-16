using System;
using System.ComponentModel.DataAnnotations;

namespace ajf.ns_planner.datalayer.Models
{
    public class Request : BaseEntity
    {
        private DateTime _requestTime;

        [Required]
        public virtual Project Project { get; set; }

        [Required]
        public DateTime RequestTime
        {
            get { return _requestTime; }
            set
            {
                _requestTime = value;
                RequestTimeValue = value.Ticks;
            }
        }

        public long RequestTimeValue { get; private set; }

        [Required]
        public string ContactName { get; set; }

        [Required]
        public string ContactPhone { get; set; }

        [Required]
        [EmailAddress]
        public string ContactEmail { get; set; }

        [Required]
        public string ApplicantName { get; set; }

        [Required]
        public string ParticipantGroup { get; set; }

        [Required]
        public string ParticipantAge { get; set; }

        [Required]
        public string ParticipantNumber { get; set; }

        [Required]
        public string RequestedEvent { get; set; }

        public string RequestedMeetingPlace { get; set; }

        public string RequestedDate { get; set; }

        [Required]
        public string RequestedPeriod { get; set; }

        public string Comments { get; set; }
    }
}