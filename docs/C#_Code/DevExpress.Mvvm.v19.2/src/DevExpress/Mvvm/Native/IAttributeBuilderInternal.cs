namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Runtime.InteropServices;

    public interface IAttributeBuilderInternal
    {
        void AddOrModifyAttribute<TAttribute>(Action<TAttribute> setAttributeValue = null) where TAttribute: Attribute, new();
        void AddOrReplaceAttribute<TAttribute>(TAttribute attribute) where TAttribute: Attribute;
    }
}

