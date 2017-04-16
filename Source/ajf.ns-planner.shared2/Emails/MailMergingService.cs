using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using ajf.ns_planner.shared2.Interfaces;

namespace ajf.ns_planner.shared2.Emails
{
    public class MailMergingService : IMailMergingService
    {
        private readonly IBookCollectionProvider _bookCollectionProvider;
        private readonly IDestinationSheetService _destinationSheetService;
        private readonly IExcelBookService _excelBookService;
        private readonly IFileContentsProvider _fileContentsProvider;
        private readonly ILogItemListViewModel _logItemListViewModel;
        private readonly IPlannerSettingsProvider _plannerSettingsProvider;

        public MailMergingService(IExcelBookService excelBookService, IPlannerSettingsProvider plannerSettingsProvider,
            ILogItemListViewModel logItemListViewModel, IBookCollectionProvider bookCollectionProvider,
            IDestinationSheetService destinationSheetService, IFileContentsProvider fileContentsProvider)
        {
            _excelBookService = excelBookService;
            _plannerSettingsProvider = plannerSettingsProvider;
            _logItemListViewModel = logItemListViewModel;
            _bookCollectionProvider = bookCollectionProvider;
            _destinationSheetService = destinationSheetService;
            _fileContentsProvider = fileContentsProvider;
        }

        public bool DoMailMerge()
        {
            var plannerSettings = _plannerSettingsProvider.GetDerivedPlannerSettings(false);

            var unversionedDestinationFileFullPath = plannerSettings.UnversionedDestinationFileFullPath;

            _logItemListViewModel.CreateInfo("Læser resultater i " + unversionedDestinationFileFullPath);

            var bookCollection = _bookCollectionProvider.GetBookCollection(plannerSettings);
            if (bookCollection == null)
            {
                _logItemListViewModel.CreateError("Kunne ikke åbne input filer.");
                return false;
            }
            var resultSheet = _destinationSheetService.Get(bookCollection);

            for (var templateId = 0; templateId < 3; templateId++)
            {
                var templateFile = plannerSettings.HtmlTemplateDir + "\\template" + templateId + ".html";

                _logItemListViewModel.CreateInfo("Kigger efter linier, der skal have mail baseret på skabelon i " +
                                                 templateFile);

                var templateLines = _fileContentsProvider.GetFileContents(templateFile);
                if (templateLines == null)
                {
                    _logItemListViewModel
                        .CreateWarning(
                            $"Templatefilen {templateFile} blev ikke fundet og emails kan derfor ikke skabes for den skabelon.");
                    continue;
                }

                var pathToHtmlEmailsForTemplate = plannerSettings.HtmlOutDirectory + templateId;

                _logItemListViewModel.CreateInfo("Skriver html emails til " + pathToHtmlEmailsForTemplate);

                if (Directory.Exists(pathToHtmlEmailsForTemplate))
                {
                    _logItemListViewModel.CreateInfo(string.Format("Sletter eksisterende email-filer fra {0}",
                        pathToHtmlEmailsForTemplate));
                    var enumerateFiles = Directory.EnumerateFiles(pathToHtmlEmailsForTemplate).ToList();
                    foreach (var enumerateFile in enumerateFiles)
                    {
                        File.Delete(enumerateFile);
                    }
                }
                else
                {
                    _logItemListViewModel.CreateInfo(string.Format("Opretter folder til email-filer i {0}",
                        pathToHtmlEmailsForTemplate));
                    Directory.CreateDirectory(pathToHtmlEmailsForTemplate);
                }

                var numberWritten = 0;
                for (var i = resultSheet.FirstRowNum + 1; i <= resultSheet.LastRowNum; i++)
                {
                    var stringCellValue = _excelBookService.GetStringCellValue(i, resultSheet,
                        plannerSettings.FirstWriteableColumnInt);

                    if (stringCellValue != templateId.ToString())
                        continue;

                    numberWritten++;

                    var row = resultSheet.GetRow(i);

                    var sb = new List<string>();

                    foreach (var templateLine in templateLines)
                    {
                        var line = templateLine;

                        for (int j = row.FirstCellNum; j < row.LastCellNum; j++)
                        {
                            var ss = "$$" + j + "$$";
                            while (line.Contains(ss))
                            {
                                var cellValue = _excelBookService.GetStringCellValue(i, resultSheet, j);
                                var htmlEncode = WebUtility.HtmlEncode(cellValue);
                                line = line.Replace(ss, htmlEncode);
                            }
                        }
                        sb.Add(line);
                    }

                    File.WriteAllLines(pathToHtmlEmailsForTemplate + "\\out-" + i.ToString("0000") + ".html", sb);
                }
                _logItemListViewModel.CreateInfo(string.Format("Skrev {0} filer til {1}", numberWritten,
                    pathToHtmlEmailsForTemplate));
            }
            return true;
        }
    }
}