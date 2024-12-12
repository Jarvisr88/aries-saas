namespace DevExpress.Data.IO
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.IO;
    using System.Text;

    internal class TypedBinaryReaderEx2 : TypedBinaryReader
    {
        private ICustomObjectConverter customConverter;

        public TypedBinaryReaderEx2(Stream input, ICustomObjectConverter customConverter);
        public TypedBinaryReaderEx2(Stream input, Encoding encoding, ICustomObjectConverter customConverter);
        public override object ReadObject(Type type);
    }
}

