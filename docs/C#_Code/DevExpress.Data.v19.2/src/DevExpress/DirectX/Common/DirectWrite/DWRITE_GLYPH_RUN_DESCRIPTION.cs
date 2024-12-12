namespace DevExpress.DirectX.Common.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DWRITE_GLYPH_RUN_DESCRIPTION
    {
        private IntPtr localeName;
        private IntPtr str;
        private int stringLength;
        private IntPtr clusterMap;
        private int textPosition;
        public IntPtr ClusterMap
        {
            get => 
                this.clusterMap;
            set => 
                this.clusterMap = value;
        }
        public int StringLength
        {
            get => 
                this.stringLength;
            set => 
                this.stringLength = value;
        }
    }
}

