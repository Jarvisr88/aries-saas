namespace DevExpress.Xpf.Data
{
    using DevExpress.Data;
    using DevExpress.Utils;
    using System;
    using System.ComponentModel;

    public abstract class RowTypeDescriptorBase : CustomTypeDescriptorBase, INotifyPropertyChanged
    {
        private WeakReference ownerReference;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        protected RowTypeDescriptorBase(WeakReference ownerReference)
        {
            this.ownerReference = ownerReference;
        }

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes) => 
            this.Owner.GetProperties();

        protected internal abstract object GetValue(DataColumnInfo info);
        internal abstract object GetValue(string fieldName);
        protected internal abstract void SetValue(DataColumnInfo info, object value);

        protected DataProviderBase Owner =>
            (DataProviderBase) this.ownerReference.Target;
    }
}

