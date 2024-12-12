namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public class TypedListBindingListAdapter : BindingListAdapter, ITypedList
    {
        internal TypedListBindingListAdapter(IList source, BindingListAdapterSourceListeningSettings sourceListeningSettings);
        public TypedListBindingListAdapter(IList source, ItemPropertyNotificationMode itemPropertyNotificationMode = 1, bool forceRaiseCollectionChange = false);
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors);
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors);

        private ITypedList TypedList { get; }
    }
}

