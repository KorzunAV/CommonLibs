using System;
using System.IO;
using NUnit.Framework;

namespace CommonLibs.Serialization.Tests
{
    [TestFixture]
    public class SerializationBinaryTest : TestFixtureBase
    {
        [Test]
        public void SerializationTest()
        {
            using (var stream = new MemoryStream())
            {
                BinarySerialization.Serialize(TestClass, stream);
                stream.Position = 0;
                var testClass = BinarySerialization.Deserialize<TestClass>(stream);

                Assert.IsTrue(Math.Abs(TestClass.DoubleProperty - testClass.DoubleProperty) < double.Epsilon);
                Assert.IsTrue(TestClass.IntProperty == testClass.IntProperty);
                Assert.IsTrue(TestClass.StringProperty == testClass.StringProperty);
            }
        }
    }
}
