namespace DevExpress.DirectX.Common.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DWRITE_SCRIPT_ANALYSIS
    {
        private short script1;
        private DWRITE_SCRIPT_SHAPES shapes1;
        public short script
        {
            get => 
                this.script1;
            set => 
                this.script1 = value;
        }
        public DWRITE_SCRIPT_SHAPES shapes
        {
            get => 
                this.shapes1;
            set => 
                this.shapes1 = value;
        }
    }
}

