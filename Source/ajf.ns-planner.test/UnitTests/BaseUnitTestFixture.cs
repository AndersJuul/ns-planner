using NUnit.Framework;

namespace ajf.ns_planner.test.UnitTests
{
    [TestFixture]
    public abstract class BaseUnitTestFixture : BaseTestFixture
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();
        }
    }
}