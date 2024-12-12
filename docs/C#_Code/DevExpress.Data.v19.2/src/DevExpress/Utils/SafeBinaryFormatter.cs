namespace DevExpress.Utils
{
    using DevExpress.Data.Internal;
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization.Formatters.Binary;

    public static class SafeBinaryFormatter
    {
        private static BinaryFormatter instanceCore;
        [ThreadStatic]
        private static int binaryFormatterDisabledCounter;
        internal static readonly Type BinaryType = typeof(byte[]);

        internal static bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType, Func<ITypeDescriptorContext, Type, bool> baseCanConvertFrom, bool allowBinaryType = true) => 
            (!allowBinaryType || !BinaryType.Equals(sourceType)) ? baseCanConvertFrom(context, sourceType) : true;

        internal static bool CanConvertTo(ITypeDescriptorContext context, Type destinationType, Func<ITypeDescriptorContext, Type, bool> baseCanConvertTo, bool allowBinaryType = true) => 
            (!allowBinaryType || !BinaryType.Equals(destinationType)) ? baseCanConvertTo(context, destinationType) : true;

        [DXHelpExclude(true), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static TFormatter Configure<TFormatter>(TFormatter formatter) where TFormatter: IFormatter => 
            SafeSerializationBinder.Initialize<TFormatter>(formatter);

        internal static object ConvertFrom(object value, Func<object, object> convertFromFallback, bool allowBinaryType = true) => 
            (!allowBinaryType || !(value is byte[])) ? convertFromFallback(value) : Deserialize(value as byte[]);

        internal static object ConvertTo(object value, Type destinationType, Func<object, object> convertToFallback, bool allowBinaryType = true) => 
            (!allowBinaryType || !BinaryType.Equals(destinationType)) ? convertToFallback(value) : Serialize(value);

        public static object Deserialize(Stream stream) => 
            ((stream == null) || IsBinaryFormatterDisabled) ? null : Instance.Deserialize(stream);

        public static object Deserialize(byte[] bytes)
        {
            if ((bytes == null) || ((bytes.Length == 0) || IsBinaryFormatterDisabled))
            {
                return null;
            }
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                return Instance.Deserialize(stream);
            }
        }

        public static object Deserialize(string base64string)
        {
            if (string.IsNullOrEmpty(base64string) || IsBinaryFormatterDisabled)
            {
                return null;
            }
            using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(base64string)))
            {
                return Instance.Deserialize(stream);
            }
        }

        public static IDisposable DisableBinaryFormatter() => 
            new BinaryFormatterDisableContext();

        public static byte[] Serialize(object graph)
        {
            if ((graph == null) || IsBinaryFormatterDisabled)
            {
                return null;
            }
            using (MemoryStream stream = new MemoryStream())
            {
                Instance.Serialize(stream, graph);
                return stream.ToArray();
            }
        }

        public static void Serialize(Stream stream, object graph)
        {
            if ((graph != null) && !IsBinaryFormatterDisabled)
            {
                Instance.Serialize(stream, graph);
            }
        }

        private static BinaryFormatter Instance =>
            SafeSerializationBinder.CreateFormatter(ref instanceCore);

        private static bool IsBinaryFormatterDisabled =>
            binaryFormatterDisabledCounter > 0;

        private sealed class BinaryFormatterDisableContext : IDisposable
        {
            public BinaryFormatterDisableContext()
            {
                SafeBinaryFormatter.binaryFormatterDisabledCounter++;
            }

            void IDisposable.Dispose()
            {
                SafeBinaryFormatter.binaryFormatterDisabledCounter--;
            }
        }
    }
}

