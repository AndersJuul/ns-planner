using System;
using System.Collections.Generic;
using System.IO;
using ajf.ns_planner.shared2.Interfaces;
using NPOI.SS.UserModel;

namespace ajf.ns_planner.shared2.NsCommands
{
    public class NsCommandHandler : INsCommandHandler<NsMergeExcelCommand, NsMergeExcelResponse>
    {
        private readonly IBookProvider _bookProvider;
        private readonly IColumnConverter _columnConverter;
        private readonly IExcelBookService _excelBookService;

        private readonly INsQueryHandler<NsValidateMergeExcelQuery, NsValidateMergeExcelResponse>
            _nsValidateMergeExcelQueryHandler;
        public NsCommandHandler(IBookProvider bookProvider, IColumnConverter columnConverter,
            IExcelBookService excelBookService, 
            INsQueryHandler<NsValidateMergeExcelQuery, NsValidateMergeExcelResponse> nsValidateMergeExcelQueryHandler)
        {
            _bookProvider = bookProvider;
            _columnConverter = columnConverter;
            _excelBookService = excelBookService;
            _nsValidateMergeExcelQueryHandler = nsValidateMergeExcelQueryHandler;
        }

        public NsMergeExcelResponse Handle(NsMergeExcelCommand command)
        {
            var nsNsValidateMergeExcelResponse = _nsValidateMergeExcelQueryHandler
                .Query(
                new NsValidateMergeExcelQuery(command.PathToSource,command.PathToLookupSource)
                );
            if (nsNsValidateMergeExcelResponse.HasError)
                return new NsMergeExcelResponse(new List<string> { nsNsValidateMergeExcelResponse.Error });

            var sourceBook = _bookProvider.GetBook(command.PathToSource, FileAccess.Read);

            var index = _columnConverter.ConvertToIndex(command.ColumnNameInSource);

            var sourceSheet = sourceBook.GetSheetAt(0);
            var firstRowNum = sourceSheet.FirstRowNum;
            var lastRowNum = sourceSheet.LastRowNum;
            var maxColumn = GetMaxColumn(sourceSheet);

            var lookupBook = _bookProvider.GetBook(command.PathToLookupSource, FileAccess.Read);
            var lookupSheet = lookupBook.GetSheetAt(0);
            var maxColumnLookup = GetMaxColumn(lookupSheet);

            for (var rowno = firstRowNum; rowno <= lastRowNum; rowno++)
            {
                var row = sourceSheet.GetRow(rowno);
                var cell = row.GetCell(index);

                if (cell == null)
                    continue;

                var lookupRow = GetLookupRow(lookupSheet, cell.StringCellValue);

                if (lookupRow == null) continue;

                var nextCellToWrite = row.LastCellNum;

                for (var i = 0; i < maxColumnLookup; i++)
                {
                    nextCellToWrite++;

                    var cell2 = row.CreateCell(nextCellToWrite);

                    var cell1 = lookupRow.Cells[i];

                    if (cell1.CellType == CellType.String)
                        cell2.SetCellValue(cell1.StringCellValue);

                    if (cell1.CellType == CellType.Numeric)
                        cell2.SetCellValue(cell1.NumericCellValue);
                }
            }

            for (int i = 0; i < maxColumnLookup; i++)
            {
                var lookupHeaderCell = lookupSheet.GetRow(0).Cells[i];

                var cell = sourceSheet.GetRow(0).CreateCell(maxColumn+i);
                cell.SetCellValue( lookupHeaderCell.StringCellValue);
            }

            _excelBookService.SaveWorkbook(sourceBook, command.PathToDestination);

            var errors = new List<string>();
            var nsMergeExcelResponse = new NsMergeExcelResponse(errors);
            return nsMergeExcelResponse;
        }

        private int GetMaxColumn(ISheet sourceSheet)
        {
            var result = 0;

            var firstRowNum = sourceSheet.FirstRowNum;
            var lastRowNum = sourceSheet.LastRowNum;

            for (var rowno = firstRowNum; rowno <= lastRowNum; rowno++)
            {
                var row = sourceSheet.GetRow(rowno);
                result = Math.Max(result, row.LastCellNum);
            }

            return result;
        }

        private IRow GetLookupRow(ISheet lookupSheet, string stringCellValue)
        {
            var firstRowNum = lookupSheet.FirstRowNum;
            var lastRowNum = lookupSheet.LastRowNum;

            for (var rowno = firstRowNum; rowno <= lastRowNum; rowno++)
            {
                var row = lookupSheet.GetRow(rowno);
                if (row.GetCell(0).StringCellValue.ToUpper() == stringCellValue.ToUpper())
                    return row;
            }

            return null;
        }

    }
}