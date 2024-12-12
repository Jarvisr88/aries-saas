namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public static class DockExtensions
    {
        internal static Dock Invert(this Dock dock)
        {
            switch (dock)
            {
                case Dock.Left:
                    return Dock.Right;

                case Dock.Top:
                    return Dock.Bottom;

                case Dock.Right:
                    return Dock.Left;

                case Dock.Bottom:
                    return Dock.Top;
            }
            return dock;
        }

        internal static Thickness RotateThickness(this Dock dock, Thickness arg)
        {
            double top;
            double right;
            double bottom;
            double left;
            switch (dock)
            {
                case Dock.Left:
                    top = arg.Top;
                    right = arg.Right;
                    bottom = arg.Bottom;
                    left = arg.Left;
                    break;

                case Dock.Right:
                    top = arg.Bottom;
                    right = arg.Left;
                    bottom = arg.Top;
                    left = arg.Right;
                    break;

                case Dock.Bottom:
                    top = arg.Right;
                    right = arg.Bottom;
                    bottom = arg.Left;
                    left = arg.Top;
                    break;

                default:
                    top = arg.Left;
                    right = arg.Top;
                    bottom = arg.Right;
                    left = arg.Bottom;
                    break;
            }
            return new Thickness(top, right, bottom, left);
        }

        public static BarContainerType ToBarContainerType(this Dock dock)
        {
            switch (dock)
            {
                case Dock.Left:
                    return BarContainerType.Left;

                case Dock.Top:
                    return BarContainerType.Top;

                case Dock.Right:
                    return BarContainerType.Right;

                case Dock.Bottom:
                    return BarContainerType.Bottom;
            }
            return BarContainerType.None;
        }

        public static Cursor ToCursor(this Dock dock)
        {
            switch (dock)
            {
                case Dock.Left:
                case Dock.Right:
                    return Cursors.SizeWE;

                case Dock.Top:
                case Dock.Bottom:
                    return Cursors.SizeNS;
            }
            return Cursors.Arrow;
        }

        public static HorizontalAlignment ToHorizontalAlignment(Dock dock, bool IsInverted = false) => 
            (dock == Dock.Left) ? (IsInverted ? HorizontalAlignment.Right : HorizontalAlignment.Left) : ((dock == Dock.Right) ? (IsInverted ? HorizontalAlignment.Left : HorizontalAlignment.Right) : HorizontalAlignment.Stretch);

        internal static Thickness ToThickness(this Dock dock, Thickness arg)
        {
            Thickness thickness = new Thickness();
            switch (dock)
            {
                case Dock.Left:
                    thickness.Left = arg.Left;
                    break;

                case Dock.Top:
                    thickness.Top = arg.Top;
                    break;

                case Dock.Right:
                    thickness.Right = arg.Right;
                    break;

                case Dock.Bottom:
                    thickness.Bottom = arg.Bottom;
                    break;

                default:
                    break;
            }
            return thickness;
        }

        public static VerticalAlignment ToVerticalAlignment(Dock dock, bool IsInverted = false) => 
            (dock == Dock.Top) ? (IsInverted ? VerticalAlignment.Bottom : VerticalAlignment.Top) : ((dock == Dock.Bottom) ? (IsInverted ? VerticalAlignment.Top : VerticalAlignment.Bottom) : VerticalAlignment.Stretch);
    }
}

