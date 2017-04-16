using System;
using AutoMapper;

namespace ajf.ns_planner.servicelayer.Models
{
    public class Project : BaseModel
    {
        public Project(datalayer.Models.Project dataproject)
        {
            Mapper.Map(dataproject, this);
        }

        public string Name { get; set; }
        public DateTime FirstDate { get; set; }
        public DateTime LastDate { get; set; }

        public int LengthInDays
        {
            get { return LastDate.Subtract(FirstDate).Days; }
        }
    }

    public class BaseModel
    {
        public int Id { get; set; }
    }
}