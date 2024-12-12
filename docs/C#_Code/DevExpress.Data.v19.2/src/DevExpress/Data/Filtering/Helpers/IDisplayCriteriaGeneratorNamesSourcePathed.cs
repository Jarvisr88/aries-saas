namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;

    public interface IDisplayCriteriaGeneratorNamesSourcePathed
    {
        string GetDisplayPropertyName(OperandProperty property, string fullPath);
        string GetValueScreenText(OperandProperty property, object value);
    }
}

