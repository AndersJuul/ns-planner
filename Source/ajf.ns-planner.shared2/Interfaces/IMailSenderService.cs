using System.Threading.Tasks;

namespace ajf.ns_planner.shared2.Interfaces
{
    public interface IMailSenderService
    {
        Task SendWaitingMails();
    }
}