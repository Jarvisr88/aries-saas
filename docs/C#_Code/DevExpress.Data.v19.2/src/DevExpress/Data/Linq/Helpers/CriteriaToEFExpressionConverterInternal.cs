namespace DevExpress.Data.Linq.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Linq;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class CriteriaToEFExpressionConverterInternal : CriteriaToExpressionConverterInternal
    {
        private static readonly MethodInfo toStringMi;
        private static readonly MethodInfo trimMi;
        private MethodInfo doubleConvertMi;
        private MethodInfo decimalConvertMi;
        private readonly EntityQueryTypeInfo queryTypeInfo;

        static CriteriaToEFExpressionConverterInternal();
        public CriteriaToEFExpressionConverterInternal(ICriteriaToExpressionConverter owner, ParameterExpression thisExpression);
        public CriteriaToEFExpressionConverterInternal(ICriteriaToExpressionConverter owner, ParameterExpression thisExpression, Type queryProviderType);
        protected override Expression ConvertToType(Expression instanceExpr, Type type);
        public Expression FnGetDate(FunctionOperator theOperator);
        public Expression FnToStr(FunctionOperator theOperator);
        private Expression VisitFunctionalOperatorEFCore(FunctionOperator theOperator);
        private Expression VisitFunctionalOperatorFullEF(FunctionOperator theOperator);
        protected override Expression VisitInternal(FunctionOperator theOperator);
        protected override Expression VisitInternal(UnaryOperator theOperator);
    }
}

