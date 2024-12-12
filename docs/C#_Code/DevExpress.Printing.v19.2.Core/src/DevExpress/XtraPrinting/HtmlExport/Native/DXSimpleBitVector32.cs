namespace DevExpress.XtraPrinting.HtmlExport.Native
{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    internal struct DXSimpleBitVector32
    {
        private int data;
        internal DXSimpleBitVector32(int data)
        {
            this.data = data;
        }

        internal int IntegerValue
        {
            get => 
                this.data;
            set => 
                this.data = value;
        }
        internal bool this[int bit]
        {
            get => 
                (this.data & bit) == bit;
            set
            {
                int data = this.data;
                if (value)
                {
                    this.data = data | bit;
                }
                else
                {
                    this.data = data & ~bit;
                }
            }
        }
        internal void Set(int bit)
        {
            this.data |= bit;
        }

        internal void Clear(int bit)
        {
            this.data &= ~bit;
        }
    }
}

