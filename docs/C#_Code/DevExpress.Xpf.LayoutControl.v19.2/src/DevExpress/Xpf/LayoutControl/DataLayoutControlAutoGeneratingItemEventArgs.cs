namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class DataLayoutControlAutoGeneratingItemEventArgs : CancelEventArgs
    {
        public DataLayoutControlAutoGeneratingItemEventArgs(string propertyName, Type propertyType, DataLayoutItem item)
        {
            this.Item = item;
            this.PropertyName = propertyName;
            this.PropertyType = propertyType;
        }

        public DataLayoutItem Item { get; set; }

        public string PropertyName { get; private set; }

        public Type PropertyType { get; private set; }
    }
}

