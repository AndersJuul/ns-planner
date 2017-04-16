using System;
using System.Globalization;
using System.Linq;
using ajf.ns_planner.shared2.BookHandling;
using ajf.ns_planner.shared2.Interfaces;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ajf.ns_planner.shared2.BasicMerge
{
    public class SingleRowMergeService : ISingleRowMergeService
    {
        private readonly IDestinationSheetService _destinationSheetService;
        private readonly IExcelBookService _excelBookService;
        private readonly ILogItemListViewModel _logItemListViewModel;
        private readonly IPlaceSheetService _placeSheetService;

        public SingleRowMergeService(IExcelBookService excelBookService, ILogItemListViewModel logItemListViewModel,
            IPlaceSheetService placeSheetService, IDestinationSheetService destinationSheetService)
        {
            _excelBookService = excelBookService;
            _logItemListViewModel = logItemListViewModel;
            _placeSheetService = placeSheetService;
            _destinationSheetService = destinationSheetService;
        }

        public void MergeSingleRow(int firstWriteableColumnInt, Statistics statistics,
            IDerivedPlannerSettings derivedPlannerSettings, ISheet destinationSheet, int rowNo,
            IBookCollection bookCollection)
        {
            var row = destinationSheet.GetRow(rowNo);

            // There may be blank rows, especially at the end.
            if (row == null)
                return;

            var nextWriteableColumn = firstWriteableColumnInt;

            UpdateAccordingToCounsellor(ref nextWriteableColumn, row,
                bookCollection.CounsellorBook,
                destinationSheet, rowNo, statistics, derivedPlannerSettings, bookCollection);
            UpdateAccordingToEvent(ref nextWriteableColumn, row, bookCollection.EventBook,
                destinationSheet, rowNo,
                derivedPlannerSettings.EventColumnInt, statistics, derivedPlannerSettings.DateColumnInt,
                derivedPlannerSettings, bookCollection);
            UpdateAccordingToPlace(ref nextWriteableColumn, row, rowNo, statistics, derivedPlannerSettings,
                bookCollection);


            var rowsWithCounsellor = statistics.RowsWithCounsellor;
            InsertSingleCellIntoDestination(nextWriteableColumn++, destinationSheet, rowNo,
                rowsWithCounsellor.ToString(),
                "Antal med vejleder");
            InsertSingleCellIntoDestination(nextWriteableColumn++, destinationSheet, rowNo,
                statistics.RowsWithoutCounsellor.ToString(),
                "Antal uden vejleder");
            InsertSingleCellIntoDestination(nextWriteableColumn++, destinationSheet, rowNo,
                statistics.Group.ToString(),
                "EmailGroup");
            InsertSingleCellIntoDestination(nextWriteableColumn, destinationSheet, rowNo,
               derivedPlannerSettings.SenderMailAddress,
                "EmailReceiver");
        }

        public void InsertCounsellorValuesInDestination(ref int nextWriteableColumnInt, int cNum,
            ISheet counsellorSheet, ISheet destinationSheet, int rowNo, IBookCollection bookCollection)
        {
            InsertSingleCellIntoDestination(nextWriteableColumnInt++, destinationSheet, rowNo, "1", "Tildeles");
            // "Full name");
            InsertSingleCellIntoDestination(nextWriteableColumnInt++, destinationSheet, rowNo,
                counsellorSheet.GetRow(cNum).Cells[1].StringCellValue,
                counsellorSheet.GetRow(counsellorSheet.FirstRowNum).GetCell(1).StringCellValue); // "Full name");
            InsertSingleCellIntoDestination(nextWriteableColumnInt++, destinationSheet, rowNo,
                _excelBookService.GetStringCellValue(cNum, counsellorSheet, 2),
                counsellorSheet.GetRow(counsellorSheet.FirstRowNum).GetCell(2).StringCellValue); // "Tlf");
            InsertSingleCellIntoDestination(nextWriteableColumnInt++, destinationSheet, rowNo,
                counsellorSheet.GetRow(cNum).Cells[3].StringCellValue,
                counsellorSheet.GetRow(counsellorSheet.FirstRowNum).GetCell(3).StringCellValue); // "Email");
        }

        public static void InsertSingleCellIntoDestination(int destinationColumn, ISheet destinationSheet, int rowNo,
            string valueToInsert, string headerText)
        {
            var cell = destinationSheet.GetRow(rowNo).CreateCell(destinationColumn);
            cell.SetCellValue(valueToInsert);

            var destinationSheetHeaderCell =
                destinationSheet.GetRow(destinationSheet.FirstRowNum).CreateCell(destinationColumn);
            destinationSheetHeaderCell.SetCellValue("C_" + headerText);
        }

        public void ProcessRowWithCounsellor(IRow row, HSSFWorkbook counsellorBook, ref int nextWriteableColumnInt, ISheet destinationSheet, int rowNo, IDerivedPlannerSettings derivedPlannerSettings, IBookCollection bookCollection, Statistics statistics)
        {
            var assignedCounsellorCell = row.GetCell(derivedPlannerSettings.VejlederColumnInt);
            var counsellorShort = assignedCounsellorCell.StringCellValue;

            if (counsellorShort == "?")
            {
                // Special case -- ikke denne sæson
                InsertSingleCellIntoDestination(nextWriteableColumnInt++, destinationSheet, rowNo, "2", "Tildeles");
                statistics.RowsWithFutureDate++;
                nextWriteableColumnInt += 3;
                return;
            }

            var counsellorSheet = counsellorBook.GetSheetAt(0);
            var found = false;

            for (var cNum = counsellorSheet.FirstRowNum + 1; cNum <= counsellorSheet.LastRowNum; cNum++)
            {
                var counsellorRow = counsellorSheet.GetRow(cNum);
                var iCounsellorShort = counsellorRow.GetCell(0);
                if (counsellorShort.ToLower() == iCounsellorShort.StringCellValue.ToLower())
                {
                    found = true;
                    statistics.RowsWithCounsellor++;
                    InsertCounsellorValuesInDestination(ref nextWriteableColumnInt,
                        cNum, counsellorSheet, destinationSheet, rowNo, bookCollection);
                    break;
                }
            }
            if (!found)
            {
                _logItemListViewModel.CreateWarning(string.Format("Fandt ikke vejleder '{0}' angivet i linie {1}",
                    counsellorShort,
                    rowNo));
            }
        }

        public void UpdateAccordingToCounsellor(ref int nextWriteableColumnInt, IRow row,
            HSSFWorkbook counsellorBook, ISheet destinationSheet, int rowNo, Statistics statistics,
            IDerivedPlannerSettings derivedPlannerSettings, IBookCollection bookCollection)
        {
            var cellAssignedCounsellor =
                row.Cells.SingleOrDefault(x => x.ColumnIndex == derivedPlannerSettings.VejlederColumnInt);

            if ((cellAssignedCounsellor != null)&&(cellAssignedCounsellor.CellType!=CellType.Blank))
            {
                ProcessRowWithCounsellor(row, counsellorBook, ref nextWriteableColumnInt, destinationSheet, rowNo,
                    derivedPlannerSettings, bookCollection, statistics);
            }
            else
            {
                statistics.RowsWithoutCounsellor++;
                InsertSingleCellIntoDestination(nextWriteableColumnInt, destinationSheet, rowNo, "0", "Tildeles");
                nextWriteableColumnInt += 4;
            }
        }

        public void UpdateAccordingToEvent(ref int nextAvailableColumn, IRow row, HSSFWorkbook eventBook,
            ISheet destinationSheet, int rowNo, int eventColumnInt, Statistics statistics, int dateColumnInt,
            IDerivedPlannerSettings derivedPlannerSettings, IBookCollection bookCollection)
        {
            var cellAssignedEvent = row.GetCell(eventColumnInt);

            if (cellAssignedEvent != null)
            {
                ProcessRowWithEvent(row, eventBook, ref nextAvailableColumn, destinationSheet, rowNo, statistics,
                    eventColumnInt, dateColumnInt, derivedPlannerSettings);
            }
            else
            {
                nextAvailableColumn += 8;
            }
        }

        public void ProcessRowWithEvent(IRow row, HSSFWorkbook eventBook, ref int firstAvailableColumn,
            ISheet destinationSheet, int rowNo, Statistics statistics, int cellnum, int dateColumnInt,
            IDerivedPlannerSettings derivedPlannerSettings)
        {
            try
            {
                var assignedEventCell = row.GetCell(cellnum);
                var eventShort = assignedEventCell.StringCellValue;
                //_logItemListViewModel.CreateInfo("Tildelt arrangement: " + eventShort);

                var assignedDate = GetAssignedDate(row, dateColumnInt,derivedPlannerSettings);

                var eventSheet = eventBook.GetSheetAt(0);
                var found = false;

                for (var cNum = eventSheet.FirstRowNum + 1; cNum <= eventSheet.LastRowNum; cNum++)
                {
                    var eventRow = eventSheet.GetRow(cNum);
                    var iEventShort = eventRow.GetCell(0);
                    if (String.Equals(eventShort, iEventShort.StringCellValue, StringComparison.CurrentCultureIgnoreCase))
                    {
                        //_logItemListViewModel.CreateInfo("Found e: " + cNum);
                        found = true;
                        InsertEventValuesInDestination(ref firstAvailableColumn, cNum, eventSheet, destinationSheet, rowNo,
                            assignedDate, statistics, derivedPlannerSettings);
                        break;
                    }
                }
                if (!found)
                {
                    //_logItemListViewModel.CreateInfo("Ikke fundet: " + eventShort);
                    firstAvailableColumn += 8;
                }
            }
            catch (Exception exception)
            {
                _logItemListViewModel.CreateError("Fejl i linie" + rowNo + ", " +exception.Message);
                
                throw;
            }
        }

        private static DateTime GetAssignedDate(IRow row, int dateColumnInt, IDerivedPlannerSettings derivedPlannerSettings)
        {
            // Raw cell
            var assignedDateCell = row.GetCell(dateColumnInt);
            
            // Get date if there, else minvalue
            if (assignedDateCell == null) return DateTime.MinValue;

            var assignedDate = assignedDateCell.DateCellValue;
            return GetDateBetween(derivedPlannerSettings.StartDate, derivedPlannerSettings.EndDate, assignedDate);
        }

        private static DateTime GetDateBetween(DateTime startDate, DateTime endDate, DateTime assignedDate)
        {
            if (startDate > endDate)
            {
                throw new Exception("Startdato skal ligge før slutdato i opsætningsfilen");
            }

            if (endDate < assignedDate)
            {
                return DateTime.MinValue;
            }

            if (startDate < assignedDate && assignedDate < endDate)
                return assignedDate;

            var dateBetween = GetDateBetween(startDate,endDate, assignedDate.AddYears(1));
            
            if (dateBetween != DateTime.MinValue)
            {
                return dateBetween;
            }

            return DateTime.MinValue;
        }

        public void UpdateAccordingToPlace(ref int nextWriteableColumn, IRow row, int rowNo,
            Statistics statistics, IDerivedPlannerSettings derivedPlannerSettings, IBookCollection bookCollection)
        {
            var assignedPlaceCell = row.GetCell(derivedPlannerSettings.PlaceColumnInt);
            var found = false;
            var placeColumn = assignedPlaceCell == null ? "" : assignedPlaceCell.StringCellValue;

            var destinationSheet = _destinationSheetService.Get(bookCollection);
            var placeSheet = _placeSheetService.Get(bookCollection);

            if (assignedPlaceCell != null)
            {
                for (var cNum = placeSheet.FirstRowNum + 1; cNum <= placeSheet.LastRowNum; cNum++)
                {
                    var placeRow = placeSheet.GetRow(cNum);
                    var placeShort = placeRow.GetCell(0);
                    if (placeColumn.ToLower() != placeShort.StringCellValue.ToLower()) continue;

                    found = true;

                    InsertPlaceValuesInDestination(ref nextWriteableColumn, cNum, placeSheet, destinationSheet, rowNo,
                        statistics);
                    break;
                }
            }

            if (!found)
            {
                if (!string.IsNullOrEmpty(placeColumn))
                {
                    _logItemListViewModel.CreateWarning(string.Format("Sted ikke fundet: {0} i række {1}", placeColumn,
                        rowNo));
                }
                nextWriteableColumn += 1;
            }
        }

        public static void InsertPlaceValuesInDestination(ref int nextWriteableColumn, int cNum, ISheet placeSheet,
            ISheet destinationSheet, int rowNo, Statistics statistics)
        {
            InsertSingleCellIntoDestination(nextWriteableColumn++, destinationSheet, rowNo,
                placeSheet.GetRow(cNum).Cells[1].StringCellValue,
                placeSheet.GetRow(placeSheet.FirstRowNum).GetCell(1).StringCellValue);
        }

        public void InsertEventValuesInDestination(ref int firstAvailableColumn, int cNum, ISheet eventSheet,
            ISheet destinationSheet, int rowNo, DateTime assignedDate, Statistics statistics,
            IDerivedPlannerSettings derivedPlannerSettings)
        {
            // "Full name");
            InsertSingleCellIntoDestination(firstAvailableColumn++, destinationSheet, rowNo,
                eventSheet.GetRow(cNum).Cells[1].StringCellValue,
                eventSheet.GetRow(eventSheet.FirstRowNum).GetCell(1).StringCellValue); // "Full name");
            InsertSingleCellIntoDestination(firstAvailableColumn++, destinationSheet, rowNo,
                assignedDate.ToString("dddd"),
                "Ugedag"); // "tid");
            InsertSingleCellIntoDestination(firstAvailableColumn++, destinationSheet, rowNo,
                _excelBookService.GetStringCellValue(cNum, eventSheet, 2),
                eventSheet.GetRow(eventSheet.FirstRowNum).GetCell(2).StringCellValue); // "Husk");

            var numberOfDaysBefore = _excelBookService.GetCell(cNum, eventSheet, 3).NumericCellValue;
            InsertSingleCellIntoDestination(firstAvailableColumn++, destinationSheet, rowNo,
                numberOfDaysBefore.ToString(CultureInfo.InvariantCulture),
                eventSheet.GetRow(eventSheet.FirstRowNum).GetCell(3).StringCellValue); // Antal dage før);

            InsertSingleCellIntoDestination(firstAvailableColumn++, destinationSheet, rowNo,
                assignedDate == DateTime.MinValue
                    ? "Ikke angivet"
                    : assignedDate.AddDays(-numberOfDaysBefore).ToString("yyyy-MM-dd"),
                "KontaktDato");
            InsertSingleCellIntoDestination(firstAvailableColumn++, destinationSheet, rowNo,
                assignedDate == DateTime.MinValue
                    ? "Ikke angivet"
                    : assignedDate.AddDays(-numberOfDaysBefore).ToString("dd. MMMM yyyy"),
                "KontaktDatoStreng");
            InsertSingleCellIntoDestination(firstAvailableColumn++, destinationSheet, rowNo,
                assignedDate == DateTime.MinValue
                    ? "Ikke angivet"
                    : assignedDate.ToString("dd. MMMM yyyy"),
                "TildeltDatoStreng");

            var fraDouble =
                Convert.ToDouble(
                    _excelBookService.GetTimeCellValue(rowNo, destinationSheet, derivedPlannerSettings.TidFraColumnInt),
                    CultureInfo.InvariantCulture);
            var tilDouble =
                Convert.ToDouble(
                    _excelBookService.GetTimeCellValue(rowNo, destinationSheet, derivedPlannerSettings.TidTilColumnInt),
                    CultureInfo.InvariantCulture);

            var tildeltTidsrumFra = String.Format("{0}:{1:00}", Math.Floor(fraDouble),
                60*(fraDouble - Math.Floor(fraDouble)));
            var tildeltTidsrumTil = String.Format("{0}:{1:00}", Math.Floor(tilDouble),
                60*(tilDouble - Math.Floor(tilDouble)));

            var tildeltTidsrum = tildeltTidsrumFra + "-" + tildeltTidsrumTil;

            InsertSingleCellIntoDestination(firstAvailableColumn++, destinationSheet, rowNo,
                tildeltTidsrum,
                "TildeltTidsrumStreng");
        }
    }
}