﻿using System;

namespace ajf.ns_planner.servicelayer.Models
{
    public class Request
    {
        public DateTime RequestTime { get; set; }

        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        
        public string ApplicantName { get; set; }

        public string ParticipantGroup { get; set; }

        public string ParticipantAge { get; set; }

        public string ParticipantNumber { get; set; }

        public string RequestedEvent { get; set; }

        public string RequestedMeetingPlace { get; set; }

        public string RequestedDate { get; set; }

        public string RequestedPeriod { get; set; }

        public string Comments { get; set; }
        public int Id { get; set; }
    }
}