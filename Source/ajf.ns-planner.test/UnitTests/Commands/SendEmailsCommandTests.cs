using ajf.ns_planner.shared2.Commands;
using ajf.ns_planner.shared2.Interfaces;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace ajf.ns_planner.test.UnitTests.Commands
{
    public class SendEmailsCommandTests : BaseUnitTestFixture
    {
        [Test]
        public void TestThatServiceIsCalled()
        {
            // Arrange
            var mailMergingServiceMock = new Mock<IMailSenderService>();
            Fixture.Register(() => mailMergingServiceMock.Object);

            var sut = new SendEmailsCommand(Fixture.Create<IMailSenderService>(), new Mock<IBackupService>().Object);

            // Act
            sut.Execute(null);

            // Assert
            mailMergingServiceMock.Verify(x => x.SendWaitingMails(), Times.Once);
        }

        [Test]
        public void TestThatCommandIsEnabled()
        {
            // Arrange
            var mailMergingServiceMock = new Mock<IMailSenderService>();
            Fixture.Register(() => mailMergingServiceMock.Object);

            var sut = new SendEmailsCommand(Fixture.Create<IMailSenderService>(), new Mock<IBackupService>().Object);

            // Act
            var canExecute = sut.CanExecute(null);

            // Assert
            Assert.IsTrue(canExecute);
        }
    }
}