namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IValueTypeResolver
    {
        Type GetAttributesType(Type metricTypeDefinition, Type valueType);
        Type GetValueBoxType(Type metricTypeDefinition, Type valueType);
        Type GetValueViewModelType(Type metricTypeDefinition, Type valueType);
    }
}

