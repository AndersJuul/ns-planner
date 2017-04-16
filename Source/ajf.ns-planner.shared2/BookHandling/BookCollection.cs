using NPOI.HSSF.UserModel;

namespace ajf.ns_planner.shared2.BookHandling
{
    public class BookCollection:IBookCollection
    {
        public HSSFWorkbook DestinationBook { get; set; }
        public HSSFWorkbook CounsellorBook { get; set; }
        public HSSFWorkbook EventBook { get; set; }
        public HSSFWorkbook PlaceBook { get; set; }
    }
}