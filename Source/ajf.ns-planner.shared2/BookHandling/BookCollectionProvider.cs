using System.IO;
using ajf.ns_planner.shared2.Interfaces;

namespace ajf.ns_planner.shared2.BookHandling
{
    public class BookCollectionProvider : IBookCollectionProvider
    {
        private readonly IBookProvider _bookProvider;
        private readonly ILogItemListViewModel _logItemListViewModel;

        public BookCollectionProvider(ILogItemListViewModel logItemListViewModel, IBookProvider bookProvider)
        {
            _logItemListViewModel = logItemListViewModel;
            _bookProvider = bookProvider;
        }

        public IBookCollection GetBookCollection(IDerivedPlannerSettings derivedPlannerSettings)
        {
            var destinationBook = _bookProvider.GetBook(derivedPlannerSettings.UnversionedDestinationFileFullPath,
                FileAccess.ReadWrite);
            //if (destinationBook == null)
            //    return null;

            var counsellorBook = _bookProvider.GetBook(derivedPlannerSettings.CounsellorFileFullPath, FileAccess.Read);
            if (counsellorBook == null)
                return null;

            var eventBook = _bookProvider.GetBook(derivedPlannerSettings.EventFileFullPath, FileAccess.Read);
            if (eventBook == null)
                return null;

            var placeBook = _bookProvider.GetBook(derivedPlannerSettings.PlaceFileFullPath, FileAccess.Read);
            if (placeBook == null)
                return null;

            return new BookCollection
            {
                DestinationBook = destinationBook,
                CounsellorBook = counsellorBook,
                EventBook = eventBook,
                PlaceBook = placeBook
            };
        }
    }
}