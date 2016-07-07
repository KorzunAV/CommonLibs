using System;

namespace CommonLibs.Serialization.Tests
{
    [Serializable]
    public class TestClass
    {
        public string StringProperty { get; set; }

        public int IntProperty { get; set; }

        public double DoubleProperty { get; set; }
    }
}
