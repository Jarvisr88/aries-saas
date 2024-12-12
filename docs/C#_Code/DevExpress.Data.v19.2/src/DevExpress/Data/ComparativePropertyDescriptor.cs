namespace DevExpress.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class ComparativePropertyDescriptor : PropertyDescriptor
    {
        private AttributeCollection attributeCollectionCore;
        private HashSet<object> blackList;
        private bool isReadOnly;
        private Dictionary<object, PropertyDescriptor> propertyDescriptorDictionary;
        private PropertyDescriptor realPropertyDescriptor;
        internal HashSet<AttributeCollection> attributeFromAllDescriptors;
        private ComparativeSource owner;

        public ComparativePropertyDescriptor(ComparativeSource owner, PropertyDescriptor propertyDescriptor);
        public override bool CanResetValue(object component);
        private void FirstCheckComponent(object component);
        public override object GetValue(object component);
        public override void ResetValue(object component);
        public override void SetValue(object component, object value);
        public override bool ShouldSerializeValue(object component);
        internal void UpdateProperties(AttributeCollection attributeCollection, bool isReadOnly);

        public override AttributeCollection Attributes { get; }

        public override string Category { get; }

        public override Type ComponentType { get; }

        public override bool IsReadOnly { get; }

        public override Type PropertyType { get; }
    }
}

