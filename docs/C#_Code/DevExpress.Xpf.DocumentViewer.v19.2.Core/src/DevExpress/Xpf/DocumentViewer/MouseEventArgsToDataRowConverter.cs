namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class MouseEventArgsToDataRowConverter : EventArgsToDataRowConverter
    {
        [CompilerGenerated, DebuggerHidden]
        private object <>n__0(object sender, EventArgs args) => 
            base.Convert(sender, args);

        protected override object Convert(object sender, EventArgs args) => 
            (args as MouseButtonEventArgs).If<MouseButtonEventArgs>(x => (x.ChangedButton == this.ChangedButton)).With<MouseButtonEventArgs, object>(x => this.<>n__0(sender, x));

        public MouseButton ChangedButton { get; set; }
    }
}

