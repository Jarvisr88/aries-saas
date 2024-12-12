namespace DevExpress.Export.Xl
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Globalization;

    internal interface IFunctionConverter
    {
        bool ConvertFunction(CriteriaOperatorCollection operands, IClientCriteriaVisitor<CriteriaPriorityClass> visitor, XlExpression expression);

        CultureInfo Culture { get; set; }
    }
}

