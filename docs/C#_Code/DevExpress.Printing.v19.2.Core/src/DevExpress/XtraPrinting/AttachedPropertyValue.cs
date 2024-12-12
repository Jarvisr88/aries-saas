namespace DevExpress.XtraPrinting
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct AttachedPropertyValue
    {
        private object value;
        private short propertyIndex;
        public short PropertyIndex
        {
            get => 
                this.propertyIndex;
            private set => 
                this.propertyIndex = value;
        }
        public object Value
        {
            get => 
                this.value;
            set => 
                this.value = value;
        }
        public AttachedPropertyValue(short propertyIndex)
        {
            this.value = null;
            this.propertyIndex = propertyIndex;
        }
    }
}

