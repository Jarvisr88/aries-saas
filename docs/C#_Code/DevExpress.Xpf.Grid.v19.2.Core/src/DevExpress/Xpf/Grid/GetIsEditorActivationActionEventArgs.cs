namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class GetIsEditorActivationActionEventArgs : ActivationActionEventArgsBase
    {
        protected internal GetIsEditorActivationActionEventArgs(ActivationAction activationAction, RoutedEventArgs activationEventArgs, DependencyObject templateChild, bool isActivationAction, DataViewBase view, int rowHandle, ColumnBase column) : base(DataViewBase.GetIsEditorActivationActionEvent, activationAction, activationEventArgs, templateChild, view, rowHandle, column)
        {
            this.IsActivationAction = isActivationAction;
        }

        public bool IsActivationAction { get; set; }
    }
}

