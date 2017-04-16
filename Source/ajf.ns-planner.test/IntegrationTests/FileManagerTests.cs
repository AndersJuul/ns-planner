using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using ajf.ns_planner.shared2.Interfaces;
using ajf.ns_planner.shared2.ViewModels;
using Autofac;
using NUnit.Framework;

namespace ajf.ns_planner.test.IntegrationTests
{
    public class FileManagerTests : BaseIntegrationTestFixture
    {
        [Test]
        public void TestThatFileCanBeCopied()
        {
            // Arrange
            var nsContext = LifetimeScope.Resolve<INsContext>();
            var directory = nsContext.Directory;

            var sut = LifetimeScope.Resolve<IFileManager>();

            var destination = @"c:\temp\" + Guid.NewGuid() + ".tmp";

            // Act
            sut.CopyWithOverwrite(directory + "a.xls", destination);

            // Assert
            Assert.IsTrue(File.Exists(destination));
        }
    }
}