namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CheckEditChromeStateProvider : ButtonChrome2StateProvider
    {
        private DevExpress.Xpf.Core.Native.CheckEditChrome checkEditChrome;

        protected override string GetState();
        protected virtual void OnCheckEditChromeChanged(DevExpress.Xpf.Core.Native.CheckEditChrome oldValue);
        protected virtual void OnIsCheckedChanged(object sender, DependencyPropertyChangedEventArgs e);
        protected virtual void OnRequestUpdate(object sender, EventArgs e);
        protected override void Subscribe(FrameworkElement source);
        protected override void Unsubscribe(FrameworkElement source);
        private void UpdateChrome();

        public DevExpress.Xpf.Core.Native.CheckEditChrome CheckEditChrome { get; set; }

        protected DevExpress.Xpf.Editors.CheckEditBox CheckEditBox { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CheckEditChromeStateProvider.<>c <>9;
            public static Action<CheckEditChrome> <>9__12_0;

            static <>c();
            internal void <UpdateChrome>b__12_0(CheckEditChrome x);
        }
    }
}

