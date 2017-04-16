using System.IO;
using NPOI.HSSF.UserModel;

namespace ajf.ns_planner.shared2.Interfaces
{
    public interface IBookProvider
    {
        HSSFWorkbook GetBook(string filename, FileAccess fileAccess);
    }
}