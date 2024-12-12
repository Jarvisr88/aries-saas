namespace DevExpress.Xpo.DB.Exceptions
{
    using System;
    using System.ComponentModel;
    using System.Runtime.Serialization;

    [Serializable]
    public class PropertyTypeMappingMissingException : Exception
    {
        private Type propertyType;

        public PropertyTypeMappingMissingException(Type objectType) : base(DbRes.GetString("ConnectionProvider_TypeMappingMissing", objArray1))
        {
            object[] objArray1 = new object[] { objectType.ToString() };
            this.propertyType = objectType;
        }

        protected PropertyTypeMappingMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        [Obsolete("Use Message instead.", false), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Type PropertyType =>
            this.propertyType;
    }
}

