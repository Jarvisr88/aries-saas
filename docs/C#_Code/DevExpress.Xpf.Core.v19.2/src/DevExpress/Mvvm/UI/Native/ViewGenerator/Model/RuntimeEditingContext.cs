namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public class RuntimeEditingContext : EditingContextBase, IEditingContext
    {
        public RuntimeEditingContext(object root, EditingContextTrace trace = null) : base(root, trace)
        {
        }

        public override IModelEditingScope CreateEditingScope(string description) => 
            new RuntimeModelEditingScope(this, description);

        public override IModelItem CreateModelItem(object obj, IModelItem parent) => 
            new RuntimeModelItem(this, obj, parent);

        public override IModelItemCollection CreateModelItemCollection(IEnumerable computedValue, IModelItem parent) => 
            new RuntimeModelItemCollection(this, computedValue, parent);

        public override IModelItemDictionary CreateModelItemDictionary(IDictionary computedValue) => 
            new ModelItemDictionaryBase(this, computedValue);

        public override IModelProperty CreateModelProperty(object obj, PropertyDescriptor property, IModelItem parent) => 
            new RuntimeModelProperty(this, obj, property, parent);

        public override IModelPropertyCollection CreateModelPropertyCollection(object element, IModelItem parent) => 
            new RuntimeModelPropertyCollection(this, element, parent);

        protected override IModelService CreateModelService() => 
            new RuntimeModelService(this);

        public override IViewItem CreateViewItem(IModelItem modelItem) => 
            null;

        private class RuntimeModelService : EditingContextBase.ModelServiceBase
        {
            public RuntimeModelService(EditingContextBase editingContext) : base(editingContext)
            {
            }
        }
    }
}

