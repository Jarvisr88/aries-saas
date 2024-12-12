namespace DevExpress.Xpf.Core.Serialization
{
    using System;
    using System.Runtime.CompilerServices;

    public class DuplicateSerializationIDException : Exception
    {
        private const string messageFormat = "Another object with the same SerializationID (\"{0}\") exists.";

        private DuplicateSerializationIDException(IDXSerializable dxObj, string id) : base($"Another object with the same SerializationID ("{id}") exists.")
        {
            this.FullPath = dxObj.FullPath;
        }

        public static void Assert(IDXSerializable dxObj1, IDXSerializable dxObj2)
        {
            if (!ReferenceEquals(dxObj1.Source, dxObj2.Source))
            {
                throw new DuplicateSerializationIDException(dxObj1, DXSerializer.GetSerializationID(dxObj1.Source));
            }
        }

        public string FullPath { get; private set; }
    }
}

