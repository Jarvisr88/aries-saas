namespace DevExpress.Data
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    public abstract class BaseRowStub
    {
        private readonly DataControllerBase DC;
        private readonly Action AdditionalCleanUp;
        private int _RowIndex;
        private object _Row;
        private bool RowGot;

        protected BaseRowStub(DataControllerBase _DC, Action additionalCleanUp);
        public void GoTo(int rowIndex);
        public void Reset();

        public int RowIndex { get; }

        public object Row { get; }

        public class CachingCriteriaCompilerDescriptor : CriteriaCompilerDescriptor
        {
            private readonly Dictionary<string, BaseRowStub.ValueCacheBase> CachesCache;
            private readonly CriteriaCompilerDescriptor FlatDescriptor;
            private readonly Func<string, bool> IsCacheable;

            public CachingCriteriaCompilerDescriptor(CriteriaCompilerDescriptor flatDescriptor, CriteriaOperator op);
            private CachingCriteriaCompilerDescriptor(CriteriaCompilerDescriptor flatDescriptor, Func<string, bool> isCacheable);
            public override CriteriaCompilerRefResult DiveIntoCollectionProperty(Expression baseExpression, string collectionPropertyPath);
            public Action GetStubCleanUpCode();
            public override LambdaExpression MakeFreeJoinLambda(string joinTypeName, CriteriaOperator condition, OperandParameter[] conditionParameters, Aggregate aggregateType, CriteriaOperator aggregateExpression, OperandParameter[] aggregateExpresssionParameters, Type[] invokeTypes);
            public override LambdaExpression MakeFreeJoinLambda(string joinTypeName, CriteriaOperator condition, OperandParameter[] conditionParameters, string customAggregateName, IEnumerable<CriteriaOperator> aggregateExpressions, OperandParameter[] aggregateExpresssionsParameters, Type[] invokeTypes);
            public override Expression MakePropertyAccess(Expression baseExpression, string propertyPath);

            public override Type ObjectType { get; }

            public class CacheInfoCollector : IClientCriteriaVisitor, ICriteriaVisitor
            {
                private Dictionary<string, int> Counter;

                private CacheInfoCollector();
                public static Func<string, bool> GetIsCacheablePropertyFunc(CriteriaOperator op);
                private void Process(CriteriaOperator op);
                private void Process(IEnumerable<CriteriaOperator> ops);
                public void Visit(AggregateOperand theOperand);
                public void Visit(BetweenOperator theOperator);
                public void Visit(BinaryOperator theOperator);
                public void Visit(FunctionOperator theOperator);
                public void Visit(GroupOperator theOperator);
                public void Visit(InOperator theOperator);
                public void Visit(JoinOperand theOperand);
                public void Visit(OperandProperty theOperand);
                public void Visit(OperandValue theOperand);
                public void Visit(UnaryOperator theOperator);

                [Serializable, CompilerGenerated]
                private sealed class <>c
                {
                    public static readonly BaseRowStub.CachingCriteriaCompilerDescriptor.CacheInfoCollector.<>c <>9;
                    public static Func<KeyValuePair<string, int>, bool> <>9__14_0;
                    public static Func<KeyValuePair<string, int>, string> <>9__14_1;
                    public static Func<string, bool> <>9__14_2;
                    public static Func<string, string> <>9__14_4;
                    public static Func<string, string> <>9__14_5;

                    static <>c();
                    internal bool <GetIsCacheablePropertyFunc>b__14_0(KeyValuePair<string, int> kvp);
                    internal string <GetIsCacheablePropertyFunc>b__14_1(KeyValuePair<string, int> kvp);
                    internal bool <GetIsCacheablePropertyFunc>b__14_2(string x);
                    internal string <GetIsCacheablePropertyFunc>b__14_4(string s);
                    internal string <GetIsCacheablePropertyFunc>b__14_5(string s);
                }
            }
        }

        public class CriteriaCompilerNullRowProofDescriptor : CriteriaCompilerDescriptor
        {
            private readonly CriteriaCompilerDescriptor Nested;

            public CriteriaCompilerNullRowProofDescriptor(CriteriaCompilerDescriptor _Nested);
            public override CriteriaCompilerRefResult DiveIntoCollectionProperty(Expression baseExpression, string collectionPropertyPath);
            public override LambdaExpression MakeFreeJoinLambda(string joinTypeName, CriteriaOperator condition, OperandParameter[] conditionParameters, Aggregate aggregateType, CriteriaOperator aggregateExpression, OperandParameter[] aggregateExpresssionParameters, Type[] invokeTypes);
            public override LambdaExpression MakeFreeJoinLambda(string joinTypeName, CriteriaOperator condition, OperandParameter[] conditionParameters, string customAggregateName, IEnumerable<CriteriaOperator> aggregateExpressions, OperandParameter[] aggregateExpresssionsParameters, Type[] invokeTypes);
            public override Expression MakePropertyAccess(Expression baseExpression, string propertyPath);

            public override Type ObjectType { get; }
        }

        public class DataControllerCriteriaCompilerDescriptor : CriteriaCompilerDescriptor
        {
            private readonly Func<PropertyDescriptorCollection> PropertyDescriptorsSource;
            private PropertyDescriptorCollection _Props;
            private CriteriaCompilerDescriptor _WorkHorseContext;

            public DataControllerCriteriaCompilerDescriptor(DataControllerBase dc);
            public DataControllerCriteriaCompilerDescriptor(Func<PropertyDescriptorCollection> propertyDescriptorsSource);
            public override CriteriaCompilerRefResult DiveIntoCollectionProperty(Expression baseExpression, string collectionPropertyPath);
            public override Expression MakePropertyAccess(Expression baseExpression, string propertyPath);

            protected PropertyDescriptorCollection Props { get; }

            protected CriteriaCompilerDescriptor WorkHorseContext { get; }

            public override Type ObjectType { get; }
        }

        public sealed class ValueCache<T> : BaseRowStub.ValueCacheBase
        {
            private readonly Func<BaseRowStub, T> ValueGetter;
            private bool ValueGot;
            private T Value;

            public ValueCache(Func<BaseRowStub, T> _ValueGetter);
            public T GetValue(BaseRowStub arg);
            public override void Reset();

            public override System.Type Type { get; }
        }

        public abstract class ValueCacheBase
        {
            protected ValueCacheBase();
            public abstract void Reset();

            public abstract System.Type Type { get; }
        }
    }
}

