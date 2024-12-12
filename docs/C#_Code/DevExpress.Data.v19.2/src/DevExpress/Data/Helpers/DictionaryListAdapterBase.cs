namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class DictionaryListAdapterBase : BindingListAdapterBase, ITypedList
    {
        private PropertyDescriptorCollection properties;

        public DictionaryListAdapterBase(IList source);
        public DictionaryListAdapterBase(IList source, IDictionary<string, Type> types);
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors);
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors);

        private class DictionaryPropertyDescriptor : PropertyDescriptor
        {
            private readonly Type propertyType;
            private readonly string name;
            private readonly object key;

            public DictionaryPropertyDescriptor(string name, Type propertyType, object key);
            public override bool CanResetValue(object component);
            public override object GetValue(object component);
            public override void ResetValue(object component);
            public override void SetValue(object component, object value);
            public override bool ShouldSerializeValue(object obj);

            public override Type ComponentType { get; }

            public override bool IsReadOnly { get; }

            public override Type PropertyType { get; }

            public override string DisplayName { get; }
        }
    }
}

