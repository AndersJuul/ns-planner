using ajf.ns_planner.servicelayer.Models;
using ajf.ns_planner.wpf.ViewModels;
using AutoMapper;

namespace ajf.ns_planner.wpf
{
    public static class GuiLayerSetup
    {
        public static void Setup()
        {
            Mapper.CreateMap<CounsellorDate, CounsellorDateViewModel>();
            Mapper.CreateMap<Counsellor, CounsellorViewModel>();
            Mapper.CreateMap<Request, RequestViewModel>();
            Mapper.CreateMap<SchoolEvent, SchoolEventViewModel>();

            Mapper.CreateMap<MatchViewModel, Match>();
        }
    }
}