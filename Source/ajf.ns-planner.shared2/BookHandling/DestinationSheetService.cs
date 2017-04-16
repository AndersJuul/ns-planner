using ajf.ns_planner.shared2.Interfaces;
using NPOI.SS.UserModel;

namespace ajf.ns_planner.shared2.BookHandling
{
    public class DestinationSheetService : IDestinationSheetService
    {
        public ISheet Get(IBookCollection bookCollection)
        {
            return bookCollection.DestinationBook.GetSheetAt(0);
        }
    }
}