namespace DevExpress.Data.IO
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.IO;
    using System.Text;

    public class TypedBinaryReader : BinaryReader
    {
        public TypedBinaryReader(Stream input);
        public TypedBinaryReader(Stream input, Encoding encoding);
        public static TypedBinaryReader CreateReader(Stream input, ICustomObjectConverter customConverter);
        public static TypedBinaryReader CreateReader(Stream input, Encoding encoding, ICustomObjectConverter customConverter);
        private DateTime ReadDateTime();
        private decimal ReadDecimalOptimized(byte decimalType);
        private object ReadEnum(Type type, byte emptyValue);
        private Guid ReadGuid();
        private short ReadInt16Optimized(byte intType);
        private int ReadInt32Optimized(byte intType);
        private long ReadInt64Optimized(byte intType);
        public string ReadNullableString();
        public T ReadObject<T>();
        public virtual object ReadObject(Type type);
        private object ReadSerializableObject();
        public Type ReadType();
        public object ReadTypedObject();
    }
}

