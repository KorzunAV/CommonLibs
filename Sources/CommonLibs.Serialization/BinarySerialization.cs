using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CommonLibs.Serialization
{
    public class BinarySerialization : SerializationBase
    {
        public static void Serialize<T>(T obj, string fileName, FileMode fileMode)
            where T : class
        {
            Serialize(obj, fileName, fileMode, new BinaryFormatter());
        }

        public static void Serialize<T>(T obj, Stream stream)
            where T : class
        {
            Serialize(obj, stream, new BinaryFormatter());
        }

        public static T Deserialize<T>(string fileName)
                     where T : class
        {
            return Deserialize<T>(fileName, new BinaryFormatter());
        }

        public static T Deserialize<T>(Stream stream)
                       where T : class
        {
            return Deserialize<T>(stream, new BinaryFormatter());
        }
    }
}
