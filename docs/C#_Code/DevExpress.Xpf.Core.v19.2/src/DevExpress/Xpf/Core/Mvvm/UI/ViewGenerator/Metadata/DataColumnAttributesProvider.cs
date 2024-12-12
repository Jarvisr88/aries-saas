namespace DevExpress.Xpf.Core.Mvvm.UI.ViewGenerator.Metadata
{
    using DevExpress.Entity.Model;
    using DevExpress.Entity.Model.Metadata;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public class DataColumnAttributesProvider : IDataColumnAttributesProvider
    {
        DataColumnAttributes IDataColumnAttributesProvider.GetAtrributes(PropertyDescriptor property, Type ownerType) => 
            GetAttributes(property, ownerType, null);

        public static DataColumnAttributes GetAttributes(PropertyDescriptor property, Type ownerType = null, IAttributesProvider attributesProvider = null)
        {
            Type componentType = ownerType;
            if (ownerType == null)
            {
                Type local1 = ownerType;
                componentType = property.ComponentType;
            }
            return new DataColumnAttributes(GetAttributesCore(property, componentType, attributesProvider), () => property.Converter);
        }

        internal static AttributeCollection GetAttributesCore(PropertyDescriptor property, Type ownerType, IAttributesProvider attributesProvider = null) => 
            AttributesHelper.GetAttributes(property, ownerType, attributesProvider);
    }
}

