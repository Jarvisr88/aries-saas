namespace DevExpress.Data
{
    using System;
    using System.ComponentModel;

    public class UnboundSourceListChangedEventArgs : ListChangedEventArgs
    {
        private readonly bool _IsTriggeredByComponentApi;

        public UnboundSourceListChangedEventArgs(bool isTriggeredByComponentApi, ListChangedType listChangedType, int index, PropertyDescriptor propertyDescriptor);
        public UnboundSourceListChangedEventArgs(bool isTriggeredByComponentApi, ListChangedType listChangedType, int newIndex, int oldIndex);

        public bool IsTriggeredByComponentApi { get; }
    }
}

