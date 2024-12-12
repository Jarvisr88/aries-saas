namespace ActiproSoftware.Products
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable"), SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors")]
    public class LicenseException : System.ComponentModel.LicenseException
    {
        public LicenseException(string message) : this(null, message)
        {
        }

        public LicenseException(Type type, string message) : base(type, null, message)
        {
        }
    }
}

