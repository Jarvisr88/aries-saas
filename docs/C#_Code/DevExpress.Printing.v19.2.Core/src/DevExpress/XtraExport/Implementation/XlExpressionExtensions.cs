namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Runtime.CompilerServices;

    internal static class XlExpressionExtensions
    {
        public static XlExpression Offset(this XlExpression expression, int columnOffset, int rowOffset)
        {
            XlPtgOffsetWalker walker = new XlPtgOffsetWalker {
                ColumnOffset = columnOffset,
                RowOffset = rowOffset
            };
            return walker.ProcessExpression(expression);
        }
    }
}

