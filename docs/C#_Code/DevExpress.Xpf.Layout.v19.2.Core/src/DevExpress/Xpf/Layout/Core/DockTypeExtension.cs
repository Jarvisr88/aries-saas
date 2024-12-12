namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    public static class DockTypeExtension
    {
        public static Dock ToDock(this DockType type)
        {
            switch (type)
            {
                case DockType.Right:
                    return Dock.Right;

                case DockType.Top:
                    return Dock.Top;

                case DockType.Bottom:
                    return Dock.Bottom;
            }
            return Dock.Left;
        }

        public static InsertType ToInsertType(this DockType type) => 
            ((type == DockType.Left) || (type == DockType.Top)) ? InsertType.Before : InsertType.After;

        public static Orientation ToOrientation(this DockType type) => 
            ((type == DockType.Left) || (type == DockType.Right)) ? Orientation.Horizontal : Orientation.Vertical;
    }
}

