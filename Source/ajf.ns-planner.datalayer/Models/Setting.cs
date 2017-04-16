namespace ajf.ns_planner.datalayer.Models
{
    public class Setting : BaseEntity
    {
        public virtual Project LatestProject { get; set; }
    }
}