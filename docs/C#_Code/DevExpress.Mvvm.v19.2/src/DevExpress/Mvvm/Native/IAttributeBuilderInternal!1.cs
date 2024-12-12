namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Runtime.InteropServices;

    public interface IAttributeBuilderInternal<TBuilder>
    {
        TBuilder AddOrModifyAttribute<TAttribute>(Action<TAttribute> setAttributeValue = null) where TAttribute: Attribute, new();
        TBuilder AddOrReplaceAttribute<TAttribute>(TAttribute attribute) where TAttribute: Attribute;
    }
}

