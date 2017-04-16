using ajf.ns_planner.servicelayer.Models;

namespace ajf.ns_planner.servicelayer
{
    public class ServiceLayerSetup
    {
        public static void SetupAutomapper()
        {
            AutoMapper.Mapper.CreateMap<datalayer.Models.Request, Request>();//.ForMember(x => x.Id, opts => opts.Ignore()).ForMember(x => x.Project, opts => opts.Ignore());
            AutoMapper.Mapper.CreateMap<datalayer.Models.Counsellor, Counsellor>();
            AutoMapper.Mapper.CreateMap<datalayer.Models.CounsellorDate, CounsellorDate>(); //.ForMember(x => x.Id, opts => opts.Ignore()).ForMember(x => x.Project, opts => opts.Ignore());
            AutoMapper.Mapper.CreateMap<datalayer.Models.Project, Project>();//.ForMember(x => x.Id, opts => opts.Ignore()).ForMember(x => x.Project, opts => opts.Ignore());
            AutoMapper.Mapper.CreateMap<datalayer.Models.Request, Request>();//.ForMember(x => x.Id, opts => opts.Ignore()).ForMember(x => x.Project, opts => opts.Ignore());
            AutoMapper.Mapper.CreateMap<datalayer.Models.SchoolEvent, SchoolEvent>();//.ForMember(x => x.Id, opts => opts.Ignore()).ForMember(x => x.Project, opts => opts.Ignore());

            AutoMapper.Mapper.CreateMap<CounsellorDate, datalayer.Models.CounsellorDate>(); //.ForMember(x => x.Id, opts => opts.Ignore()).ForMember(x => x.Project, opts => opts.Ignore());
        }

        public static void Setup()
        {
            SetupAutomapper();
        }
    }
}