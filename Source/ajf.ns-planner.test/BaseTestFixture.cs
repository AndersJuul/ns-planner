using NUnit.Framework;
using Ploeh.AutoFixture;

namespace ajf.ns_planner.test
{
    [TestFixture]
    public abstract class BaseTestFixture
    {
        protected IFixture Fixture { get; private set; }

        [SetUp]
        public virtual void Setup()
        {
            Fixture = new Fixture();
        }

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
        }
    }
}