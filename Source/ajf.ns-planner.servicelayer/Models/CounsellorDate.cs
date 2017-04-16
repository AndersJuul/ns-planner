using System;
using AutoMapper;

namespace ajf.ns_planner.servicelayer.Models
{
    public class CounsellorDate
    {
        public CounsellorDate(datalayer.Models.CounsellorDate counsellorDate) : this()
        {
            Mapper.Map(counsellorDate, this);
        }

        public CounsellorDate()
        {
        }

        public DateTime Date { get; set; }
        public int CounsellorId { get; set; }
        public int ProjectId { get; set; }
    }
}