namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public static class DockPanelLayoutHelper
    {
        public static Size ArrangeDockPanelLayout(FrameworkElement parent, Size arrangeSize, bool lastChildFill);
        public static Size ArrangeDockPanelLayout(FrameworkElement parent, Rect arrangeRect, bool lastChildFill, SkipLayout skipLayout, IsLastChild isLastChild);
        public static Size ArrangeDockPanelLayout(FrameworkElement parent, Size arrangeSize, bool lastChildFill, SkipLayout skipLayout, IsLastChild isLastChild);
        public static Size MeasureDockPanelLayout(FrameworkElement parent, Size constraint);
        public static Size MeasureDockPanelLayout(FrameworkElement parent, Size constraint, SkipLayout skipLayout);
    }
}

