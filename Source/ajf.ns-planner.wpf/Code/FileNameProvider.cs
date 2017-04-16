using ajf.ns_planner.shared;
using Microsoft.Win32;

namespace ajf.ns_planner.wpf.Code
{
    public class FileNameProvider : IFileNameProvider
    {
        private readonly ICurrentProject _currentProject;

        public FileNameProvider(ICurrentProject currentProject)
        {
            _currentProject = currentProject;
        }

        public string GetExcelToImport()
        {
            var dlg = new OpenFileDialog
            {
                InitialDirectory = _currentProject.DocumentDirectory,
                DefaultExt = ".xls",
                Filter = "Excel filer (.xls)|*.xls"
            };

            var result = dlg.ShowDialog();

            if (result != true)
            {
                return null;
            }

            // Open document 
            var filename = dlg.FileName;
            return filename;
        }
    }
}