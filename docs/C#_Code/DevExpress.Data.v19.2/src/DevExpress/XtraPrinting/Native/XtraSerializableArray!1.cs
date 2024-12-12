namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing;
    using System;

    public class XtraSerializableArray<T>
    {
        private T[] values;

        [XtraSerializableProperty]
        public T[] Values
        {
            get => 
                this.values;
            set => 
                this.values = value;
        }
    }
}

