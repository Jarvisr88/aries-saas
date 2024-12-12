namespace DMEWorks.Forms
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct DataStorage<T> : IDataStorage
    {
        private VersionStorage<T> defaultValue;
        private VersionStorage<T> current;
        private VersionStorage<T> original;
        public object OriginalValue
        {
            get => 
                this.original.Value;
            set => 
                this.original.Value = value;
        }
        public object Value
        {
            get => 
                this.current.Value;
            set => 
                this.current.Value = value;
        }
        public Type DataType =>
            typeof(T);
        public object DefaultValue
        {
            get => 
                this.defaultValue.Value;
            set => 
                this.defaultValue.Value = value;
        }
        public void AcceptChanges()
        {
            this.original = this.current;
        }

        public void RejectChanges()
        {
            this.current = this.original;
        }
    }
}

