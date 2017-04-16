using ajf.ns_planner.shared2.NsCommands;
using NUnit.Framework;

namespace ajf.ns_planner.test.IntegrationTests
{
    public class ColumnConverterTests : BaseIntegrationTestFixture
    {
        [TestCase("A",Result=0)]
        [TestCase("B",Result = 1)]
        [TestCase("C",Result = 2)]
        [TestCase("AA", Result = 26)]
        [TestCase("AB", Result = 27)]
        [TestCase("AC", Result = 28)]
        [TestCase("AD", Result = 29)]
        public int HandleTest(string columnName)
        {
            // Arrange
            var columnConverter = new ColumnConverter();

            // Act
            var nsMergeExcelResponse = columnConverter.ConvertToIndex(columnName);

            // Assert
            return nsMergeExcelResponse;
        }
    }
}