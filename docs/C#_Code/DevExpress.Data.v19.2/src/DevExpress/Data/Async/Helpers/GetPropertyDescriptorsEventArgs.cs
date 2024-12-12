namespace DevExpress.Data.Async.Helpers
{
    using System;
    using System.ComponentModel;

    public class GetPropertyDescriptorsEventArgs : EventArgs
    {
        public readonly object TypeInfo;
        public PropertyDescriptorCollection PropertyDescriptors;

        public GetPropertyDescriptorsEventArgs(object typeInfo);
    }
}

