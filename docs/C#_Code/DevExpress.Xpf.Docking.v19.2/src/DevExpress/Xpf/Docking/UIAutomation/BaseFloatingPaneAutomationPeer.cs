namespace DevExpress.Xpf.Docking.UIAutomation
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;

    public class BaseFloatingPaneAutomationPeer : FrameworkElementAutomationPeer, IWindowProvider, ITransformProvider
    {
        public BaseFloatingPaneAutomationPeer(FrameworkElement element) : base(element)
        {
        }

        public void Close()
        {
            this.Manager.DockController.Close(this.FloatGroup);
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.Window;

        protected override string GetAutomationIdCore() => 
            AutomationIdHelper.GetIdByLayoutItem(base.Owner);

        protected override Rect GetBoundingRectangleCore() => 
            new Rect(TransformProviderHelper.PointToScreen(this.Manager, this.FloatGroup.FloatLocation), this.FloatGroup.FloatSize);

        protected override string GetClassNameCore() => 
            this.FloatGroup.GetType().Name;

        protected override string GetNameCore()
        {
            DevExpress.Xpf.Docking.FloatGroup floatGroup = this.FloatGroup;
            return (((floatGroup == null) || (floatGroup.Items.Count == 0)) ? string.Empty : AutomationIdHelper.GetLayoutItemName(floatGroup.Items[0], "FloatPaneWindow"));
        }

        public override object GetPattern(PatternInterface patternInterface) => 
            (patternInterface != PatternInterface.Window) ? ((patternInterface != PatternInterface.Transform) ? base.GetPattern(patternInterface) : this) : this;

        public void Move(double x, double y)
        {
            this.FloatGroup.FloatLocation = TransformProviderHelper.PointFromScreen(this.Manager, new Point(x, y));
        }

        public void Resize(double width, double height)
        {
            this.FloatGroup.FloatSize = new Size(width, height);
        }

        public void Rotate(double degrees)
        {
            throw new InvalidOperationException();
        }

        protected override void SetFocusCore()
        {
            this.Manager.Activate(this.FloatGroup);
        }

        public void SetVisualState(WindowVisualState state)
        {
            switch (state)
            {
                case WindowVisualState.Normal:
                    this.Manager.MDIController.Restore(this.FloatGroup);
                    return;

                case WindowVisualState.Maximized:
                    this.Manager.MDIController.Maximize(this.FloatGroup);
                    return;

                case WindowVisualState.Minimized:
                    this.Manager.MDIController.Minimize(this.FloatGroup);
                    return;
            }
        }

        public bool WaitForInputIdle(int milliseconds)
        {
            throw new NotImplementedException();
        }

        protected internal DevExpress.Xpf.Docking.FloatGroup FloatGroup =>
            ((IFloatingPane) base.Owner).FloatGroup;

        protected internal DockLayoutManager Manager =>
            ((IFloatingPane) base.Owner).Manager;

        public WindowInteractionState InteractionState =>
            WindowInteractionState.Running;

        public bool IsModal =>
            false;

        public bool IsTopmost =>
            false;

        public bool Maximizable =>
            this.FloatGroup.IsMaximizable;

        public bool Minimizable =>
            false;

        public WindowVisualState VisualState =>
            WindowVisualState.Normal;

        public bool CanMove =>
            true;

        public bool CanResize =>
            true;

        public bool CanRotate =>
            false;
    }
}

