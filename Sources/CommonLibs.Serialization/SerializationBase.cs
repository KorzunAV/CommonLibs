using System.IO;
using System.Runtime.Serialization;

namespace CommonLibs.Serialization
{
    public class SerializationBase
    {
        public static void Serialize<T>(T obj, string fileName, FileMode fileMode, IFormatter formatter)
            where T : class
        {
            using (Stream fs = new FileStream(fileName, fileMode))
            {
                Serialize(obj, fs, formatter);
            }
        }

        public static void Serialize<T>(T obj, Stream stream, IFormatter formatter)
           where T : class
        {
            formatter.Serialize(stream, obj);
        }


        public static T Deserialize<T>(string fileName, IFormatter formatter)
          where T : class
        {
            T loadConfig;
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                loadConfig = Deserialize<T>(fs, formatter);
            }

            return loadConfig;
        }

        public static T Deserialize<T>(Stream stream, IFormatter formatter)
           where T : class
        {
            return formatter.Deserialize(stream) as T;
        }
    }
}