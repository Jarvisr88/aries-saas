namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    public static class DockExtensions
    {
        public static DockType ToDockType(this Dock type)
        {
            DockType none = DockType.None;
            switch (type)
            {
                case Dock.Left:
                    none = DockType.Left;
                    break;

                case Dock.Top:
                    none = DockType.Top;
                    break;

                case Dock.Right:
                    none = DockType.Right;
                    break;

                case Dock.Bottom:
                    none = DockType.Bottom;
                    break;

                default:
                    break;
            }
            return none;
        }

        public static Orientation ToOrthogonalOrientation(this Dock type) => 
            ((type == Dock.Left) || (type == Dock.Right)) ? Orientation.Vertical : Orientation.Horizontal;
    }
}

