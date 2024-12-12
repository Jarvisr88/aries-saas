namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class ControlControllerBase : Controller
    {
        public ControlControllerBase(IControl control) : base(control)
        {
        }

        protected override void AttachToEvents()
        {
            base.AttachToEvents();
            this.Control.IsEnabledChanged += (sender, e) => this.OnIsEnabledChanged();
        }

        protected virtual void OnIsEnabledChanged()
        {
            this.UpdateState(false);
        }

        protected override void OnIsMouseEnteredChanged()
        {
            base.OnIsMouseEnteredChanged();
            this.UpdateState(true);
        }

        protected override void OnIsMouseLeftButtonDownChanged()
        {
            base.OnIsMouseLeftButtonDownChanged();
            this.UpdateState(true);
        }

        protected override void OnTouchDown(TouchEventArgs e)
        {
            base.OnTouchDown(e);
            this.UpdateState(true);
        }

        protected override void OnTouchLeave(TouchEventArgs e)
        {
            base.OnTouchLeave(e);
            this.UpdateState(true);
        }

        protected override void OnTouchUp(TouchEventArgs e)
        {
            base.OnTouchUp(e);
            this.UpdateState(true);
        }

        public virtual void UpdateState(bool useTransitions)
        {
            string stateName = !this.Control.IsEnabled ? "Disabled" : ((base.IsMouseLeftButtonDown || this.Control.AreAnyTouchesOver) ? "Pressed" : (!base.IsMouseEntered ? "Normal" : "MouseOver"));
            VisualStateManager.GoToState(this.Control, stateName, useTransitions);
        }

        public System.Windows.Controls.Control Control =>
            (System.Windows.Controls.Control) base.Control;
    }
}

