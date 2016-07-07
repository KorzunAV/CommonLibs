using System.IO;

namespace CommonLibs.Serialization
{
    public class XmlSerialization : SerializationBase
    {
        public static void Serialize<T>(T obj, string fileName, FileMode fileMode)
            where T : class
        {
            Serialize(obj, fileName, fileMode, new Formatter<T>());
        }

        public static void Serialize<T>(T obj, Stream stream)
           where T : class
        {
            Serialize(obj, stream, new Formatter<T>());
        }


        public static T Deserialize<T>(string fileName) where T : class
        {
            return Deserialize<T>(fileName, new Formatter<T>());
        }

        public static T Deserialize<T>(Stream stream)
                       where T : class
        {
            return Deserialize<T>(stream, new Formatter<T>());
        }
    }

}
