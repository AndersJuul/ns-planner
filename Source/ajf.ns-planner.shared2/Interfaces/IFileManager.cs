using System.Collections.Generic;

namespace ajf.ns_planner.shared2.Interfaces
{
    public interface IFileManager
    {
        void MarkMailFileAsSend(string fileName);
        IEnumerable<string> GetHtmlFilesInDir(string directoryName);
        void CreateDirectory(string destination);
        void CopyWithOverwrite(string source, string destination);
    }
}