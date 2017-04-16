using System;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using ajf.ns_planner.shared2.Emails;
using ajf.ns_planner.shared2.Interfaces;
using ajf.ns_planner.shared2.ViewModels;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using SendGrid;

namespace ajf.ns_planner.test.UnitTests
{
    public class SingleMailSendingServiceTests : BaseUnitTestFixture
    {
        [Test]
        public async Task TestThatSingleMailCanBePassedOnToTransport()
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
            var fileContentsProviderMock = new Mock<IFileContentsProvider>();
            fileContentsProviderMock.Setup(x => x.GetFileContents("")).Returns(new[]
            {
                "<!doctype html>",
                "<!--receiver: andersjuulsfirma@gmail.com -->",
                "<!--mailsubject: Test Subject -->",
                "</html>"
            });
            Fixture.Register(() => fileContentsProviderMock.Object);

            var transportMock = new Mock<ITransport>();

            var sut = Fixture.Build<SingleMailSendingService>().Create();

            // Act
            await
                sut.SendSingleMail(new MailAddress("andersjuulsfirma@gmail.com"), transportMock.Object, DateTime.Now, "",true, "andersjuulsfirma@gmail.com");

            // Assert
            transportMock.Verify(x => x.DeliverAsync(It.IsAny<ISendGrid>()), Times.Once);
        }
    }
}