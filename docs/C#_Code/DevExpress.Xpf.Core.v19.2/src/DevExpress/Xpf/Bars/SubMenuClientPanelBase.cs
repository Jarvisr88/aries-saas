namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    public class SubMenuClientPanelBase : System.Windows.Controls.Panel
    {
        private SubMenuBarControl subMenuControl;
        private GlyphSidePanel panel;
        private PopupMenuBase menuBase;

        private List<RectEx> ArrangeColumn(double columnWidth, List<UIElement> column, int currentColumnIndex, bool isLastRow);
        protected override Size ArrangeOverride(Size finalSize);
        private PopupMenuBase GetPopupMenuBase(GlyphSidePanel panel = null, SubMenuBarControl subMenu = null);
        protected override Size MeasureOverride(Size availableSize);
        private T TryGetLinkControlAtIndex<T>(int index) where T: class, IBarItemLinkControl;

        protected internal SubMenuBarControl SubMenuControl { get; }

        protected internal GlyphSidePanel Panel { get; }

        protected internal PopupMenuBase MenuBase { get; }

        internal int DesiredColumnsCount { get; set; }

        internal double DesiredColumnWidth { get; set; }

        internal double DesiredColumnHeight { get; set; }

        internal double ColumnSplitterWidth { get; set; }

        internal Size SplitterDesiredSize { get; set; }

        internal double ResultWidth { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SubMenuClientPanelBase.<>c <>9;
            public static Func<BarItemLinkMenuHeaderControl, bool> <>9__34_0;

            static <>c();
            internal bool <MeasureOverride>b__34_0(BarItemLinkMenuHeaderControl x);
        }
    }
}

