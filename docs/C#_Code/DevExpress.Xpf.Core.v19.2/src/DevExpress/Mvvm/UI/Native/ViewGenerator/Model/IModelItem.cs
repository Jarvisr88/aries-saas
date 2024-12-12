namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using System;
    using System.Collections.Generic;

    public interface IModelItem
    {
        IModelEditingScope BeginEdit(string description);
        IEnumerable<object> GetAttributes(Type attributeType);
        object GetCurrentValue();
        IModelSubscribedEvent SubscribeToPropertyChanged(EventHandler handler);
        void UnsubscribeFromPropertyChanged(IModelSubscribedEvent e);

        IModelPropertyCollection Properties { get; }

        IEditingContext Context { get; }

        IViewItem View { get; }

        Type ItemType { get; }

        IModelItem Root { get; }

        string Name { get; set; }

        IModelItem Parent { get; }
    }
}

