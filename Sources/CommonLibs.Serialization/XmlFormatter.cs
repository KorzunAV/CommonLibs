using System.IO;
using System.Runtime;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace CommonLibs.Serialization
{
    public class Formatter<T> : IFormatter
        where T : class
    {
        public object Deserialize(Stream serializationStream)
        {
            var serializer = new XmlSerializer(typeof(T));
            return serializer.Deserialize(serializationStream);
        }

        public void Serialize(Stream serializationStream, object graph)
        {
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(serializationStream, graph);
        }

        public ISurrogateSelector SurrogateSelector
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get;
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            set;
        }

        public SerializationBinder Binder
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get;
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            set;
        }

        public StreamingContext Context
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get;
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            set;
        }
    }

}
