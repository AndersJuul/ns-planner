using System;
using System.IO;
using ajf.ns_planner.shared2.Interfaces;
using ajf.ns_planner.shared2.NsCommands;
using Autofac;
using NPOI.HPSF;
using NUnit.Framework;

namespace ajf.ns_planner.test.IntegrationTests
{
    public class NsCommandHandlerTests : BaseIntegrationTestFixture
    {
        [TestCase("B.xls","P")]
        [TestCase("C.xls","T")]
        [TestCase("D.xls","U")]
        public void HandleTest(string lookupName, string columnNameInSource)
        {
            // Arrange
            var nsContext = LifetimeScope.Resolve<INsContext>();
            var pathToSource = nsContext.Directory + "A.xls";
            var pathToDestination = Path.GetTempPath()+ Guid.NewGuid() + ".xls";
            var pathToLookupSource = nsContext.Directory + lookupName;

            var nsMergeExcelCommand = new NsMergeExcelCommand(
                pathToSource,
                pathToDestination,
                pathToLookupSource,
                columnNameInSource
                );

            var nsCommandHandler = LifetimeScope.Resolve<INsCommandHandler<NsMergeExcelCommand, NsMergeExcelResponse>>();

            // Act
            var nsMergeExcelResponse = nsCommandHandler.Handle(nsMergeExcelCommand);

            // Assert
            Assert.AreEqual(0, nsMergeExcelResponse.Errors.Count);
            Assert.IsTrue(File.Exists(nsMergeExcelCommand.PathToDestination));
        }
    }
}