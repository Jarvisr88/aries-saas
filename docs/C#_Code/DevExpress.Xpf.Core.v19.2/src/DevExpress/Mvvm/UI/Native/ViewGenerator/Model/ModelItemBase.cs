namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using DevExpress.Data.Utils;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class ModelItemBase : IModelItem
    {
        public readonly object element;
        private readonly EditingContextBase context;
        private readonly IModelItem parent;
        private readonly IViewItem view;
        private readonly IModelPropertyCollection properties;
        private readonly PropertyChangedWeakEventHandler<ModelItemBase> propertyChangedHandler;
        private PropertyChangedEventHandler propertyChanged;

        public ModelItemBase(EditingContextBase context, object element, IModelItem parent)
        {
            Guard.ArgumentNotNull(context, "context");
            this.parent = parent;
            this.context = context;
            this.element = element;
            this.properties = context.CreateModelPropertyCollection(element, this);
            this.view = context.CreateViewItem(this);
            Action<ModelItemBase, object, PropertyChangedEventArgs> onEventAction = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Action<ModelItemBase, object, PropertyChangedEventArgs> local1 = <>c.<>9__8_0;
                onEventAction = <>c.<>9__8_0 = (item, sender, e) => item.OnPropertyChanged(sender, e);
            }
            this.propertyChangedHandler = new PropertyChangedWeakEventHandler<ModelItemBase>(this, onEventAction);
            if (element is INotifyPropertyChanged)
            {
                ((INotifyPropertyChanged) element).PropertyChanged += this.propertyChangedHandler.Handler;
            }
        }

        IModelEditingScope IModelItem.BeginEdit(string description) => 
            this.context.CreateEditingScope(description);

        object IModelItem.GetCurrentValue() => 
            this.element;

        IModelSubscribedEvent IModelItem.SubscribeToPropertyChanged(EventHandler handler) => 
            ModelSubscribedEvent<PropertyChangedEventHandler>.Subscribe(delegate (object s, PropertyChangedEventArgs e) {
                handler(s, e);
            }, delegate (PropertyChangedEventHandler h) {
                this.propertyChanged += h;
            });

        void IModelItem.UnsubscribeFromPropertyChanged(IModelSubscribedEvent e)
        {
            ModelSubscribedEvent<PropertyChangedEventHandler>.Unsubscribe(e, h => this.propertyChanged -= h);
        }

        public IEnumerable<object> GetAttributes(Type attributeType) => 
            this.element.GetType().GetCustomAttributes(attributeType, true);

        public static object GetUnderlyingObject(object value)
        {
            if (value is IModelItem)
            {
                value = ((IModelItem) value).View.PlatformObject;
            }
            return value;
        }

        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.propertyChanged != null)
            {
                this.propertyChanged(sender, e);
            }
        }

        public IModelPropertyCollection Properties =>
            this.properties;

        IEditingContext IModelItem.Context =>
            this.context;

        IViewItem IModelItem.View =>
            this.view;

        Type IModelItem.ItemType =>
            this.element?.GetType();

        IModelItem IModelItem.Root =>
            this.context.Services.GetService<IModelService>().Root;

        public virtual string Name
        {
            get => 
                null;
            set
            {
            }
        }

        IModelItem IModelItem.Parent =>
            this.parent;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ModelItemBase.<>c <>9 = new ModelItemBase.<>c();
            public static Action<ModelItemBase, object, PropertyChangedEventArgs> <>9__8_0;

            internal void <.ctor>b__8_0(ModelItemBase item, object sender, PropertyChangedEventArgs e)
            {
                item.OnPropertyChanged(sender, e);
            }
        }
    }
}

