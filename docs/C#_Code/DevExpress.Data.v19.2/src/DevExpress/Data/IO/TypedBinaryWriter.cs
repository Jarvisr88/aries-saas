namespace DevExpress.Data.IO
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.IO;
    using System.Text;

    public class TypedBinaryWriter : BinaryWriter
    {
        public TypedBinaryWriter(Stream input);
        public TypedBinaryWriter(Stream input, Encoding encoding);
        public static TypedBinaryWriter CreateWriter(Stream input, ICustomObjectConverter customConverter);
        public static TypedBinaryWriter CreateWriter(Stream input, Encoding encoding, ICustomObjectConverter customConverter);
        internal static bool IsRequireSerializableWriter(Type type);
        private bool IsSmartWrittingType(Type type);
        private void WriteDateTime(DateTime dateTime);
        private void WriteDecimal(decimal value);
        private void WriteEnum(Type type, object value);
        private void WriteGuid(Guid guid);
        private void WriteInt16(short value);
        private void WriteInt32(int value);
        private void WriteInt64(long value);
        public void WriteNullableString(string str);
        public virtual void WriteObject(object value);
        private void WriteSerializable(object value);
        public void WriteType(object value);
        public void WriteType(Type type);
        public void WriteTypedObject(object value);
    }
}

