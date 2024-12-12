namespace DevExpress.Data.Browsing
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class DataBrowserHelper : DataBrowserHelperBase
    {
        private static readonly Attribute[] browsableAttributes;
        private readonly bool suppressListFilling;

        static DataBrowserHelper();
        public DataBrowserHelper();
        public DataBrowserHelper(bool suppressListFilling);
        private PropertyDescriptor[] ApplyNameToArray(object component, string name, PropertyDescriptor[] listAccessors);
        private List<PropertyDescriptor> GetInterfaceProperties(Type componentType);
        protected override object GetList(object list);
        public override PropertyDescriptorCollection GetListItemProperties(object list, PropertyDescriptor[] listAccessors);
        protected override PropertyDescriptorCollection GetProperties(object component);
        protected override PropertyDescriptorCollection GetProperties(Type componentType);
        protected override bool IsCustomType(Type type);
    }
}

