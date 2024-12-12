namespace DevExpress.Xpf.Docking.UIAutomation
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;

    public class MDIDocumentAutomationPeer : BaseLayoutItemAutomationPeer, IWindowProvider, ITransformProvider
    {
        public MDIDocumentAutomationPeer(BaseLayoutItem frameworkElement) : base(frameworkElement)
        {
        }

        public void Close()
        {
            this.Manager.DockController.Close(this.Owner);
        }

        protected override Rect GetBoundingRectangleCore()
        {
            MDIPanel target = LayoutHelper.FindParentObject<MDIPanel>(this.Owner);
            if (target == null)
            {
                return base.GetBoundingRectangleCore();
            }
            Size mDISize = DocumentPanel.GetMDISize(this.Owner);
            return new Rect(TransformProviderHelper.PointToScreen(target, DocumentPanel.GetMDILocation(this.Owner)), MathHelper.IsEmpty(mDISize) ? this.Owner.RenderSize : mDISize);
        }

        public override object GetPattern(PatternInterface patternInterface) => 
            (patternInterface != PatternInterface.Window) ? ((patternInterface != PatternInterface.Transform) ? base.GetPattern(patternInterface) : this) : this;

        public void Move(double x, double y)
        {
            Point point = TransformProviderHelper.PointFromScreen(LayoutHelper.FindParentObject<MDIPanel>(this.Owner), new Point(x, y));
            DocumentPanel.SetMDILocation(this.Owner, point);
        }

        public void Resize(double width, double height)
        {
            DocumentPanel.SetMDISize(this.Owner, new Size(width, height));
        }

        public void Rotate(double degrees)
        {
            throw new NotImplementedException();
        }

        public void SetVisualState(WindowVisualState state)
        {
            switch (state)
            {
                case WindowVisualState.Normal:
                    this.Manager.MDIController.Restore(this.Owner);
                    return;

                case WindowVisualState.Maximized:
                    this.Manager.MDIController.Maximize(this.Owner);
                    return;

                case WindowVisualState.Minimized:
                    this.Manager.MDIController.Minimize(this.Owner);
                    return;
            }
        }

        public bool WaitForInputIdle(int milliseconds)
        {
            throw new NotImplementedException();
        }

        public DevExpress.Xpf.Docking.DocumentGroup DocumentGroup =>
            this.Owner.Parent as DevExpress.Xpf.Docking.DocumentGroup;

        protected internal DockLayoutManager Manager =>
            this.Owner.Manager;

        public DocumentPanel Owner =>
            base.Owner as DocumentPanel;

        public WindowInteractionState InteractionState
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsModal =>
            false;

        public bool IsTopmost =>
            false;

        public bool Maximizable =>
            true;

        public bool Minimizable =>
            true;

        public WindowVisualState VisualState =>
            !this.Owner.IsMaximized ? (!this.Owner.IsMinimized ? WindowVisualState.Normal : WindowVisualState.Minimized) : WindowVisualState.Maximized;

        public bool CanMove =>
            true;

        public bool CanResize =>
            true;

        public bool CanRotate =>
            false;
    }
}

