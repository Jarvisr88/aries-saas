namespace DevExpress.Utils.Controls
{
    using System;

    public class BaseOptionChangedEventArgs : EventArgs
    {
        private string name;
        private object oldValue;
        private object newValue;

        public BaseOptionChangedEventArgs() : this("", null, null)
        {
        }

        public BaseOptionChangedEventArgs(string name, object oldValue, object newValue)
        {
            this.name = name;
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        public string Name =>
            this.name;

        public object OldValue =>
            this.oldValue;

        public virtual object NewValue
        {
            get => 
                this.newValue;
            set => 
                this.newValue = value;
        }
    }
}

