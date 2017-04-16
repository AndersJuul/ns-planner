using ajf.ns_planner.shared2.BookHandling;
using ajf.ns_planner.shared2.Emails;
using ajf.ns_planner.shared2.Interfaces;
using Moq;
using NPOI.SS.UserModel;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace ajf.ns_planner.test.UnitTests
{
    public class MailMergingServiceTests : BaseUnitTestFixture
    {
        [Test]
        public void TestThatServiceCanProcessSunshine()
        {
            // Arrange
            var plannerSettingsProviderMock = new Mock<IPlannerSettingsProvider>();
            var derivedPlannerSettingsMock = new Mock<IDerivedPlannerSettings>();
            derivedPlannerSettingsMock
                .Setup(x => x.UnversionedDestinationFileFullPath)
                .Returns(@"Result.xls");
            plannerSettingsProviderMock
                .Setup(x => x.GetDerivedPlannerSettings(false))
                .Returns(derivedPlannerSettingsMock.Object);
            Fixture.Register(() => plannerSettingsProviderMock.Object);
            Fixture.Register(() => new Mock<IExcelBookService>().Object);
            Fixture.Register(() => new Mock<IFileContentsProvider>().Object);
            var bookCollectionProviderMock = new Mock<IBookCollectionProvider>();
            var bookCollectionMock = new Mock<IBookCollection>();
            bookCollectionProviderMock
                .Setup(x => x.GetBookCollection(derivedPlannerSettingsMock.Object))
                .Returns(bookCollectionMock.Object);
            Fixture.Register(() => bookCollectionProviderMock.Object);
            var logItemListViewModelMock = new Mock<ILogItemListViewModel>();
            Fixture.Register(() => logItemListViewModelMock.Object);
            var destinationSheetServiceMock = new Mock<IDestinationSheetService>();
            destinationSheetServiceMock.Setup(x => x.Get(bookCollectionMock.Object)).Returns(new Mock<ISheet>().Object);
            Fixture.Register(() => destinationSheetServiceMock.Object);

            var sut = Fixture.Build<MailMergingService>().Create();

            // Act
            sut.DoMailMerge();

            // Assert
            logItemListViewModelMock.Verify(x => x.CreateError(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void TestThatServiceFailsWithMissingTemplates()
        {
            // Arrange
            var plannerSettingsProviderMock = new Mock<IPlannerSettingsProvider>();
            var derivedPlannerSettingsMock = new Mock<IDerivedPlannerSettings>();
            derivedPlannerSettingsMock
                .Setup(x => x.UnversionedDestinationFileFullPath)
                .Returns(@"Result.xls");
            plannerSettingsProviderMock
                .Setup(x => x.GetDerivedPlannerSettings(false))
                .Returns(derivedPlannerSettingsMock.Object);
            Fixture.Register(() => plannerSettingsProviderMock.Object);
            Fixture.Register(() => new Mock<IExcelBookService>().Object);
            Fixture.Register(() => new Mock<IFileContentsProvider>().Object);
            var bookCollectionProviderMock = new Mock<IBookCollectionProvider>();
            Fixture.Register(() => bookCollectionProviderMock.Object);
            var logItemListViewModelMock = new Mock<ILogItemListViewModel>();
            Fixture.Register(() => logItemListViewModelMock.Object);
            var destinationSheetServiceMock = new Mock<IDestinationSheetService>();
            Fixture.Register(() => destinationSheetServiceMock.Object);

            var sut = Fixture.Build<MailMergingService>().Create();

            // Act
            sut.DoMailMerge();

            // Assert
            logItemListViewModelMock.Verify(x => x.CreateError(It.IsAny<string>()), Times.Once);
        }
    }
}