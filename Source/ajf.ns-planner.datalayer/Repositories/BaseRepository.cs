using System;
using System.Globalization;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ajf.ns_planner.datalayer.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        protected string GetCellString(IRow row, int cellnum)
        {
            var cell = row.GetCell(cellnum);
            if (cell == null) return "";
            return cell.CellType == CellType.Numeric
                ? cell.NumericCellValue.ToString(CultureInfo.InvariantCulture)
                : cell.StringCellValue;
        }

        protected ISheet GetFirstSheet(string filename)
        {
            HSSFWorkbook hssfwb;
            try
            {
                using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    hssfwb = new HSSFWorkbook(file);
                }
            }
            catch (Exception ex)
            {
                hssfwb = null;
            }
            if (hssfwb == null)
                return null;

            var worksheet = hssfwb.GetSheetAt(0);
            return worksheet;
        }
    }
}