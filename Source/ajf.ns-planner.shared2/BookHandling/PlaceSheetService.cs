using ajf.ns_planner.shared2.Interfaces;
using NPOI.SS.UserModel;

namespace ajf.ns_planner.shared2.BookHandling
{
    public class PlaceSheetService : IPlaceSheetService
    {
        public ISheet Get(IBookCollection bookCollection)
        {
            return bookCollection.PlaceBook.GetSheetAt(0);
        }
    }
}