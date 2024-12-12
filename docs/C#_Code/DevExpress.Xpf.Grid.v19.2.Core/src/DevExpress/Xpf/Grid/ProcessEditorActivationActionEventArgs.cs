namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ProcessEditorActivationActionEventArgs : ActivationActionEventArgsBase
    {
        protected internal ProcessEditorActivationActionEventArgs(ActivationAction activationAction, RoutedEventArgs activationEventArgs, DependencyObject templateChild, bool raiseEventAgain, DataViewBase view, int rowHandle, ColumnBase column) : base(DataViewBase.ProcessEditorActivationActionEvent, activationAction, activationEventArgs, templateChild, view, rowHandle, column)
        {
            this.RaiseEventAgain = raiseEventAgain;
        }

        public bool RaiseEventAgain { get; set; }
    }
}

