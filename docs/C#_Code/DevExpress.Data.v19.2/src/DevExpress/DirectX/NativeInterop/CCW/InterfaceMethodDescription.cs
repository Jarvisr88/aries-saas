namespace DevExpress.DirectX.NativeInterop.CCW
{
    using System;

    internal class InterfaceMethodDescription
    {
        private readonly DevExpress.DirectX.NativeInterop.CCW.MethodDescription methodDescription;
        private readonly int offset;

        public InterfaceMethodDescription(DevExpress.DirectX.NativeInterop.CCW.MethodDescription methodDescription, int offset)
        {
            this.methodDescription = methodDescription;
            this.offset = offset;
        }

        public DevExpress.DirectX.NativeInterop.CCW.MethodDescription MethodDescription =>
            this.methodDescription;

        public int Offset =>
            this.offset;
    }
}

