namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Utils;
    using System;
    using System.Windows;

    internal class IndependentQueryContinueDragEventArgs : IndependentRoutedEventArgs, IQueryContinueDragEventArgs, IRoutedEventArgs
    {
        private readonly QueryContinueDragEventArgs component;

        public IndependentQueryContinueDragEventArgs(QueryContinueDragEventArgs arg) : base(arg)
        {
            Guard.ArgumentNotNull(arg, "arg");
            this.component = arg;
        }

        public bool EscapePressed =>
            this.component.EscapePressed;

        public DragDropKeyStates KeyStates =>
            this.component.KeyStates;

        public DragAction Action
        {
            get => 
                this.component.Action;
            set => 
                this.component.Action = value;
        }
    }
}

