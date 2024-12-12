namespace DevExpress.Data.Linq
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Linq.Helpers;
    using System;
    using System.Collections.Concurrent;
    using System.Linq.Expressions;

    public class CriteriaToEFExpressionConverter : ICriteriaToExpressionConverter, ICriteriaToExpressionConverterCustomizable
    {
        public static Type EntityFunctionsType;
        public static Type SqlFunctionsType;
        private readonly Type queryProviderType;
        private static readonly ConcurrentDictionary<Type, EntityQueryTypeInfo> queryTypeInfoDictionary;

        static CriteriaToEFExpressionConverter();
        public CriteriaToEFExpressionConverter();
        public CriteriaToEFExpressionConverter(Type queryProviderType);
        public Expression Convert(ParameterExpression thisExpression, CriteriaOperator op);
        public Expression Convert(ParameterExpression thisExpression, CriteriaOperator op, CriteriaToExpressionConverterEventsHelper eventsHelper);
        internal EntityQueryTypeInfo GetQueryTypeInfo();
        internal static EntityQueryTypeInfo GetQueryTypeInfo(Type queryProviderType);
    }
}

