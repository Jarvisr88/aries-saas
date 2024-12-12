namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Docking.Platform;
    using System;
    using System.Runtime.CompilerServices;

    internal static class DockHintsExtensions
    {
        public static DockHint ToAutoHideDockHint(this DockVisualizerElement element)
        {
            switch (element)
            {
                case DockVisualizerElement.Right:
                    return DockHint.AutoHideRight;

                case DockVisualizerElement.Top:
                    return DockHint.AutoHideTop;

                case DockVisualizerElement.Bottom:
                    return DockHint.AutoHideBottom;
            }
            return DockHint.AutoHideLeft;
        }

        public static DockHint ToCenterDockHint(this DockVisualizerElement element)
        {
            switch (element)
            {
                case DockVisualizerElement.Right:
                    return DockHint.CenterRight;

                case DockVisualizerElement.Top:
                    return DockHint.CenterTop;

                case DockVisualizerElement.Bottom:
                    return DockHint.CenterBottom;

                case DockVisualizerElement.Center:
                    return DockHint.Center;
            }
            return DockHint.CenterLeft;
        }

        public static DockGuide ToDockGuide(this DockVisualizerElement element)
        {
            switch (element)
            {
                case DockVisualizerElement.Right:
                    return DockGuide.Right;

                case DockVisualizerElement.Top:
                    return DockGuide.Top;

                case DockVisualizerElement.Bottom:
                    return DockGuide.Bottom;

                case DockVisualizerElement.Center:
                    return DockGuide.Center;
            }
            return DockGuide.Left;
        }

        public static DockHint ToSideDockHint(this DockVisualizerElement element)
        {
            switch (element)
            {
                case DockVisualizerElement.Right:
                    return DockHint.SideRight;

                case DockVisualizerElement.Top:
                    return DockHint.SideTop;

                case DockVisualizerElement.Bottom:
                    return DockHint.SideBottom;
            }
            return DockHint.SideLeft;
        }
    }
}

