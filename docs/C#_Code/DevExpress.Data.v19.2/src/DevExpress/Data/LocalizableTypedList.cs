namespace DevExpress.Data
{
    using System;
    using System.ComponentModel;

    public abstract class LocalizableTypedList : Component, IDisplayNameProvider, ITypedList
    {
        private ITypedList typedList;

        public LocalizableTypedList(ITypedList typedList);
        string IDisplayNameProvider.GetDataSourceDisplayName();
        string IDisplayNameProvider.GetFieldDisplayName(string[] fieldAccessors);
        protected abstract string GetDataSourceDisplayName();
        protected abstract string GetFieldDisplayName(string[] fieldAccessors);
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors);
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors);
        private static string[] ToFieldAccessors(PropertyDescriptor[] listAccessors);
    }
}

