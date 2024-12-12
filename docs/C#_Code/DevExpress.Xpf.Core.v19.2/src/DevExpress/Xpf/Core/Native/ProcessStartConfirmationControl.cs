namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public class ProcessStartConfirmationControl : UserControl, IComponentConnector
    {
        private bool _contentLoaded;

        public ProcessStartConfirmationControl();
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        internal Delegate _CreateDelegate(Type delegateType, string handler);
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent();
        [EditorBrowsable(EditorBrowsableState.Never), GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        void IComponentConnector.Connect(int connectionId, object target);
    }
}

