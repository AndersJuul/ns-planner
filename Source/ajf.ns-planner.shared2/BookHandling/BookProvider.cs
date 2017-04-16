using System;
using System.IO;
using ajf.ns_planner.shared2.Interfaces;
using NPOI.HSSF.UserModel;

namespace ajf.ns_planner.shared2.BookHandling
{
    public class BookProvider : IBookProvider
    {
        private readonly ILogItemListViewModel _logItemListViewModel;

        public BookProvider(ILogItemListViewModel logItemListViewModel)
        {
            _logItemListViewModel = logItemListViewModel;
        }

        public HSSFWorkbook GetBook(string filename, FileAccess fileAccess)
        {
            try
            {
                using (var file = new FileStream(filename, FileMode.Open, fileAccess))
                {
                    return new HSSFWorkbook(file);
                }
            }
            catch (Exception ex)
            {
                _logItemListViewModel.CreateError("Filen kunne ikke åbnes -- er den åben i excel? " + filename);
                return null;
            }
        }
    }
}