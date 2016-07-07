using System;
using System.IO;
using NUnit.Framework;

namespace CommonLibs.Serialization.Tests
{
    [TestFixture]
    public class SerializationXmlTest : TestFixtureBase
    {
        [Test]
        public void SerializationTest()
        {
            using (var stream = new MemoryStream())
            {
                XmlSerialization.Serialize(TestClass, stream);
                stream.Position = 0;
                var testClass = XmlSerialization.Deserialize<TestClass>(stream);

                Assert.IsTrue(Math.Abs(TestClass.DoubleProperty - testClass.DoubleProperty) < double.Epsilon);
                Assert.IsTrue(TestClass.IntProperty == testClass.IntProperty);
                Assert.IsTrue(TestClass.StringProperty == testClass.StringProperty);
            }
        }
    }
}
