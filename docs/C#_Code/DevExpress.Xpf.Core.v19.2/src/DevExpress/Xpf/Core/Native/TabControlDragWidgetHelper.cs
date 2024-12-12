namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class TabControlDragWidgetHelper
    {
        public static readonly DependencyProperty UseWPFMethodProperty;

        static TabControlDragWidgetHelper();
        private Size CalcSize(FrameworkElement content, FrameworkElement tab, Orientation orientation);
        protected virtual FrameworkElement CreateDragWidget(DXTabItem tab);
        public virtual DragWidgetWindow CreateDragWidgetWindow(DXTabItem tab);
        public static bool GetUseWPFMethod(DXTabItem obj);
        protected virtual FrameworkElement ScreenshotFromContent(DXTabControl tabControl, double width, double maxHeight, bool useWPFMethod);
        protected static Rectangle ScreenshotFromScreen(FrameworkElement control, double width, double height);
        protected virtual FrameworkElement ScreenshotFromTab(DXTabItem tab);
        protected static Rectangle ScreenshotFromVisualBrush(VisualBrush brush, double width, double height);
        public static void SetUseWPFMethod(DXTabItem obj, bool value);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabControlDragWidgetHelper.<>c <>9;
            public static Func<FrameworkElement, Size> <>9__5_0;

            static <>c();
            internal Size <CalcSize>b__5_0(FrameworkElement x);
        }
    }
}

