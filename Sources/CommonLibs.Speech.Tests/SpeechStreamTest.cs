using System.IO;
using NUnit.Framework;

namespace CommonLibs.Speech.Tests
{
    [TestFixture]
    public class SpeechStreamTest
    {
        [Test]
        public void WriteTest()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var ss = new SpeechWriter();

                var voices = ss.AvailibleVoices();
                Assert.IsNotNull(voices);
                Assert.IsTrue(voices.Length > 0);

                ss.Write("some test text", ms, voices[0]);
                Assert.IsTrue(ms.Position > 0);
                var pos1 = ms.Position;


                ss.Write("some test text", ms);
                Assert.IsTrue(ms.Position > 0);
                var pos2 = ms.Position;

                Assert.IsTrue(pos2 - pos1 == pos1);

                ss.Write("some another test text", ms);
                Assert.IsTrue(ms.Position > 0);
                var pos3 = ms.Position;

                Assert.IsTrue(pos3 - pos2 != pos1);
            }
        }
    }
}
