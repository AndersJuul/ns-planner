using ajf.ns_planner.shared2;
using AutoMapper;

namespace ajf.ns_planner.mailsender
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Mapper.CreateMap<IPlannerSettings, DerivedPlannerSettings>();

            MailSenderService.SendWaitingMails();
        }
    }
}