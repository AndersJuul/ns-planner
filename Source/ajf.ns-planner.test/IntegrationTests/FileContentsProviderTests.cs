using System.Linq;
using System.Xml;
using ajf.ns_planner.shared2.Emails;
using ajf.ns_planner.shared2.Interfaces;
using Autofac;
using NUnit.Framework;

namespace ajf.ns_planner.test.IntegrationTests
{
    public class FileContentsProviderTests : BaseIntegrationTestFixture
    {
        [Test]
        public void TestThatAFileCanBeRead()
        {
            // Arrange
            // Prepare results
            LifetimeScope.Resolve<ICreateResultFileCommand>().Execute(null);
            LifetimeScope.Resolve<ICreateEmailsCommand>().Execute(null);
            var nsContext = LifetimeScope.Resolve<INsContext>();
            var s = nsContext.Directory + nsContext.ConfigFile;

            var sut = LifetimeScope.Resolve<IFileContentsProvider>();

            // Act
            var fileContents = sut.GetFileContents(s);

            // Assert that contents can be read and recognized as xml
            var fileAsSingleString = fileContents.Aggregate("", (current, x) => current + " " + x);
            var doc = new XmlDocument();
            doc.LoadXml(fileAsSingleString);
        }
    }
}