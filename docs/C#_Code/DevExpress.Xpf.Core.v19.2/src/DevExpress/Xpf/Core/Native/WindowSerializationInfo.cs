namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class WindowSerializationInfo
    {
        [XtraSerializableProperty]
        public Rect Bounds { get; set; }

        [XtraSerializableProperty]
        public System.Windows.WindowState WindowState { get; set; }

        [XtraSerializableProperty]
        public Rect ScreenBounds { get; set; }
    }
}

