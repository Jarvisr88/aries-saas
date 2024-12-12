namespace DevExpress.Data.IO
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.IO;
    using System.Text;

    internal class TypedBinaryWriterEx2 : TypedBinaryWriter
    {
        private ICustomObjectConverter customConverter;

        public TypedBinaryWriterEx2(Stream input, ICustomObjectConverter customConverter);
        public TypedBinaryWriterEx2(Stream input, Encoding encoding, ICustomObjectConverter customConverter);
        public override void WriteObject(object value);
    }
}

