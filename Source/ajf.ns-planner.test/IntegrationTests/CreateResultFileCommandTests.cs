using ajf.ns_planner.shared2.Interfaces;
using Autofac;
using NUnit.Framework;

namespace ajf.ns_planner.test.IntegrationTests
{
    public class CreateResultFileCommandTests : BaseIntegrationTestFixture
    {
        [Test]
        public void TestThatResultFileIsWritten()
        {
            // Prepare results
            LifetimeScope.Resolve<ICreateResultFileCommand>().Execute(null);

            // Assert:
        }
    }
}