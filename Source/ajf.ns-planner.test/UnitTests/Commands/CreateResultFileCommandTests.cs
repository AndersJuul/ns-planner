using ajf.ns_planner.shared2.Commands;
using ajf.ns_planner.shared2.Interfaces;
using Moq;
using NUnit.Framework;

namespace ajf.ns_planner.test.UnitTests.Commands
{
    public class CreateResultFileCommandTests : BaseUnitTestFixture
    {
        private Mock<ILogItemListViewModel> _logListViewModelMock;
        private Mock<IMergeService> _mergeServiceMock;
        private CreateResultFileCommand _sut;

        [SetUp]
        public void SetUp()
        {
            _mergeServiceMock = new Mock<IMergeService>();
            _logListViewModelMock = new Mock<ILogItemListViewModel>();

            _sut = new CreateResultFileCommand(_mergeServiceMock.Object, _logListViewModelMock.Object, new Mock<IBackupService>().Object);
        }

        [Test]
        public void TestThatServiceIsCalled()
        {
            // Arrange

            // Act
            _sut.Execute(null);

            // Assert
            _mergeServiceMock.Verify(x => x.MergeOnce(), Times.Once);
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