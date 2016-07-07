using NUnit.Framework;

namespace CommonLibs.Serialization.Tests
{
    [TestFixture]
    public class TestFixtureBase
    {
        protected TestClass TestClass;

        [TestFixtureSetUp]
        public void SetUp()
        {
            TestClass = new TestClass
            {
                DoubleProperty = 66.66,
                IntProperty = 42,
                StringProperty = "Test"
            };
        }
    }
}
