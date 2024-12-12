namespace DevExpress.Pdf.ContentGeneration.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SCRIPT_STATE
    {
        private const short bidiLevelMask = 0x1f;
        private short value;
        public byte BidiLevel
        {
            get => 
                (byte) (this.value & 0x1f);
            set
            {
                this.value = (short) (this.value & -32);
                this.value = (short) (this.value | ((short) (value & 0x1f)));
            }
        }
    }
}

