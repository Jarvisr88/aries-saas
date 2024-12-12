namespace DevExpress.DirectX.Common.Direct3D
{
    using DevExpress.DirectX.Common.DXGI;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D3D11_TEXTURE2D_DESC
    {
        private int width;
        private int height;
        private int mipLevels;
        private int arraySize;
        private DXGI_FORMAT format;
        private DXGI_SAMPLE_DESC sampleDescription;
        private D3D11_USAGE usage;
        private D3D11_BIND_FLAG bindFlags;
        private D3D11_CPU_ACCESS_FLAG cpuAccessFlags;
        private D3D11_RESOURCE_MISC_FLAG miscFlags;
        public D3D11_BIND_FLAG BindFlags
        {
            get => 
                this.bindFlags;
            set => 
                this.bindFlags = value;
        }
        public D3D11_CPU_ACCESS_FLAG CpuAccessFlags
        {
            get => 
                this.cpuAccessFlags;
            set => 
                this.cpuAccessFlags = value;
        }
        public int Width
        {
            get => 
                this.width;
            set => 
                this.width = value;
        }
        public int Height
        {
            get => 
                this.height;
            set => 
                this.height = value;
        }
        public D3D11_USAGE Usage
        {
            get => 
                this.usage;
            set => 
                this.usage = value;
        }
        public D3D11_RESOURCE_MISC_FLAG MiscFlags
        {
            get => 
                this.miscFlags;
            set => 
                this.miscFlags = value;
        }
        public DXGI_FORMAT Format
        {
            get => 
                this.format;
            set => 
                this.format = value;
        }
        public D3D11_TEXTURE2D_DESC(int width, int height, int mipLevels, int arraySize, DXGI_FORMAT format, DXGI_SAMPLE_DESC sampleDescription, D3D11_USAGE usage, D3D11_BIND_FLAG bindFlags, D3D11_CPU_ACCESS_FLAG cpuAccessFlags, D3D11_RESOURCE_MISC_FLAG miscFlags)
        {
            this.width = width;
            this.height = height;
            this.mipLevels = mipLevels;
            this.arraySize = arraySize;
            this.format = format;
            this.sampleDescription = sampleDescription;
            this.usage = usage;
            this.bindFlags = bindFlags;
            this.cpuAccessFlags = cpuAccessFlags;
            this.miscFlags = miscFlags;
        }
    }
}

