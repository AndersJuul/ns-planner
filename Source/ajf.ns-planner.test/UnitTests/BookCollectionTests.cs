using ajf.ns_planner.shared2.BookHandling;
using ajf.ns_planner.shared2.Interfaces;
using Moq;
using NPOI.HSSF.UserModel;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace ajf.ns_planner.test.UnitTests
{
    [TestFixture]
    public class BookCollectionTests : BaseUnitTestFixture
    {
        [Test]
        public void TestThatPropertyCanBeSet()
        {
            // Arrange
            Fixture.Register(() => new Mock<ICreateResultFileCommand>().Object);
            Fixture.Register(() => new Mock<ISendEmailsCommand>().Object);
            Fixture.Register(() => new Mock<ICreateEmailsCommand>().Object);
            Fixture.Register(() => new Mock<ILogItemListViewModel>().Object);
            var workbookMock = new Mock<HSSFWorkbook>();

            var sut = new BookCollection();

            // Act
            sut.DestinationBook = workbookMock.Object;

            // Assert
            Assert.IsTrue(workbookMock.Object == sut.DestinationBook);
        }
    }
}