namespace DevExpress.Data.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    public static class MultiselectRoundedDateTimeFilterHelper
    {
        public static CriteriaOperator DatesToCriteria(string columnName, IEnumerable<DateTime> dates);
        public static IEnumerable<DateTime> GetCheckedDates(CriteriaOperator criteria, string dateTimePropertyName, IEnumerable<DateTime> dates);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MultiselectRoundedDateTimeFilterHelper.<>c <>9;
            public static Func<DateTime, DateTime> <>9__0_0;
            public static Func<IGrouping<DateTime, DateTime>, DateTime> <>9__0_1;
            public static Func<DateTime, DateTime> <>9__0_2;

            static <>c();
            internal DateTime <DatesToCriteria>b__0_0(DateTime d);
            internal DateTime <DatesToCriteria>b__0_1(IGrouping<DateTime, DateTime> g);
            internal DateTime <DatesToCriteria>b__0_2(DateTime d);
        }

        private class DTDescriptor : CriteriaCompilerDescriptor
        {
            protected readonly string PropertyName;

            public DTDescriptor(string propertyName);
            public override CriteriaCompilerRefResult DiveIntoCollectionProperty(Expression baseExpression, string collectionPropertyPath);
            public override LambdaExpression MakeFreeJoinLambda(string joinTypeName, CriteriaOperator condition, OperandParameter[] conditionParameters, Aggregate aggregateType, CriteriaOperator aggregateExpression, OperandParameter[] aggregateExpresssionParameters, Type[] invokeTypes);
            public override LambdaExpression MakeFreeJoinLambda(string joinTypeName, CriteriaOperator condition, OperandParameter[] conditionParameters, string customAggregateName, IEnumerable<CriteriaOperator> aggregateExpressions, OperandParameter[] aggregateExpresssionsParameters, Type[] invokeTypes);
            public override Expression MakePropertyAccess(Expression baseExpression, string propertyPath);

            public override Type ObjectType { get; }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly MultiselectRoundedDateTimeFilterHelper.DTDescriptor.<>c <>9;
                public static Func<Type, ParameterExpression> <>9__6_0;
                public static Func<Type, ParameterExpression> <>9__7_0;

                static <>c();
                internal ParameterExpression <MakeFreeJoinLambda>b__6_0(Type t);
                internal ParameterExpression <MakeFreeJoinLambda>b__7_0(Type t);
            }
        }
    }
}

