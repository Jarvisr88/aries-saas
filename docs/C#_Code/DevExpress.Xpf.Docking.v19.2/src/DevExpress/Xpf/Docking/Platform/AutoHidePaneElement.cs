namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class AutoHidePaneElement : DockLayoutContainer
    {
        private DevExpress.Xpf.Docking.VisualElements.AutoHidePane autoHidePane;

        public AutoHidePaneElement(UIElement uiElement, UIElement view) : base(LayoutItemType.AutoHideContainer, uiElement, view)
        {
        }

        protected override UIElement CheckView(UIElement view) => 
            view;

        protected override bool CheckVisualHitTestCore(Point pt) => 
            HitTestHelper.CheckVisualHitTest(this, pt, new Func<DependencyObject, DependencyObject, bool>(this.IsVisualChild));

        public override ILayoutElementBehavior GetBehavior() => 
            new AutoHidePaneElementBehavior(this);

        private bool IsVisualChild(DependencyObject root, DependencyObject child)
        {
            DependencyObject objA = child;
            while (objA != null)
            {
                if (ReferenceEquals(objA, root))
                {
                    return true;
                }
                objA = VisualTreeHelper.GetParent(objA);
                if (objA is AutoHideWindowHost.AutoHideWindowRoot)
                {
                    objA = ((AutoHideWindowHost.AutoHideWindowRoot) objA).Pane;
                }
            }
            return false;
        }

        public DevExpress.Xpf.Docking.VisualElements.AutoHidePane AutoHidePane
        {
            get
            {
                this.autoHidePane ??= (base.Element as DevExpress.Xpf.Docking.VisualElements.AutoHidePane);
                return this.autoHidePane;
            }
        }

        public Dock DockType =>
            (this.AutoHidePane != null) ? this.AutoHidePane.DockType : Dock.Left;

        public override bool HitTestingEnabled =>
            (this.AutoHidePane != null) && this.AutoHidePane.AutoHideTray.IsExpanded;
    }
}

