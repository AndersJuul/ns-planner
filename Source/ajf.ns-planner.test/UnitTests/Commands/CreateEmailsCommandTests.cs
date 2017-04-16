using ajf.ns_planner.shared2.Commands;
using ajf.ns_planner.shared2.Interfaces;
using Moq;
using NUnit.Framework;

namespace ajf.ns_planner.test.UnitTests.Commands
{
    public class CreateEmailsCommandTests : BaseUnitTestFixture
    {
        private Mock<IMailMergingService> _mailMergingServiceMock;
        private CreateEmailsCommand _sut;

        [SetUp]
        public void SetUp()
        {
            _mailMergingServiceMock = new Mock<IMailMergingService>();

            _sut = new CreateEmailsCommand(_mailMergingServiceMock.Object, new Mock<ILogItemListViewModel>().Object, new Mock<IBackupService>().Object);
        }

        [Test]
        public void TestThatServiceIsCalled()
        {
            // Arrange

            // Act
            _sut.Execute(null);

            // Assert
            _mailMergingServiceMock.Verify(x => x.DoMailMerge(), Times.Once);
        }

        [Test]
        public void TestThatCommandIsEnabled()
        {
            // Arrange

            // Act
            var canExecute = _sut.CanExecute(null);

            // Assert
            Assert.IsTrue(canExecute);
        }
    }
}