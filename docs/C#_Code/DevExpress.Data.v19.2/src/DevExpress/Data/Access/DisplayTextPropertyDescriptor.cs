namespace DevExpress.Data.Access
{
    using DevExpress.Data;
    using System;
    using System.ComponentModel;

    public class DisplayTextPropertyDescriptor : PropertyDescriptor
    {
        private DataColumnInfo info;
        private DataController controller;
        private IDataControllerSort sortClient;
        public const string DxFtsPropertyPrefix = "DxFts_";

        internal DisplayTextPropertyDescriptor(DataController controller, DataColumnInfo info);
        protected internal DisplayTextPropertyDescriptor(DataController controller, DataColumnInfo info, string name);
        public override bool CanResetValue(object component);
        public virtual string GetDisplayText(int listRowIndex, object value);
        public override object GetValue(object component);
        public object GetValue(int listRowIndex, object component);
        public override void ResetValue(object component);
        public override void SetValue(object component, object value);
        public override bool ShouldSerializeValue(object component);

        public DataController Controller { get; }

        public DataColumnInfo Info { get; }

        public override bool IsReadOnly { get; }

        public override string Category { get; }

        public override Type PropertyType { get; }

        public override Type ComponentType { get; }
    }
}

