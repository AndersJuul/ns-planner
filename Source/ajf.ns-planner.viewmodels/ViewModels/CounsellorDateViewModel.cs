using System;
using ajf.ns_planner.servicelayer.Models;
using AutoMapper;

namespace ajf.ns_planner.viewmodels.ViewModels
{
    public class CounsellorDateViewModel
    {
        public CounsellorDateViewModel(CounsellorDate counsellorDate)
        {
            Mapper.Map(counsellorDate, this);
        }

        public DateTime Date { get; set; }
        public string Initials { get; set; }
        public string ProjectName { get; set; }

        public string ShortIdString
        {
            get { return string.Format("{0}{1:yyyy-MM-dd}", Initials, Date); }
        }
    }
}