using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ajf.ns_planner.shared2.Interfaces
{
    public interface IExcelBookService
    {
        string GetStringCellValue(int cNum, ISheet sheet, int colIndex);
        string GetTimeCellValue(int cNum, ISheet sheet, int colIndex);
        string GetDeltagerIdentString(int rowId, ISheet sheetAt);
        ICell GetCell(int cNum, ISheet sheet, int colIndex);
        void SaveWorkbook(HSSFWorkbook destinationBook, string destinationFileFullPath);
    }
}