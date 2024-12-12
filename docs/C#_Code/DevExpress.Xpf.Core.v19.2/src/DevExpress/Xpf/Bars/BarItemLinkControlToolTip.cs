namespace DevExpress.Xpf.Bars
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public class BarItemLinkControlToolTip : ToolTip
    {
        public static readonly DependencyProperty UseToolTipPlacementTargetProperty;
        public static readonly DependencyProperty HorizontalPlacementProperty;
        public static readonly DependencyProperty VerticalPlacementProperty;

        static BarItemLinkControlToolTip();
        public BarItemLinkControlToolTip();
        internal static double CalcHorizontalOffset(BarItemLinkControlToolTipHorizontalPlacement horizontalPlacement, Size toolTipSize, Rect targetBounds, Thickness mouseCursorPos, FlowDirection flowDirection = 0);
        protected virtual CustomPopupPlacement[] CalculatePlacement(Size toolTipSize, Size targetSize, Point offset);
        public static CustomPopupPlacement[] CalculatePlacement(UIElement control, Size toolTipSize, Size targetSize, Point offset, UIElement PlacementTarget, bool UseToolTipPlacementTarget, Func<IToolTipPlacementTarget> GetPropertiesSource);
        protected static CustomPopupPlacement[] CalculatePlacement(UIElement control, UIElement PlacementTarget, IToolTipPlacementTarget propertiesSource, Size internalTargetSize, Size toolTipSize, Point offset, FlowDirection flowDirection);
        internal static double CalcVerticalOffset(BarItemLinkControlToolTipVerticalPlacement verticalPlacement, Size toolTipSize, Rect targetBounds, Thickness mouseCursorPos);
        public static BarItemLinkControlToolTipHorizontalPlacement GetHorizontalPlacement(DependencyObject obj);
        private static Thickness GetMouseCursorPos(UIElement control, object targetElement, FlowDirection flowDirection);
        internal static Size GetMouseCursorSize(ToolTip instance);
        protected virtual IToolTipPlacementTarget GetPropertiesSource();
        public static BarItemLinkControlToolTipVerticalPlacement GetVerticalPlacement(DependencyObject obj);
        protected override AutomationPeer OnCreateAutomationPeer();
        public static void SetHorizontalPlacement(DependencyObject obj, BarItemLinkControlToolTipHorizontalPlacement value);
        public static void SetVerticalPlacement(DependencyObject obj, BarItemLinkControlToolTipVerticalPlacement value);

        public bool UseToolTipPlacementTarget { get; set; }

        public PlacementMode Placement { get; protected set; }
    }
}

