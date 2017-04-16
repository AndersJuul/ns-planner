using ajf.ns_planner.shared2.BasicMerge;
using ajf.ns_planner.shared2.BookHandling;
using NPOI.SS.UserModel;

namespace ajf.ns_planner.shared2.Interfaces
{
    public interface ISingleRowMergeService
    {
        void MergeSingleRow(int firstWriteableColumnInt, Statistics statistics,
            IDerivedPlannerSettings derivedPlannerSettings, ISheet destinationSheet, int rowNo, IBookCollection bookCollection);
    }
}