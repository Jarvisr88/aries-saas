namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class EditingContextBase : IEditingContext
    {
        private readonly IModelItem root;
        internal readonly EditingContextTrace Trace;

        public EditingContextBase(object root, EditingContextTrace trace = null)
        {
            EditingContextTrace trace1 = trace;
            if (trace == null)
            {
                EditingContextTrace local1 = trace;
                trace1 = new EditingContextTrace(null, null, null, null, null, null, null, null, null);
            }
            this.Trace = trace1;
            Guard.ArgumentNotNull(root, "root");
            this.root = this.CreateModelItem(root, null);
            this.Services = new ServiceManagerBase();
            this.Services.Publish<IModelService>(new Func<IModelService>(this.CreateModelService));
        }

        public abstract IModelEditingScope CreateEditingScope(string description);
        public abstract IModelItem CreateModelItem(object obj, IModelItem parent);
        public abstract IModelItemCollection CreateModelItemCollection(IEnumerable computedValue, IModelItem parent);
        public abstract IModelItemDictionary CreateModelItemDictionary(IDictionary computedValue);
        public abstract IModelProperty CreateModelProperty(object obj, PropertyDescriptor property, IModelItem parent);
        public abstract IModelPropertyCollection CreateModelPropertyCollection(object element, IModelItem parent);
        protected abstract IModelService CreateModelService();
        public abstract IViewItem CreateViewItem(IModelItem modelItem);
        IModelItem IEditingContext.CreateItem(DXTypeIdentifier typeIdentifier)
        {
            throw new NotImplementedException();
        }

        IModelItem IEditingContext.CreateItem(Type type) => 
            this.CreateModelItem(Activator.CreateInstance(type), null);

        IModelItem IEditingContext.CreateItem(DXTypeIdentifier typeIdentifier, bool useDefaultInitializer)
        {
            throw new NotSupportedException();
        }

        IModelItem IEditingContext.CreateItem(Type type, bool useDefaultInitializer)
        {
            throw new NotSupportedException();
        }

        IModelItem IEditingContext.CreateStaticMemberItem(Type type, string memberName)
        {
            throw new NotImplementedException();
        }

        public IModelItem Root =>
            this.root;

        IServiceProvider IEditingContext.Services =>
            this.Services;

        public ServiceManagerBase Services { get; private set; }

        public abstract class ModelServiceBase : IModelService
        {
            private readonly EditingContextBase editingContext;

            public ModelServiceBase(EditingContextBase editingContext)
            {
                Guard.ArgumentNotNull(editingContext, "editingContext");
                this.editingContext = editingContext;
            }

            protected virtual IModelSubscribedEvent AddModelChangedHandler(EventHandler value) => 
                null;

            void IModelService.RaiseModelChanged()
            {
                throw new NotImplementedException();
            }

            IModelSubscribedEvent IModelService.SubscribeToModelChanged(EventHandler handler) => 
                this.AddModelChangedHandler(handler);

            void IModelService.UnsubscribeFromModelChanged(IModelSubscribedEvent e)
            {
                this.RemoveModelChangedHandler(e);
            }

            protected virtual void RemoveModelChangedHandler(IModelSubscribedEvent value)
            {
            }

            IModelItem IModelService.Root =>
                this.editingContext.root;
        }
    }
}

