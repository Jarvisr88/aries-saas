namespace DevExpress.Pdf.ContentGeneration.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SCRIPT_ANALYSIS
    {
        private const short logicalOrderMask = 0x4000;
        private const short scriptIdMask = 0x3f;
        private short value;
        private SCRIPT_STATE s;
        public SCRIPT_STATE State =>
            this.s;
        public int ScriptId
        {
            get => 
                this.value & 0x3f;
            set
            {
                this.value = (short) (this.value & -64);
                this.value = (short) (this.value | ((short) (value & 0x3f)));
            }
        }
        public bool LogicalOrder
        {
            get => 
                (this.value & 0x4000) != 0;
            set
            {
                if (value)
                {
                    this.value = (short) (this.value | 0x4000);
                }
                else
                {
                    this.value = (short) (this.value & -16385);
                }
            }
        }
    }
}

