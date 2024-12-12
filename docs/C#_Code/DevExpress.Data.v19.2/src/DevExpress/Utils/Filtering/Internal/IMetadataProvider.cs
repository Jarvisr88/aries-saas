namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Utils;
    using System;

    public interface IMetadataProvider
    {
        AnnotationAttributes GetAnnotationAttributes(string name);
        bool GetApplyFormatInEditMode(string name);
        IMetricAttributes GetAttributes(string name);
        Type GetAttributesTypeDefinition(string name);
        string GetCaption(string name);
        string GetDataFormatString(string name);
        DataType? GetDataType(string name);
        string GetDescription(string name);
        Type GetEnumDataType(string name);
        FilterAttributes GetFilterAttributes(string name);
        bool GetIsVisible(string name);
        string GetLayout(string name);
        string GetNullDisplayText(string name);
        int GetOrder(string name);
        string GetShortName(string name);
        Type GetType(string name);
    }
}

