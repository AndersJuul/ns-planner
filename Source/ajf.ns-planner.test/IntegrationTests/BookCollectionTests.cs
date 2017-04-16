using ajf.ns_planner.shared2.Interfaces;
using Autofac;
using NUnit.Framework;

namespace ajf.ns_planner.test.IntegrationTests
{
    public class BookCollectionTests : BaseIntegrationTestFixture
    {
        [Test]
        public void TestThatBookCollectionCanBeObtained()
        {
            var bookCollectionProvider = LifetimeScope.Resolve<IBookCollectionProvider>();
            var myConfigurationManager = LifetimeScope.Resolve<IPlannerSettingsProvider>();
            var derivedPlannerSettings = myConfigurationManager.GetDerivedPlannerSettings(false);

            var bookCollection = bookCollectionProvider.GetBookCollection(derivedPlannerSettings);

            Assert.IsNotNull(bookCollection);
        }
    }
}