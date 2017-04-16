using System.Data.Entity;
using ajf.ns_planner.datalayer.Migrations;
using ajf.ns_planner.datalayer.Models;
using AutoMapper;

namespace ajf.ns_planner.datalayer
{
    public class DataLayerSetup
    {
        public static void SetupAutomapper()
        {
            Mapper.CreateMap<Request, Request>().ForMember(x => x.Id, opts => opts.Ignore());
            Mapper.CreateMap<Counsellor, Counsellor>().ForMember(x => x.Id, opts => opts.Ignore());
            Mapper.CreateMap<Project, Project>().ForMember(x => x.Id, opts => opts.Ignore());
            Mapper.CreateMap<CounsellorDate, CounsellorDate>().ForMember(x => x.Id, opts => opts.Ignore());
        }

        public static void Setup()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NsContext, Configuration>());
            SetupAutomapper();
        }
    }
}