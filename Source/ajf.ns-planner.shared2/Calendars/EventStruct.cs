using System;
using System.Text;
using NPOI.SS.UserModel;

namespace ajf.ns_planner.shared2.Calendars
{
    public class EventStruct : IComparable<EventStruct>
    {
        public string CounsellorShort { get; set; }
        public string PlaceShort { get; set; }
        public string EventShort { get; set; }
        public string Deltager { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public IRow Row { get; set; }

        public int CompareTo(EventStruct other)
        {
            return String.Compare(ToString(), other.ToString(), StringComparison.Ordinal);
        }

        public override string ToString()
        {
            //return string.Format("{0}/{1}/{2}/{3}/{4}/{5}", CounsellorShort, PlaceShort, EventShort, Deltager, TimeFrom,
            //    TimeTo);
            return string.Format("{0}/{1}/{2}/{3}/{4}-{5}", GetCellByLetter("X"), GetCellByLetter("AI"), GetCellByLetter("AA"), GetCellByLetter("E") + " " + GetCellByLetter("F"), TimeFrom,
                TimeTo);
        }

        private string GetCellByLetter(string columnId)
        {
            try
            {
                byte[] asciiBytes = Encoding.ASCII.GetBytes(columnId);
                var columnNo = asciiBytes.Length == 1 ? (asciiBytes[0] - 65) : (asciiBytes[0] - 64) * 25 + (asciiBytes[1] - 65) + 1;
                var cell = Row.GetCell(columnNo);
                var stringCellValue = GetStringCellValue(cell);
                return stringCellValue;
            }
            catch (Exception exception)
            {
                return string.Format("#{0}#", columnId);
            }
        }

        private static string GetStringCellValue(ICell cell)
        {
            if (cell.CellType == CellType.String)
                return cell.StringCellValue;
            if (cell.CellType == CellType.Numeric)
                return cell.NumericCellValue.ToString();
            if (cell.CellType == CellType.Blank)
                return "";

            throw new Exception();
        }
    }
}