namespace DMEWorks.Forms
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct VersionStorage<T> : IVersionStorage
    {
        private bool isNull;
        private T value;
        public bool IsNull
        {
            get => 
                this.isNull;
            set => 
                this.isNull = value;
        }
        public object Value
        {
            get => 
                !this.isNull ? ((object) this.value) : ((object) DBNull.Value);
            set
            {
                if ((value is DBNull) || (value == null))
                {
                    this.isNull = true;
                }
                else
                {
                    if (!(value is IConvertible))
                    {
                        throw new ArgumentException("value must be IConvertible");
                    }
                    this.value = (T) ((IConvertible) value).ToType(typeof(T), null);
                    this.isNull = false;
                }
            }
        }
    }
}

