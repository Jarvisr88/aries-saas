namespace DevExpress.Utils.Native
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;

    public class HistoryItem : DisposableObject, IHistoryItem, IDisposable
    {
        private readonly CommandBase command;
        private readonly object oldValue;
        private readonly object newValue;
        private readonly object parameter;

        public HistoryItem(CommandBase command, object oldValue, object newValue, object parameter)
        {
            this.command = command;
            this.oldValue = oldValue;
            this.newValue = newValue;
            this.parameter = parameter;
        }

        void IHistoryItem.Redo()
        {
            this.Command.Redo(this);
        }

        void IHistoryItem.Undo()
        {
            this.Command.Undo(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DisposeValue(this.oldValue, this.command.UndoManager.CanDisposeOldValue(this));
                this.DisposeValue(this.newValue, this.command.UndoManager.CanDisposeNewValue(this));
            }
            base.Dispose(disposing);
        }

        private void DisposeValue(object value, bool canDispose)
        {
            if (canDispose && !(value is string))
            {
                this.DisposeValueCore(value);
                IEnumerable enumerable = value as IEnumerable;
                if (enumerable != null)
                {
                    foreach (object obj2 in enumerable)
                    {
                        this.DisposeValueCore(obj2);
                    }
                }
            }
        }

        private void DisposeValueCore(object value)
        {
            if (value is IDisposable)
            {
                ((IDisposable) value).Dispose();
            }
        }

        public override string ToString() => 
            "HistoryItem (" + this.command.GetType().Name + ")";

        public CommandBase Command =>
            this.command;

        public object OldValue =>
            this.oldValue;

        public object NewValue =>
            this.newValue;

        public object Parameter =>
            this.parameter;

        public object TargetObject { get; set; }

        public object ObjectToSelect { get; set; }

        object IHistoryItem.ObjectToSelect =>
            this.ObjectToSelect;
    }
}

