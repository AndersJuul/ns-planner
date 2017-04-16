using System;

namespace ajf.ns_planner.datalayer.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime LatestChange { get; set; }
    }
}