namespace DevExpress.Mvvm.Native
{
    using System;
    using System.IO;
    using System.Text;

    public static class SerializationHelper
    {
        public static void DeserializeFromString(string state, Action<Stream> deserializationMethod)
        {
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(state)))
            {
                deserializationMethod(stream);
            }
        }

        public static string SerializeToString(Action<Stream> serializationMethod)
        {
            string str = null;
            using (MemoryStream stream = new MemoryStream())
            {
                serializationMethod(stream);
                stream.Seek(0L, SeekOrigin.Begin);
                using (StreamReader reader = new StreamReader(stream))
                {
                    str = reader.ReadToEnd();
                }
            }
            return str;
        }
    }
}

