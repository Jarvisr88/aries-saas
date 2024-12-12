namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;

    public interface IDisplayCriteriaGeneratorNamesSource
    {
        string GetDisplayPropertyName(OperandProperty property);
        string GetValueScreenText(OperandProperty property, object value);
    }
}

