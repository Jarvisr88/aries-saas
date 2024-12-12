namespace DevExpress.Export.Xl
{
    using DevExpress.XtraExport.Implementation;
    using System;

    public static class XlOper
    {
        public static IXlFormulaParameter Add(IXlFormulaParameter left, IXlFormulaParameter right) => 
            new XlAddOperator(left, right);

        public static IXlFormulaParameter Divide(IXlFormulaParameter left, IXlFormulaParameter right) => 
            new XlDivideOperator(left, right);

        public static IXlFormulaParameter Equal(IXlFormulaParameter left, IXlFormulaParameter right) => 
            new XlEqualOperator(left, right);

        public static IXlFormulaParameter Greater(IXlFormulaParameter left, IXlFormulaParameter right) => 
            new XlGreaterOperator(left, right);

        public static IXlFormulaParameter GreaterEqual(IXlFormulaParameter left, IXlFormulaParameter right) => 
            new XlGreaterEqualOperator(left, right);

        public static IXlFormulaParameter Less(IXlFormulaParameter left, IXlFormulaParameter right) => 
            new XlLessOperator(left, right);

        public static IXlFormulaParameter LessEqual(IXlFormulaParameter left, IXlFormulaParameter right) => 
            new XlLessEqualOperator(left, right);

        public static IXlFormulaParameter Multiply(IXlFormulaParameter left, IXlFormulaParameter right) => 
            new XlMultiplyOperator(left, right);

        public static IXlFormulaParameter NotEqual(IXlFormulaParameter left, IXlFormulaParameter right) => 
            new XlNotEqualOperator(left, right);

        public static IXlFormulaParameter Subtract(IXlFormulaParameter left, IXlFormulaParameter right) => 
            new XlSubtractOperator(left, right);
    }
}

