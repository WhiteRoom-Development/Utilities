using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Utilities.Runtime
{
    public static class SerializationHelper
    {
        public static byte[] Serialize(object obj)
        {
            if (obj == null) return null;

            using var ms = new MemoryStream();
            var formatter = new BinaryFormatter();
            
            formatter.Serialize(ms, obj);
            return ms.ToArray();
        }

        public static bool Deserialize<T>(byte[] data, out T value) where T : struct
        {
            value = default;
            
            if (data == null) return false;

            using var ms = new MemoryStream(data);
            
            var formatter = new BinaryFormatter();

            var deserialize = formatter.Deserialize(ms);
            
            if (deserialize is T convertedData)
            {
                value = convertedData;
                return true;
            }
            
            return false;
        }
    }
}