namespace DevExpress.DirectX.NativeInterop.CCW
{
    using System;
    using System.Runtime.CompilerServices;

    public class MethodOffsetAttribute : Attribute
    {
        public MethodOffsetAttribute(int offset)
        {
            this.<Offset>k__BackingField = offset;
        }

        public int Offset { get; }
    }
}

