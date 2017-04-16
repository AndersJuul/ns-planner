using System;
using System.Globalization;
using System.IO;
using System.Linq;
using ajf.ns_planner.shared2.Interfaces;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ajf.ns_planner.shared2.BookHandling
{
    public class ExcelBookService : IExcelBookService
    {
        public ICell GetCell(int cNum, ISheet sheet, int colIndex)
        {
            return sheet.GetRow(cNum).Cells.SingleOrDefault(x => x.ColumnIndex == colIndex);
        }

        public string GetTimeCellValue(int cNum, ISheet sheet, int colIndex)
        {
            var cell = GetCell(cNum, sheet, colIndex);
            if (cell == null)
                return "";

            switch (cell.CellType)
            {
                case CellType.Numeric:
                    var dateCellValue = cell.DateCellValue;                    
                    return GetCell(dateCellValue.Hour, dateCellValue.Minute, cell.NumericCellValue);
                case CellType.String:
                    var stringValue = cell.StringCellValue;
                    var strings = stringValue.Split('.');
                    if(strings.Count()!=2)
                        throw new Exception(stringValue + " kan ikke forstås som tidspunkt!");
                    return GetCell(Convert.ToInt32(strings[0]), Convert.ToInt32(strings[1]), 0);
                default:
                    return cell.StringCellValue;
            }
        }

        private static string GetCell(int h, int m, double cellValue)
        {
            if ((h == 0) && (m == 0))
            {
                var numericCellValue = cellValue;
                return numericCellValue.ToString(CultureInfo.InvariantCulture);
            }

            var d = (h + (m/60d));
            return d.ToString(CultureInfo.InvariantCulture);
        }

        public string GetStringCellValue(int cNum, ISheet sheet, int colIndex)
        {
            var cell = GetCell(cNum, sheet, colIndex);
            if (cell == null)
                return "";

            switch (cell.CellType)
            {
                case CellType.Numeric:
                    return cell.NumericCellValue.ToString(CultureInfo.InvariantCulture);
                default:
                    return cell.StringCellValue;
            }
        }

        public string GetDeltagerIdentString(int rowId, ISheet sheetAt)
        {
            return GetStringCellValue(rowId, sheetAt, 4) + ">" + GetStringCellValue(rowId, sheetAt, 5);
        }

        public void SaveWorkbook(HSSFWorkbook destinationBook, string destinationFileFullPath)
        {
            if (File.Exists(destinationFileFullPath))
                File.Delete(destinationFileFullPath);

            using (var file = new FileStream(destinationFileFullPath, FileMode.CreateNew, FileAccess.Write))
            {
                destinationBook.Write(file);
            }
        }
    }
}