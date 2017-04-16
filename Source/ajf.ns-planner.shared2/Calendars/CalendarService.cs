using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.UI;
using ajf.ns_planner.shared2.BookHandling;
using ajf.ns_planner.shared2.Interfaces;
using DDay.iCal;
using DDay.iCal.Serialization.iCalendar;
using NPOI.SS.UserModel;

namespace ajf.ns_planner.shared2.Calendars
{
    public class CalendarService : ICalendarService
    {
        private readonly IDestinationSheetService _destinationSheetService;
        private readonly IExcelBookService _excelBookService;
        private readonly ILogItemListViewModel _logItemListViewModel;
        private readonly IPlannerSettingsProvider _plannerSettingsProvider;
        private readonly IFileManager _fileManager;

        public CalendarService(IExcelBookService excelBookService, IDestinationSheetService destinationSheetService,
            ILogItemListViewModel logItemListViewModel, IPlannerSettingsProvider plannerSettingsProvider, IFileManager fileManager)
        {
            _excelBookService = excelBookService;
            _destinationSheetService = destinationSheetService;
            _logItemListViewModel = logItemListViewModel;
            _plannerSettingsProvider = plannerSettingsProvider;
            _fileManager = fileManager;
        }

        public void CreateCalendar(int dateColumnInt, int columnAssignedCounsellor, int eventColumnInt, int placeColumnInt, string counsellorShort, IDerivedPlannerSettings derivedPlannerSettings, IBookCollection bookCollection)
        {
            var iCal = new iCalendar
            {
                Version = "2.0"
            };

            var destinationSheet = _destinationSheetService.Get(bookCollection);

            var contentByDate = GetContentByDate(dateColumnInt, columnAssignedCounsellor, eventColumnInt, placeColumnInt,
                counsellorShort, derivedPlannerSettings, destinationSheet);

            if (contentByDate.Count == 0)
            {
                _logItemListViewModel.CreateWarning($"Kunne ikke skabe kalender for {counsellorShort}; ingen elementer.");
                return;
            }

            var firstDate = contentByDate.Keys.OrderBy(x => x).First();

            for (var monthCounter = 1; monthCounter <= 12; monthCounter++)
            {
                var firstInMonth = new DateTime(firstDate.Year, monthCounter, 1);
                var lastInMonth = firstInMonth.AddMonths(1).AddDays(-1);

                var firstDateShown = firstInMonth;
                while (firstDateShown.DayOfWeek != DayOfWeek.Monday) firstDateShown = firstDateShown.AddDays(-1);

                var lastDateShown = lastInMonth;
                while (lastDateShown.DayOfWeek != DayOfWeek.Sunday) lastDateShown = lastDateShown.AddDays(1);

                WriteHtml(counsellorShort, firstInMonth, firstDateShown, lastDateShown, lastInMonth, contentByDate, iCal);
            }
        }

        private void WriteHtml(string counsellorCriteria, DateTime firstInMonth, DateTime firstDateShown,
            DateTime lastDateShown, DateTime lastInMonth, Dictionary<DateTime, List<EventStruct>> contentByDate,
            iCalendar iCal)
        {
            using (var htmlwriter = new HtmlTextWriter(new StringWriter()))
            {
                htmlwriter.AddAttribute(HtmlTextWriterAttribute.Border, "1");
                htmlwriter.RenderBeginTag(HtmlTextWriterTag.Table);

                htmlwriter.RenderBeginTag(HtmlTextWriterTag.Tr);
                htmlwriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "7");
                htmlwriter.AddAttribute(HtmlTextWriterAttribute.Align, "Center");
                htmlwriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlwriter.Write(firstInMonth.ToString("MMMM"));
                htmlwriter.RenderEndTag();
                htmlwriter.RenderEndTag();

                var dt = firstDateShown;

                while (dt < lastDateShown)
                {
                    Console.Write(dt.ToString("dd") + " - ");
                    htmlwriter.RenderBeginTag(HtmlTextWriterTag.Tr);

                    for (var weekDay = 0; weekDay < 7; weekDay++)
                    {
                        htmlwriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width:100px");
                        htmlwriter.AddAttribute(HtmlTextWriterAttribute.Valign, "top");
                        htmlwriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        if ((firstInMonth <= dt) && (dt <= lastInMonth))
                        {
                            htmlwriter.AddAttribute(HtmlTextWriterAttribute.Style,
                                "textDecoration:underline;text-align:center");
                            htmlwriter.RenderBeginTag(HtmlTextWriterTag.Div);
                            htmlwriter.Write(WebUtility.HtmlEncode(dt.ToString("dd. ddd")));
                            htmlwriter.RenderEndTag();

                            if (contentByDate.ContainsKey(dt))
                            {
                                var list = contentByDate[dt];
                                var orderedEnumerable = list.OrderBy(x => x);
                                foreach (var s in orderedEnumerable)
                                {
                                    htmlwriter.RenderBeginTag(HtmlTextWriterTag.Div);
                                    htmlwriter.Write(WebUtility.HtmlEncode(s.ToString()));
                                    htmlwriter.RenderEndTag();

                                    if (s != orderedEnumerable.Last())
                                    {
                                        htmlwriter.AddAttribute(HtmlTextWriterAttribute.Style, "height:10px");
                                        htmlwriter.RenderBeginTag(HtmlTextWriterTag.Div);
                                        htmlwriter.RenderEndTag();
                                    }

                                    var evt = iCal.Create<Event>();
                                    evt.Summary = "NS " + s;
                                    var dTimeFrom = Convert.ToDouble(s.TimeFrom, CultureInfo.InvariantCulture);
                                    var dTimeTo = Convert.ToDouble(s.TimeTo, CultureInfo.InvariantCulture);
                                    evt.Start = new iCalDateTime(dt.AddHours(dTimeFrom));
                                    evt.Duration = TimeSpan.FromHours(dTimeTo - dTimeFrom);
                                    var now = DateTime.Now;
                                    evt.Description = String.Format("Oprettet {0} {1}",
                                        now.ToLongDateString(),
                                        now.ToLongTimeString()
                                        );
                                    evt.IsAllDay = false;
                                    evt.UID = Guid.NewGuid().ToString();
                                    evt.Organizer = new Organizer("CN=Tine Nord:MAILTO:tnraa@slagelse.dk");
                                }
                            }
                            else
                            {
                                htmlwriter.AddAttribute(HtmlTextWriterAttribute.Style, "height:80px");
                                htmlwriter.RenderBeginTag(HtmlTextWriterTag.Div);
                                htmlwriter.RenderEndTag();
                            }
                        }
                        htmlwriter.RenderEndTag();

                        dt = dt.AddDays(1);
                    }

                    htmlwriter.RenderEndTag();
                }
                Console.WriteLine();

                htmlwriter.RenderEndTag();
                var renderedContent = htmlwriter.InnerWriter.ToString();

                var derivedPlannerSettings = _plannerSettingsProvider.GetDerivedPlannerSettings(false);

                var fullPathToHtml = String.Format(derivedPlannerSettings.Directory+ @"\calendars\html\result{0:yyyy-MM}{1}.html", firstInMonth,
                    counsellorCriteria ?? "");

                _fileManager.CreateDirectory(fullPathToHtml);

                File.WriteAllText(
                    fullPathToHtml,
                    renderedContent);

                var res = new iCalendarSerializer().SerializeToString(iCal);
                var fullPathToIcs = String.Format(derivedPlannerSettings.Directory +  @"\calendars\ics\result{0:yyyy-MM}{1}.ics", firstInMonth,
                    counsellorCriteria ?? "");

                _fileManager.CreateDirectory(fullPathToIcs);

                File.WriteAllText(
                    fullPathToIcs,
                    res);
            }
        }

        private Dictionary<DateTime, List<EventStruct>> GetContentByDate(int dateColumnInt, int columnAssignedCounsellor,
            int eventColumnInt,
            int placeColumnInt, string counsellorCriteria, IDerivedPlannerSettings derivedPlannerSettings, ISheet sheetAt)
        {
            var contentBy = new Dictionary<DateTime, List<EventStruct>>();
            for (var rowId = sheetAt.FirstRowNum + 1; rowId < sheetAt.LastRowNum; rowId++)
            {
                try
                {
                    var row = sheetAt.GetRow(rowId);
                    //var counsellorCell = row.GetCell(columnAssignedCounsellor);
                    if (row == null)
                    {
                        Debug.WriteLine("");
                        continue;
                    }

                    var dateCell = row.GetCell(dateColumnInt);

                    if (dateCell == null)
                        continue;

                    var counsellorString = _excelBookService.GetStringCellValue(rowId, sheetAt, columnAssignedCounsellor);

                    if ((counsellorCriteria != null) && (counsellorCriteria != counsellorString))
                        continue;

                    if (string.IsNullOrEmpty(counsellorString))
                        continue;

                    var dateCellValue = dateCell.DateCellValue;
                    if (dateCellValue < DateTime.Today)
                    {
                        dateCellValue = dateCellValue.AddYears(1);
                    }
                    if (!contentBy.ContainsKey(dateCellValue))
                        contentBy[dateCellValue] = new List<EventStruct>();

                    var timeCellValue = _excelBookService.GetTimeCellValue(rowId, sheetAt,
                        derivedPlannerSettings.TidFraColumnInt);
                    var cellValue = _excelBookService.GetTimeCellValue(rowId, sheetAt,
                        derivedPlannerSettings.TidTilColumnInt);

                    var item = new EventStruct
                    {
                        CounsellorShort = counsellorString,
                        PlaceShort = _excelBookService.GetStringCellValue(rowId, sheetAt, placeColumnInt),
                        EventShort = _excelBookService.GetStringCellValue(rowId, sheetAt, eventColumnInt),
                        Deltager = _excelBookService.GetDeltagerIdentString(rowId, sheetAt),
                        TimeFrom = timeCellValue,
                        TimeTo = cellValue,
                        Row = row
                    };
                    contentBy[dateCellValue].Add(item);
                }
                catch (Exception exception)
                {
                    _logItemListViewModel.CreateError("Fejl i kalender-generering: " + Environment.NewLine +
                                                      exception.Message +
                                                      Environment.NewLine + exception.StackTrace);
                }
            }
            return contentBy;
        }
    }
}