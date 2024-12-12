namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_PROPERTY_BINDING
    {
        private IntPtr propertyName;
        private IntPtr setFunction;
        private IntPtr getFunction;
        public D2D1_PROPERTY_BINDING(IntPtr propertyName, IntPtr setFunction, IntPtr getFunction)
        {
            this.propertyName = propertyName;
            this.setFunction = setFunction;
            this.getFunction = getFunction;
        }
    }
}

