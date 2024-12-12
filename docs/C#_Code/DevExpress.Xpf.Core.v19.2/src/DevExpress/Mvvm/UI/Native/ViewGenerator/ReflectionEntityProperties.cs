namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using DevExpress.Entity.Model;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Mvvm.UI.ViewGenerator.Metadata;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class ReflectionEntityProperties : IEntityProperties
    {
        private readonly IEdmPropertyInfo[] properties;

        public ReflectionEntityProperties(IEnumerable<PropertyDescriptor> properties, Type ownerType, bool includeReadonly = false, IMetadataLocator locator = null)
        {
            IAttributesProvider attributesProvider = locator.With<IMetadataLocator, IAttributesProvider>(x => MetadataHelper.GetAttributesProvider(ownerType, x));
            PropertyDescriptor[] descriptorArray = (from x in properties
                where includeReadonly || !x.IsReadOnly
                select x).ToArray<PropertyDescriptor>();
            this.properties = (from x in descriptorArray select new EdmPropertyInfo(x, DataColumnAttributesProvider.GetAttributes(x, ownerType, attributesProvider), this.IsNavigationProperty(x), false)).ToArray<EdmPropertyInfo>();
        }

        private static Type GetUnderlyingType(PropertyDescriptor property) => 
            Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

        private bool IsNavigationProperty(PropertyDescriptor property)
        {
            Type underlyingType = GetUnderlyingType(property);
            return ((Type.GetTypeCode(underlyingType) == TypeCode.Object) && !typeof(IEnumerable).IsAssignableFrom(underlyingType));
        }

        IEnumerable<IEdmPropertyInfo> IEntityProperties.AllProperties =>
            this.properties;
    }
}

