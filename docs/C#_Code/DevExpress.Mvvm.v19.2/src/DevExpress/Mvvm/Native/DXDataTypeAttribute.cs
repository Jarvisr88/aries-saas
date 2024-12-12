namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class DXDataTypeAttribute : DXValidationAttribute
    {
        protected DXDataTypeAttribute()
        {
            throw new NotSupportedException();
        }

        protected DXDataTypeAttribute(Func<string> errorMessageAccessor, Func<string> defaultErrorMessageAccessor, PropertyDataType dataType) : base(errorMessageAccessor, defaultErrorMessageAccessor)
        {
            this.DataType = dataType;
        }

        public PropertyDataType DataType { get; private set; }
    }
}

