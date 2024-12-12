namespace DevExpress.Xpf.Data
{
    using DevExpress.Data;
    using DevExpress.Xpf.Core;
    using System;

    internal class RowPropertyDescriptor : CustomPropertyDescriptor
    {
        private WeakReference infoReference;
        private Type propertyType;
        private string name;
        private bool readOnly;

        public RowPropertyDescriptor(DataColumnInfo info) : base(info.Name)
        {
            this.name = info.Name;
            this.propertyType = info.Type;
            this.readOnly = info.ReadOnly;
            this.infoReference = new WeakReference(info);
        }

        public override object GetValue(object component) => 
            ((RowTypeDescriptorBase) component).GetValue(this.Info);

        public override void SetValue(object component, object value)
        {
            ((RowTypeDescriptorBase) component).SetValue(this.Info, value);
        }

        private DataColumnInfo Info =>
            (DataColumnInfo) this.infoReference.Target;

        public override Type PropertyType =>
            this.propertyType;

        public override string Name =>
            this.name;

        public override bool IsReadOnly =>
            this.readOnly;
    }
}

