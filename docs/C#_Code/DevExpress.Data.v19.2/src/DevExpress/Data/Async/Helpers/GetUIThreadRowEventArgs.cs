namespace DevExpress.Data.Async.Helpers
{
    using System;
    using System.ComponentModel;

    public class GetUIThreadRowEventArgs : EventArgs
    {
        public readonly object TypeInfo;
        public readonly PropertyDescriptorCollection PropertyDescriptors;
        public readonly object RowInfo;
        public object UIThreadRow;

        public GetUIThreadRowEventArgs(object typeInfo, PropertyDescriptorCollection propertyDescriptors, object rowInfo);
    }
}

