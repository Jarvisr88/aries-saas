namespace DevExpress.Export.Xl
{
    using System;

    public interface IXlFormulaParser
    {
        XlExpression Parse(string formula, IXlExpressionContext context);
    }
}

