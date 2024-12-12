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

    internal class SpinUpDownButtonInfoAutomationPeer : ButtonInfoBaseAutomationPeer, IInvokeProvider
    {
        private readonly bool isUp;

        public SpinUpDownButtonInfoAutomationPeer(ButtonInfoBase owner, bool isUp) : base(owner)
        {
            this.isUp = isUp;
        }

        protected override string GetClassNameCore() => 
            "SpinButtonInfo";

        public override object GetPattern(PatternInterface patternInterface) => 
            (patternInterface != PatternInterface.Invoke) ? null : this;

        void IInvokeProvider.Invoke()
        {
            if (!base.IsEnabled())
            {
                throw new ElementNotEnabledException();
            }
            base.Dispatcher.BeginInvoke(DispatcherPriority.Input, delegate (object <arg>) {
                (this.isUp ? new CommandSourceWrapper(this.Owner.SpinUpCommand, this.Owner.SpinUpCommandParameter, this.Owner.SpinUpCommandTarget) : new CommandSourceWrapper(this.Owner.SpinDownCommand, this.Owner.SpinDownCommandParameter, this.Owner.SpinDownCommandTarget)).TryExecute();
                RoutedEventArgs originalEventArgs = new RoutedEventArgs(ButtonBase.ClickEvent, this.Owner);
                if (this.isUp)
                {
                    this.Owner.OnRenderSpinUpButtonClick(null, new RenderEventArgs(null, originalEventArgs, RenderEvents.MouseDown));
                }
                else
                {
                    this.Owner.OnRenderSpinDownButtonClick(null, new RenderEventArgs(null, originalEventArgs, RenderEvents.MouseDown));
                }
                return null;
            }, null);
        }

        private SpinButtonInfo Owner =>
            base.Owner as SpinButtonInfo;
    }
}

