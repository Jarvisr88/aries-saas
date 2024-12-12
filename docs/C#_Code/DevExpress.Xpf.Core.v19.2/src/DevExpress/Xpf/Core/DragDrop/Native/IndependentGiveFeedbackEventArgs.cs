namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Utils;
    using System;
    using System.Windows;

    internal class IndependentGiveFeedbackEventArgs : IndependentRoutedEventArgs, IGiveFeedbackEventArgs, IRoutedEventArgs
    {
        private readonly GiveFeedbackEventArgs component;

        public IndependentGiveFeedbackEventArgs(GiveFeedbackEventArgs arg) : base(arg)
        {
            Guard.ArgumentNotNull(arg, "arg");
            this.component = arg;
        }

        public bool UseDefaultCursors
        {
            get => 
                this.component.UseDefaultCursors;
            set => 
                this.component.UseDefaultCursors = value;
        }

        public DragDropEffects Effects =>
            this.component.Effects;
    }
}

