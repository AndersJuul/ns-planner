using NPOI.HSSF.UserModel;

namespace ajf.ns_planner.shared2.BookHandling
{
    public interface IBookCollection
    {
        HSSFWorkbook DestinationBook { get; set; }
        HSSFWorkbook CounsellorBook { get; set; }
        HSSFWorkbook EventBook { get; set; }
        HSSFWorkbook PlaceBook { get; set; }
    }
}