namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    public class EditableDataObject : DataObjectBase
    {
        private object value;

        protected virtual object CoerceValue(object newValue) => 
            newValue;

        protected virtual void OnContentChanged()
        {
            this.RaiseContentChanged();
        }

        protected static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EditableDataObject) d).OnContentChanged();
        }

        protected virtual void OnValueChanged(object oldValue)
        {
        }

        public object Value
        {
            get => 
                this.value;
            set
            {
                if (!Equals(this.value, value))
                {
                    object oldValue = this.value;
                    this.value = this.CoerceValue(value);
                    base.RaisePropertyChanged("Value");
                    this.OnValueChanged(oldValue);
                    this.OnContentChanged();
                }
            }
        }
    }
}

