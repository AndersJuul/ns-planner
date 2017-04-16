using ajf.ns_planner.shared2.Interfaces;
using Autofac;
using NUnit.Framework;

namespace ajf.ns_planner.test.IntegrationTests
{
    public class CalendarServiceTests : BaseIntegrationTestFixture
    {
        [Test]
        public void TestThatBookCollectionCanBeObtained()
        {
            // Prepare results
            LifetimeScope.Resolve<ICreateResultFileCommand>().Execute(null);

            var myConfigurationManager = LifetimeScope.Resolve<IPlannerSettingsProvider>();
            var derivedPlannerSettings = myConfigurationManager.GetDerivedPlannerSettings(false);

            var bookCollectionProvider = LifetimeScope.Resolve<IBookCollectionProvider>();
            var bookCollection = bookCollectionProvider.GetBookCollection(derivedPlannerSettings);

            var calendarService = LifetimeScope.Resolve<ICalendarService>();
            calendarService.CreateCalendar(derivedPlannerSettings.DateColumnInt,
                derivedPlannerSettings.VejlederColumnInt,
                derivedPlannerSettings.EventColumnInt, derivedPlannerSettings.PlaceColumnInt, "me",
                derivedPlannerSettings, bookCollection);
        }
    }
}