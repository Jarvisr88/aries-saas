namespace DevExpress.DirectX.Common.DXGI
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DXGI_SAMPLE_DESC
    {
        private int count;
        private int quality;
        public int Count
        {
            get => 
                this.count;
            set => 
                this.count = value;
        }
        public int Quality
        {
            get => 
                this.quality;
            set => 
                this.quality = value;
        }
    }
}

