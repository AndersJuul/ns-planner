using System.Collections.Generic;
using ajf.ns_planner.shared2.BasicMerge;
using ajf.ns_planner.shared2.BasicMerge.Validation;
using ajf.ns_planner.shared2.BookHandling;
using ajf.ns_planner.shared2.Interfaces;
using Moq;
using NPOI.SS.UserModel;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace ajf.ns_planner.test.UnitTests
{
    public class MergeServiceTests : BaseUnitTestFixture
    {
        [Test]
        public void TestThatMailMerge()
        {
            // Arrange
            var plannerSettingsProviderMock = new Mock<IPlannerSettingsProvider>();
            var derivedPlannerSettingsMock = new Mock<IDerivedPlannerSettings>();
            derivedPlannerSettingsMock
                .Setup(x => x.FirstWriteableColumnInt)
                .Returns(Fixture.Create<int>());
            derivedPlannerSettingsMock
                .Setup(x => x.UnversionedDestinationFileFullPath).Returns(Fixture.Create<string>());
            plannerSettingsProviderMock
                .Setup(x => x.GetDerivedPlannerSettings(true))
                .Returns(derivedPlannerSettingsMock.Object);
            Fixture.Register(() => plannerSettingsProviderMock.Object);
            Fixture.Register(() => new Mock<IExcelBookService>().Object);
            Fixture.Register(() => new Mock<ICalendarService>().Object);
            var bookCollectionProviderMock = new Mock<IBookCollectionProvider>();
            var bookCollectionMock = new Mock<IBookCollection>();
            bookCollectionProviderMock
                .Setup(x => x.GetBookCollection(derivedPlannerSettingsMock.Object))
                .Returns(bookCollectionMock.Object);
            Fixture.Register(() => bookCollectionProviderMock.Object);
            var destinationSheetServiceMock = new Mock<IDestinationSheetService>();
            destinationSheetServiceMock.Setup(x => x.Get(bookCollectionMock.Object)).Returns(new Mock<ISheet>().Object);
            Fixture.Register(() => destinationSheetServiceMock.Object);
            Fixture.Register(() => new Mock<IFileManager>().Object);
            var logItemListViewModelMock = new Mock<ILogItemListViewModel>();
            Fixture.Register(() => logItemListViewModelMock.Object);
            Fixture.Register(() => new Mock<ISingleRowMergeService>().Object);
            Fixture.Register(()=>
            {
                var mock = new Mock<IValidationService>();
                mock.Setup(x => x.GetValidationProblems(derivedPlannerSettingsMock.Object)).Returns(new List<IValidationProblem>());
                return mock.Object;
            });
            var sut = Fixture.Build<MergeService>().Create();

            // Act
            sut.MergeOnce();

            // Assert
            logItemListViewModelMock.Verify(x => x.CreateError(It.IsAny<string>()), Times.Never);
        }
    }
}