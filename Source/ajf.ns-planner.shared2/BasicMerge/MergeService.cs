using System;
using System.Diagnostics;
using System.Linq;
using ajf.ns_planner.shared2.BasicMerge.Validation;
using ajf.ns_planner.shared2.Interfaces;

namespace ajf.ns_planner.shared2.BasicMerge
{
    public class MergeService : IMergeService
    {
        private readonly IBookCollectionProvider _bookCollectionProvider;
        private readonly ICalendarService _calendarService;
        private readonly IDestinationSheetService _destinationSheetService;
        private readonly IExcelBookService _excelBookService;
        private readonly IFileManager _fileManager;
        private readonly ILogItemListViewModel _logItemListViewModel;
        private readonly IPlannerSettingsProvider _plannerSettingsProvider;
        private readonly ISingleRowMergeService _singleRowMergeService;
        private readonly IValidationService _validationService;

        public MergeService(ILogItemListViewModel logItemListViewModel, IExcelBookService excelBookService,
            ICalendarService calendarService, IPlannerSettingsProvider plannerSettingsProvider,
            IBookCollectionProvider bookCollectionProvider, IDestinationSheetService destinationSheetService,
            IFileManager fileManager,
            ISingleRowMergeService singleRowMergeService,
            IValidationService validationService)
        {
            _logItemListViewModel = logItemListViewModel;
            _excelBookService = excelBookService;
            _calendarService = calendarService;
            _plannerSettingsProvider = plannerSettingsProvider;
            _bookCollectionProvider = bookCollectionProvider;
            _destinationSheetService = destinationSheetService;
            _fileManager = fileManager;
            _singleRowMergeService = singleRowMergeService;
            _validationService = validationService;
        }

        public void MergeOnce()
        {
            _logItemListViewModel.Clear();

            var derivedPlannerSettings = _plannerSettingsProvider.GetDerivedPlannerSettings(true);
            var statistics = new Statistics();

            _logItemListViewModel
                .CreateInfo(
                $"Laver resultatfil for perioden {derivedPlannerSettings.StartDate} til {derivedPlannerSettings.EndDate}");

            _logItemListViewModel
                .CreateInfo(
                $"Forventet valg af ønsket periode {derivedPlannerSettings.ExpectedPeriod}");

            DoMerge(derivedPlannerSettings.FirstWriteableColumnInt, statistics, derivedPlannerSettings);

            _logItemListViewModel.CreateInfo("Resultatet af kørsel:");
            _logItemListViewModel.CreateInfo("-- Resultat i " +
                                             derivedPlannerSettings.UnversionedDestinationFileFullPath);
            _logItemListViewModel.CreateInfo(statistics.ToString());
        }

        public void DoMerge(int firstWriteableColumnInt, Statistics statistics,
            IDerivedPlannerSettings derivedPlannerSettings)
        {
            _logItemListViewModel.CreateInfo($"Læser ønsker/tildeling fra {derivedPlannerSettings.RequestFileFullPath}");
            var destination = derivedPlannerSettings.UnversionedDestinationFileFullPath;
            _logItemListViewModel.CreateInfo($"Skriver til {destination}");

            _fileManager.CreateDirectory(destination);
            _fileManager.CopyWithOverwrite(derivedPlannerSettings.RequestFileFullPath, destination);

            var fileExistenceProblems = _validationService
                .GetValidationProblems(derivedPlannerSettings);

            if (fileExistenceProblems.Any())
            {
                foreach (var fileExistenceProblem in fileExistenceProblems)
                {
                    _logItemListViewModel.CreateError($"{fileExistenceProblem}");
                }
                return;
            }

            var bookCollection = _bookCollectionProvider.GetBookCollection(derivedPlannerSettings);
            if (bookCollection == null)
            {
                _logItemListViewModel.CreateError("Et eller flere excel-ark kunne ikke læses!");
                return;
            }

            var destinationSheet = _destinationSheetService.Get(bookCollection);

            var firstRowNum = destinationSheet.FirstRowNum;
            var lastRowNum = destinationSheet.LastRowNum;

            _logItemListViewModel.CreateInfo($"Gennemløber rækkerne fra {firstRowNum} til {lastRowNum}");

            for (var rowNo = firstRowNum + 1; rowNo <= lastRowNum; rowNo++)
            {
                _singleRowMergeService.MergeSingleRow(firstWriteableColumnInt, statistics, derivedPlannerSettings,
                    destinationSheet, rowNo, bookCollection);
            }

            var expectedPeriod = derivedPlannerSettings.ExpectedPeriod;
            for (var rowNo = firstRowNum + 1; rowNo <= lastRowNum; rowNo++)
            {
                try
                {
                    var row = destinationSheet.GetRow(rowNo);
                    if (row != null)
                    {
                        var cellAssignedCounsellor = row.Cells.SingleOrDefault(x => x.ColumnIndex == 10);

                        if (cellAssignedCounsellor != null)
                        {
                            Debug.WriteLine(rowNo + " " + cellAssignedCounsellor.StringCellValue);
                            if (cellAssignedCounsellor.StringCellValue != expectedPeriod)
                            {
                                var isAssignedCell =
                                    row.Cells.SingleOrDefault(
                                        x => x.ColumnIndex == derivedPlannerSettings.FirstWriteableColumnInt);
                                isAssignedCell?.SetCellValue(2);
                            }
                        }
                    }
                }
                catch (Exception ex1)
                {
Debug.WriteLine(ex1.Message);
                }
            }

                _logItemListViewModel.CreateInfo("Færdig med at indsætte info i Resultatfil. Gemmer til " +
                                             destination);
            _excelBookService.SaveWorkbook(bookCollection.DestinationBook, destination);

            var vejleders = new[] {null, "me", "ti"};

            foreach (var vejleder in vejleders)
            {
                _logItemListViewModel.CreateInfo("Skaber kalenderfil begrænset til: " + vejleder);
                _calendarService.CreateCalendar(derivedPlannerSettings.DateColumnInt,
                    derivedPlannerSettings.VejlederColumnInt,
                    derivedPlannerSettings.EventColumnInt, derivedPlannerSettings.PlaceColumnInt, vejleder,
                    derivedPlannerSettings, bookCollection);
            }

            _logItemListViewModel.CreateInfo("Succes!");
        }
    }
}