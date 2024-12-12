namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    public static class AutoHideTypeExtension
    {
        public static AutoHideType ToAutoHideType(this Dock dock)
        {
            switch (dock)
            {
                case Dock.Left:
                    return AutoHideType.Left;

                case Dock.Top:
                    return AutoHideType.Top;

                case Dock.Right:
                    return AutoHideType.Right;

                case Dock.Bottom:
                    return AutoHideType.Bottom;
            }
            return AutoHideType.Default;
        }

        public static Dock ToDock(this AutoHideType type)
        {
            switch (type)
            {
                case AutoHideType.Top:
                    return Dock.Top;

                case AutoHideType.Right:
                    return Dock.Right;

                case AutoHideType.Bottom:
                    return Dock.Bottom;
            }
            return Dock.Left;
        }
    }
}

