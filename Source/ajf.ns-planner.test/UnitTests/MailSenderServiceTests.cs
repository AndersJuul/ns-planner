using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using ajf.ns_planner.shared2.Emails;
using ajf.ns_planner.shared2.Interfaces;
using ajf.ns_planner.shared2.ViewModels;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace ajf.ns_planner.test.UnitTests
{
    public class MailSenderServiceTests : BaseUnitTestFixture
    {
        [Test]
        public async Task TestThatPreparedEmailsCanBeSend()
        {
            // Arrange
            var assembly = Assembly.GetAssembly(typeof (MainWindowViewModel));
            var location = assembly.Location;
            var directory = Path.GetFullPath(
                Path.GetDirectoryName(location) + @"\.." + @"\.." + @"\.." + @"\TestData\Ns.2016-1\");

            var plannerSettingsProviderMock = new Mock<IPlannerSettingsProvider>();
            var derivedPlannerSettingsMock = new Mock<IDerivedPlannerSettings>();
            derivedPlannerSettingsMock
                .Setup(x => x.SenderMailAddress)
                .Returns("andersjuulsfirma@gmail.com");
            derivedPlannerSettingsMock.Setup(x => x.HtmlOutDirectory).Returns(directory + "HtmlOut");
            plannerSettingsProviderMock
                .Setup(x => x.GetDerivedPlannerSettings(false))
                .Returns(derivedPlannerSettingsMock.Object);
            Fixture.Register(() => plannerSettingsProviderMock.Object);
            Fixture.Register(() => new Mock<ISingleMailSendingService>().Object);
            Fixture.Register(() => new Mock<ILogItemListViewModel>().Object);
            Fixture.Register(() => new Mock<IFileManager>().Object);
            var messageBoxServiceMock = new Mock<IMessageBoxService>();
            messageBoxServiceMock.Setup(x => x.Show(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(MessageBoxResult.OK);
            Fixture.Register(() => messageBoxServiceMock.Object);
            
            var sut = Fixture.Build<MailSenderService>().Create();

            // Act
            await sut.SendWaitingMails();

            // Assert
            //            Assert.IsTrue(workbookMock.Object == sut.DestinationBook);
        }
    }
}