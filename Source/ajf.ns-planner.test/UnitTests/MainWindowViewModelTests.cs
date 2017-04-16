using ajf.ns_planner.shared2.Interfaces;
using ajf.ns_planner.shared2.ViewModels;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace ajf.ns_planner.test.UnitTests
{
    public class MainWindowViewModelTests : BaseUnitTestFixture
    {
        private MainWindowViewModel _sut;

        [Test]
        public void TestThatTitleIsSet()
        {
            // Arrange

            // Act
            var title = _sut.Title;

            // Assert
            Assert.IsNotNull(title);
        }

        [SetUp]
        public void SetUp()
        {
            // Arrange
            Fixture.Register(() => new Mock<ICreateResultFileCommand>().Object);
            Fixture.Register(() => new Mock<ISendEmailsCommand>().Object);
            Fixture.Register(() => new Mock<ICreateEmailsCommand>().Object);
            Fixture.Register(() => new Mock<ILogItemListViewModel>().Object);
            Fixture.Register(() => new Mock<IChangeConfigCommand>().Object);
            Fixture.Register(() => new Mock<INsContext>().Object);

            _sut = Fixture.Create<MainWindowViewModel>();
        }

        [Test]
        public void TestThatCreateEmailsCommandIsSet()
        {
            // Arrange

            // Act
            var command = _sut.CreateEmailsCommand;

            // Assert
            Assert.IsNotNull(command);
        }

        [Test]
        public void TestThatCreateResultFileCommandIsSet()
        {
            // Arrange

            // Act
            var command = _sut.CreateResultFileCommand;

            // Assert
            Assert.IsNotNull(command);
        }

        [Test]
        public void TestThatSendEmailsCommandIsSet()
        {
            // Arrange

            // Act
            var command = _sut.SendEmailsCommand;

            // Assert
            Assert.IsNotNull(command);
        }
    }
}