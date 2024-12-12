namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Utils;
    using System;
    using System.Windows;

    internal class IndependentDragEventArgs : IndependentRoutedEventArgs, IDragEventArgs, IRoutedEventArgs
    {
        private readonly DragEventArgs component;

        public IndependentDragEventArgs(DragEventArgs arg) : base(arg)
        {
            Guard.ArgumentNotNull(arg, "arg");
            this.component = arg;
        }

        Point IDragEventArgs.GetPosition(IInputElement relativeTo) => 
            this.component.GetPosition(relativeTo);

        IDataObject IDragEventArgs.Data =>
            this.component.Data;

        DragDropEffects IDragEventArgs.Effects
        {
            get => 
                this.component.Effects;
            set => 
                this.component.Effects = value;
        }

        DragDropKeyStates IDragEventArgs.KeyStates =>
            this.component.KeyStates;

        DragDropEffects IDragEventArgs.AllowedEffects =>
            this.component.AllowedEffects;
    }
}

