namespace ajf.ns_planner.servicelayer.Models
{
    public class Counsellor
    {
        public Counsellor(datalayer.Models.Counsellor counsellor)
        {
            AutoMapper.Mapper.Map(counsellor, this);
        }

        public string FullName { get; set; }
        public string Initials { get; set; }
    }
}