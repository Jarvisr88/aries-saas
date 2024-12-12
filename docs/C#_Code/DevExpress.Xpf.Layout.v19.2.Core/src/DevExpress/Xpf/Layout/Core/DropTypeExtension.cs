namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    public static class DropTypeExtension
    {
        public static Orientation ToOrientation(this DropType type) => 
            ((type == DropType.Left) || (type == DropType.Right)) ? Orientation.Horizontal : Orientation.Vertical;
    }
}

