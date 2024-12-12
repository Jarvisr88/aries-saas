namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;

    public abstract class RaisePosponedEventAction<T> : CellEditorAction where T: RoutedEventArgs
    {
        private T posponedEventArgs;
        private Func<bool> condition;

        protected RaisePosponedEventAction(InplaceEditorBase editor, T e, Func<bool> condition = null) : base(editor)
        {
            this.posponedEventArgs = e;
            this.condition = condition;
        }

        protected abstract T CloneEventArgs(T posponedEventArgs);
        public sealed override void Execute()
        {
            if (base.editor.IsInTree && ((this.condition == null) || this.condition()))
            {
                ReraiseEventHelper.ReraiseEvent<T>(this.posponedEventArgs, this.GetElement(this.posponedEventArgs), this.TunnelingEvent, this.BubblingEvent, new Func<T, T>(this.CloneEventArgs));
            }
        }

        protected abstract UIElement GetElement(T posponedEventArgs);

        protected abstract RoutedEvent BubblingEvent { get; }

        protected abstract RoutedEvent TunnelingEvent { get; }
    }
}

