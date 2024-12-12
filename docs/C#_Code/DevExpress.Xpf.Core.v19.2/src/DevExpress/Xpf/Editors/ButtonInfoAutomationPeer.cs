namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;
    using System.Windows.Controls.Primitives;
    using System.Windows.Threading;

    internal class ButtonInfoAutomationPeer : ButtonInfoBaseAutomationPeer, IInvokeProvider, IToggleProvider
    {
        public ButtonInfoAutomationPeer(ButtonInfo owner) : base(owner)
        {
        }

        protected override Rect GetBoundingRectangleCore() => 
            this.Owner.CalcRenderBounds();

        protected override string GetClassNameCore() => 
            "ButtonInfo";

        public override object GetPattern(PatternInterface patternInterface) => 
            (patternInterface != PatternInterface.Invoke) ? ((patternInterface != PatternInterface.Toggle) ? null : this) : this;

        private System.Windows.Automation.ToggleState GetToggleState(bool? value)
        {
            bool? nullable = value;
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag) ? (nullable != null) : false)
            {
                return System.Windows.Automation.ToggleState.On;
            }
            nullable = value;
            flag = false;
            return (((nullable.GetValueOrDefault() == flag) ? ((System.Windows.Automation.ToggleState) (nullable != null)) : ((System.Windows.Automation.ToggleState) false)) ? System.Windows.Automation.ToggleState.Off : System.Windows.Automation.ToggleState.Indeterminate);
        }

        protected override bool IsOffscreenCore() => 
            false;

        void IInvokeProvider.Invoke()
        {
            if (!base.IsEnabled())
            {
                throw new ElementNotEnabledException();
            }
            base.Dispatcher.BeginInvoke(DispatcherPriority.Input, delegate (object <arg>) {
                this.Owner.TryExecute();
                RoutedEventArgs originalEventArgs = new RoutedEventArgs(ButtonBase.ClickEvent, this.Owner);
                this.Owner.OnRenderButtonClick(null, new RenderEventArgs(null, originalEventArgs, RenderEvents.MouseDown));
                return null;
            }, null);
        }

        public void Toggle()
        {
            bool? nullable1;
            if (!base.IsEnabled())
            {
                throw new ElementNotEnabledException();
            }
            bool? isChecked = this.Owner.IsChecked;
            if (isChecked != null)
            {
                nullable1 = new bool?(!isChecked.GetValueOrDefault());
            }
            else
            {
                nullable1 = null;
            }
            this.Owner.IsChecked = nullable1;
        }

        private ButtonInfo Owner =>
            base.Owner as ButtonInfo;

        public System.Windows.Automation.ToggleState ToggleState =>
            (this.Owner.ButtonKind == ButtonKind.Toggle) ? this.GetToggleState(this.Owner.IsChecked) : System.Windows.Automation.ToggleState.Off;
    }
}

