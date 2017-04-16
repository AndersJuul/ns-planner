using System.Linq;
using ajf.ns_planner.shared2.Interfaces;
using Autofac;
using NUnit.Framework;

namespace ajf.ns_planner.test.IntegrationTests
{
    public class CreateEmailsCommandTests : BaseIntegrationTestFixture
    {
        [Test]
        public void TestThatEmailFilesAreWrittenWithoutErrors()
        {
            var logItemListViewModel = LifetimeScope.Resolve<ILogItemListViewModel>();
            // Prepare results
            LifetimeScope.Resolve<ICreateResultFileCommand>().Execute(null);

            // Prepare results
            LifetimeScope.Resolve<ICreateEmailsCommand>().Execute(null);

            // Assert:
            Assert.IsFalse(logItemListViewModel.Any(x => x.Type == "Error"));
        }
    }
}