using System.Collections.Generic;
using System.IO;
using ajf.ns_planner.shared2.Interfaces;

namespace ajf.ns_planner.shared2.Emails
{
    public class FileManager : IFileManager
    {
        public void MarkMailFileAsSend(string fileName)
        {
            File.Move(fileName, fileName.Replace(".html", ".htm"));
        }

        public IEnumerable<string> GetHtmlFilesInDir(string directoryName)
        {
            return Directory.EnumerateFiles(directoryName, "*.html");
        }

        public void CreateDirectory(string destination)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(destination));
        }

        public void CopyWithOverwrite(string source, string destination)
        {
            File.Copy(source, destination, true);
        }
    }
}