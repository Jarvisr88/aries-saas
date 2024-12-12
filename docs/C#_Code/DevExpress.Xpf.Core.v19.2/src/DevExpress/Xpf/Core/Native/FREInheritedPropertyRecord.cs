namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct FREInheritedPropertyRecord
    {
        public object DefaultValue { get; set; }
        public Action<FrameworkRenderElementContext, object, object> PropertyChangedCallback { get; set; }
        public Func<FrameworkRenderElementContext, object, FREInheritedPropertyValueSource, object> CoerceValueCallback { get; set; }
    }
}

