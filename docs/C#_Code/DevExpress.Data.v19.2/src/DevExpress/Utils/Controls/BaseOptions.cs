namespace DevExpress.Utils.Controls
{
    using DevExpress.Utils.Design;
    using DevExpress.WebUtils;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class BaseOptions : ViewStatePersisterCore, INotifyPropertyChanged
    {
        private int lockUpdate;
        protected internal BaseOptionChangedEventHandler ChangedCore;
        private static object boolFalse = false;
        private static object boolTrue = true;
        private PropertyChangedEventHandler onPropertyChanged;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                this.onPropertyChanged += value;
            }
            remove
            {
                this.onPropertyChanged -= value;
            }
        }

        public BaseOptions() : this(null, string.Empty)
        {
        }

        public BaseOptions(IViewBagOwner viewBagOwner, string objectPath) : base(viewBagOwner, objectPath)
        {
        }

        public virtual void Assign(BaseOptions options)
        {
        }

        public virtual void BeginUpdate()
        {
            this.lockUpdate++;
        }

        public virtual void CancelUpdate()
        {
            this.lockUpdate--;
        }

        public virtual void EndUpdate()
        {
            int num = this.lockUpdate - 1;
            this.lockUpdate = num;
            if (num == 0)
            {
                this.OnChanged(EmptyOptionChangedEventArgs.Instance);
            }
        }

        protected virtual void OnChanged(BaseOptionChangedEventArgs e)
        {
            this.RaisePropertyChanged(e.Name);
            if (!this.IsLockUpdate)
            {
                this.RaiseOnChanged(e);
            }
        }

        protected void OnChanged(string option, bool oldValue, bool newValue)
        {
            this.OnChanged(option, oldValue ? boolTrue : boolFalse, newValue ? boolTrue : boolFalse);
        }

        protected void OnChanged(string option, object oldValue, object newValue)
        {
            this.RaisePropertyChanged(option);
            if (!this.IsLockUpdate)
            {
                this.RaiseOnChanged(new BaseOptionChangedEventArgs(option, oldValue, newValue));
            }
        }

        protected virtual void RaiseOnChanged(BaseOptionChangedEventArgs e)
        {
            if (this.ChangedCore != null)
            {
                this.ChangedCore(this, e);
            }
        }

        protected internal virtual void RaisePropertyChanged(string propertyName)
        {
            if (this.onPropertyChanged != null)
            {
                this.onPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public virtual void Reset()
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this);
            this.BeginUpdate();
            try
            {
                foreach (PropertyDescriptor descriptor in properties)
                {
                    descriptor.ResetValue(this);
                }
            }
            finally
            {
                this.EndUpdate();
            }
        }

        protected internal bool ShouldSerialize() => 
            this.ShouldSerialize(null);

        protected internal virtual bool ShouldSerialize(IComponent owner) => 
            UniversalTypeConverter.ShouldSerializeObject(this, owner);

        public override string ToString() => 
            OptionsHelper.GetObjectText(this);

        protected virtual bool IsLockUpdate =>
            this.lockUpdate != 0;

        private sealed class EmptyOptionChangedEventArgs : BaseOptionChangedEventArgs
        {
            internal static readonly BaseOptionChangedEventArgs Instance = new BaseOptions.EmptyOptionChangedEventArgs();

            private EmptyOptionChangedEventArgs() : base(string.Empty, null, null)
            {
            }

            public sealed override object NewValue
            {
                get => 
                    null;
                set
                {
                }
            }
        }
    }
}

