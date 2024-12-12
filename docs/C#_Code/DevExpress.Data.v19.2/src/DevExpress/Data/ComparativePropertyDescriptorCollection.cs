namespace DevExpress.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class ComparativePropertyDescriptorCollection : PropertyDescriptorCollection
    {
        public static readonly ComparativePropertyDescriptorCollection Empty;

        static ComparativePropertyDescriptorCollection();
        public ComparativePropertyDescriptorCollection(PropertyDescriptor[] properties);
        public ComparativePropertyDescriptorCollection(PropertyDescriptor[] properties, bool readOnly);
        private static void AddCalculatedPropertyDescriptors(CalculatedColumnCollection calculatedColumnCollection, ComparativePropertyDescriptorCollection comparativePropertyDesriptorCollection);
        private static void AddComparativePropertyDescriptor(Dictionary<PropertyDescriptor, ComparativePropertyDescriptor> result, PropertyDescriptorCollection collection, ComparativeSource owner);
        internal static ComparativePropertyDescriptorCollection CreateFullPropertyDescriptorCollection(IBindingList DataSourceAdapter, ComparativeSource owner);
        internal static ComparativePropertyDescriptorCollection CreatePropertyDescriptorCollectionBase(IBindingList dataSourceAdapter, ComparativeSource owner);
        private static ComparativePropertyDescriptorCollection FilterByShowValues(IBindingList DataSourceAdapter, ComparativeSource owner, ComparativePropertyDescriptorCollection comparativePropertyDescriptorCollection);
    }
}

