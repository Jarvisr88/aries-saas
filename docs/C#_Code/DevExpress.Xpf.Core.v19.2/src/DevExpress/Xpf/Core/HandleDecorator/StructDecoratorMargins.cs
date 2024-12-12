namespace DevExpress.Xpf.Core.HandleDecorator
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;

    [StructLayout(LayoutKind.Sequential)]
    public struct StructDecoratorMargins
    {
        public Thickness LeftMargins;
        public Thickness RightMargins;
        public Thickness TopMargins;
        public Thickness BottomMargins;
    }
}

