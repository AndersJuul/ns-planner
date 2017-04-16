using ajf.ns_planner.shared2.BookHandling;
using NPOI.SS.UserModel;

namespace ajf.ns_planner.shared2.Interfaces
{
    public interface IPlaceSheetService
    {
        ISheet Get(IBookCollection bookCollection);
    }
}