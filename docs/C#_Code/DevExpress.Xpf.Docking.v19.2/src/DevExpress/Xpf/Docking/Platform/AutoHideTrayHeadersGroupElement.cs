namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class AutoHideTrayHeadersGroupElement : DockLayoutContainer
    {
        private bool isTabContainerCore;
        private DevExpress.Xpf.Docking.VisualElements.AutoHideTrayHeadersGroup autoHideTrayHeadersGroup;
        private AutoHideTrayHeadersPanel headersPanelCore;
        private int thickness;

        public AutoHideTrayHeadersGroupElement(UIElement uiElement, UIElement view) : base(LayoutItemType.AutoHideGroup, uiElement, view)
        {
            this.thickness = 3;
            this.isTabContainerCore = (this.AutoHideTrayHeadersGroup != null) && (this.AutoHideTrayHeadersGroup.PartHeadersPanel != null);
        }

        protected override UIElement CheckView(UIElement view) => 
            view;

        protected override LayoutElementHitInfo CreateHitInfo(Point pt) => 
            new AutoHideTrayHeadersGroupElementHitInfo(pt, this);

        private Dock GetDock()
        {
            Dock dockType = this.AutoHideTrayHeadersGroup.Tray.DockType;
            switch (dockType)
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
            return dockType;
        }

        public override Rect GetHeadersPanelBounds()
        {
            bool isHorizontalHeaders = this.IsHorizontalHeaders;
            Dock dock = this.GetDock();
            bool flag2 = (dock == Dock.Right) || (dock == Dock.Bottom);
            Size size = base.Bounds.Size();
            double width = this.IsHorizontalHeaders ? size.Width : (size.Width - this.thickness);
            return new Rect(isHorizontalHeaders ? base.Bounds.Left : (flag2 ? (base.Bounds.Left + this.thickness) : base.Bounds.Left), isHorizontalHeaders ? (flag2 ? (base.Bounds.Top + this.thickness) : base.Bounds.Top) : base.Bounds.Top, width, this.IsHorizontalHeaders ? (size.Height - this.thickness) : size.Height);
        }

        public override Rect GetSelectedPageBounds()
        {
            bool isHorizontalHeaders = this.IsHorizontalHeaders;
            Dock dock = this.GetDock();
            bool flag2 = (dock == Dock.Right) || (dock == Dock.Bottom);
            Size renderSize = base.View.RenderSize;
            double width = isHorizontalHeaders ? renderSize.Width : ((double) this.thickness);
            double height = isHorizontalHeaders ? ((double) this.thickness) : renderSize.Height;
            return new Rect(isHorizontalHeaders ? 0.0 : (flag2 ? 0.0 : (renderSize.Width - width)), isHorizontalHeaders ? (flag2 ? 0.0 : (renderSize.Height - height)) : 0.0, width, height);
        }

        protected DevExpress.Xpf.Docking.VisualElements.AutoHideTrayHeadersGroup AutoHideTrayHeadersGroup
        {
            get
            {
                this.autoHideTrayHeadersGroup ??= LayoutItemsHelper.GetVisualChild<DevExpress.Xpf.Docking.VisualElements.AutoHideTrayHeadersGroup>(base.Element);
                return this.autoHideTrayHeadersGroup;
            }
        }

        protected AutoHideTrayHeadersPanel HeadersPanel
        {
            get
            {
                this.headersPanelCore ??= this.AutoHideTrayHeadersGroup.PartHeadersPanel;
                return this.headersPanelCore;
            }
        }

        public override bool IsTabContainer =>
            this.isTabContainerCore;

        public override bool IsHorizontalHeaders =>
            this.HeadersPanel.Orientation == Orientation.Horizontal;

        public override bool HasHeadersPanel =>
            this.IsTabContainer && (this.HeadersPanel != null);

        public override Dock TabHeaderLocation =>
            this.GetDock();
    }
}

