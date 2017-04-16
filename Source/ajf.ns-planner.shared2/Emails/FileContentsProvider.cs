using System.IO;
using ajf.ns_planner.shared2.Interfaces;

namespace ajf.ns_planner.shared2.Emails
{
    public class FileContentsProvider : IFileContentsProvider
    {
        private readonly ILogItemListViewModel _logItemListViewModel;

        public FileContentsProvider(ILogItemListViewModel logItemListViewModel)
        {
            _logItemListViewModel = logItemListViewModel;
        }

        public string[] GetFileContents(string file)
        {
            if (!File.Exists(file))
            {
                _logItemListViewModel.CreateError("Forsøgte at læse indholdet af " + file + " men filen findes ikke.");
                return null;
            }
            return File.ReadAllLines(file);
        }
    }
}