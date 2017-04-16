using System.IO;
using ajf.ns_planner.shared2.Interfaces;

namespace ajf.ns_planner.shared2.NsCommands
{
    public class NsValidateMergeExcelQueryHandler : INsQueryHandler<NsValidateMergeExcelQuery, NsValidateMergeExcelResponse>
    {
        private readonly IBookProvider _bookProvider;

        public NsValidateMergeExcelQueryHandler(IBookProvider bookProvider)
        {
            _bookProvider = bookProvider;
        }

        private NsValidateMergeExcelResponse GetNsMergeErrorResponse(string message)
        {
            return new NsValidateMergeExcelResponse(message);
        }
        public NsValidateMergeExcelResponse Query(NsValidateMergeExcelQuery query)
        {
            var pathToSource = query.PathToSource;
            if (!File.Exists(pathToSource))
            {
                return GetNsMergeErrorResponse("Ikke fundet source: " + pathToSource);
            }

            var pathToLookupSource = query.PathToLookupSource;
            if (!File.Exists(pathToLookupSource))
            {
                return GetNsMergeErrorResponse("Ikke fundet lookup source: " + pathToLookupSource);
            }

            var sourceBook = _bookProvider.GetBook(pathToSource, FileAccess.Read);
            if (sourceBook == null)
            {
                return
                    GetNsMergeErrorResponse("Fil findes men kan ikke åbnes af programmet. Allerede åben? : " +
                                            pathToSource);
            }

            var lookupBook = _bookProvider.GetBook(pathToLookupSource, FileAccess.Read);
            if (lookupBook == null)
            {
                return
                    GetNsMergeErrorResponse("Fil findes men kan ikke åbnes af programmet. Allerede åben? : " +
                                            pathToLookupSource);
            }

            return new NsValidateMergeExcelResponse ();
        }
    }
}