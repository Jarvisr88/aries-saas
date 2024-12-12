namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class TypedListBindingListAdapterBase : BindingListAdapterBase, ITypedList
    {
        public TypedListBindingListAdapterBase(IList source);
        public TypedListBindingListAdapterBase(IList source, ItemPropertyNotificationMode itemPropertyNotificationMode);
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors);
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors);

        private ITypedList TypedList { get; }
    }
}

