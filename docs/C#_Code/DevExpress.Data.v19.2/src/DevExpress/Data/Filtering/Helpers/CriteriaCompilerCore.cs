namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class CriteriaCompilerCore : IClientCriteriaVisitor<Expression>, ICriteriaVisitor<Expression>
    {
        public readonly CriteriaCompilerContext Context;
        private static MethodInfo evalHelpersDoCustomAggregagteMethodInfo;
        private static readonly Dictionary<Type, MethodInfo> evalHelpersDoCustomAggregagteMethodInfoCache;
        private static readonly Dictionary<Type, Type> getGenericOperandDelegateTypeCache;

        static CriteriaCompilerCore();
        private CriteriaCompilerCore(CriteriaCompilerContext context);
        [CompilerGenerated]
        private Expression <DevExpress.Data.Filtering.IClientCriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__20_0(OperandParameter op);
        [CompilerGenerated]
        private Expression <DevExpress.Data.Filtering.IClientCriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__20_2(OperandParameter op);
        public static LambdaExpression Compile(CriteriaCompilerDescriptor descriptor, CriteriaCompilerAuxSettings settings, CriteriaOperator op);
        Expression IClientCriteriaVisitor<Expression>.Visit(AggregateOperand theOperand);
        Expression IClientCriteriaVisitor<Expression>.Visit(JoinOperand theOperand);
        Expression IClientCriteriaVisitor<Expression>.Visit(OperandProperty theOperand);
        Expression ICriteriaVisitor<Expression>.Visit(BetweenOperator theOperator);
        Expression ICriteriaVisitor<Expression>.Visit(BinaryOperator theOperator);
        Expression ICriteriaVisitor<Expression>.Visit(FunctionOperator theOperator);
        Expression ICriteriaVisitor<Expression>.Visit(GroupOperator theOperator);
        Expression ICriteriaVisitor<Expression>.Visit(InOperator theOperator);
        Expression ICriteriaVisitor<Expression>.Visit(OperandValue theOperand);
        Expression ICriteriaVisitor<Expression>.Visit(UnaryOperator theOperator);
        private static Expression DoFinalAggregate(CriteriaCompilerContext collectionContext, CriteriaOperator filter, Aggregate aggregateType, CriteriaOperator aggregateExpression);
        private Expression DoFinalAggregate(CriteriaCompilerContext collectionContext, CriteriaOperator filter, string customAggregateName, IEnumerable<CriteriaOperator> aggregateExpressions);
        private static Expression DoSubAggregate(CriteriaCompilerContext outerContext, string propertyPath, CriteriaOperator filter, Aggregate aggregateType, CriteriaOperator aggregateExpression);
        private Expression DoSubAggregate(CriteriaCompilerContext outerContext, string propertyPath, CriteriaOperator filter, string customAggregateName, IEnumerable<CriteriaOperator> aggregateExpressions);
        private static MethodInfo GetGenericMethodForExecuteAggregate(Type collectionElementType);
        private Type GetGenericOperandSelectorDelegateType(Type collectionElementType);
        private static void GuardNull(object arg, Func<string> getParameterName);
        private static void GuardNulls(IEnumerable arg, Func<int, string> getParameterName);
        private static bool IsConstantUntypedNull(Expression e);
        private static Expression MakeAggregate(Aggregate aggregate, Expression collectionArg, LambdaExpression argLambda, Type rowType, CriteriaCompilerAuxSettings settings);
        private static Expression MakeBinaryMath(Expression left, Expression right, Expression<Func<object, object, object>> objectsCombinator, Func<Expression, Expression, Expression> typedCombinator);
        private static Expression MakeCompare(Expression left, Expression right, bool isEqualsCompare, Func<int, bool> postCmpLambda, Func<Expression, Expression, Expression> directCmp, CriteriaCompilerAuxSettings auxSettings);
        private Expression MakeConcat(FunctionOperator theOperator);
        private Expression MakeCustom(FunctionOperator fn);
        private Expression MakeCustomAggregate(string customAggregateName, Expression collectionArg, LambdaExpression[] argLambdas, Type rowType, CriteriaCompilerAuxSettings settings);
        private static Expression MakeElementaryConditional(Expression conditionExpression, Expression trueExpression, Expression falseExpression);
        private static Expression MakeGroupCore(GroupOperatorType opType, Expression[] operands, int start, int count);
        private Expression MakeIif(FunctionOperator theOperator);
        private static LambdaExpression MakeInsideAggregateLambda(CriteriaCompilerDescriptor collectionAccessDescriptor, CriteriaCompilerContext donorContext, CriteriaOperator toCompile, bool needBoolRetType);
        private static LambdaExpression[] MakeInsideAggregateLambdas(CriteriaCompilerDescriptor collectionAccessDescriptor, CriteriaCompilerContext donorContext, IEnumerable<CriteriaOperator> toCompile);
        private static Expression MakeInSmart(Expression leftParam, IList<CriteriaOperator> operands, CriteriaCompilerCore.NestedLambdaCompiler nestedLambdaCompiler);
        private Expression MakeIsNull(FunctionOperator theOperator);
        private Expression MakeIsSameDay(FunctionOperator theOperator);
        private static Expression MakeSimpleIsNull(Expression operand);
        private static Expression PrepareCollectionExpressionForAggregate(CriteriaCompilerContext collectionContext, CriteriaOperator filter, out Type targetCollectionType, out CriteriaCompilerDescriptor rowDescriptor);
        private Expression Process(CriteriaOperator criteria);
        [IteratorStateMachine(typeof(CriteriaCompilerCore.<Process>d__44))]
        private IEnumerable<Expression> Process(IEnumerable<CriteriaOperator> ops);
        private static void ResolveElementaryConditionalType(ref Expression left, ref Expression right);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CriteriaCompilerCore.<>c <>9;
            public static Func<string> <>9__7_0;
            public static Func<int, string> <>9__8_0;
            public static Func<int, bool> <>9__10_0;
            public static Func<Expression, Expression, Expression> <>9__10_1;
            public static Func<int, bool> <>9__10_2;
            public static Func<Expression, Expression, Expression> <>9__10_3;
            public static Func<Expression, Type> <>9__20_1;
            public static Func<Expression, Type> <>9__20_3;
            public static Func<string> <>9__21_0;
            public static Func<string> <>9__21_1;
            public static Func<string> <>9__21_2;
            public static Func<int, bool> <>9__21_3;
            public static Func<Expression, Expression, Expression> <>9__21_4;
            public static Func<int, bool> <>9__21_5;
            public static Func<Expression, Expression, Expression> <>9__21_6;
            public static Func<string> <>9__22_0;
            public static Func<string> <>9__22_1;
            public static Func<int, bool> <>9__22_2;
            public static Func<Expression, Expression, Expression> <>9__22_3;
            public static Func<int, bool> <>9__22_4;
            public static Func<Expression, Expression, Expression> <>9__22_5;
            public static Func<int, bool> <>9__22_6;
            public static Func<Expression, Expression, Expression> <>9__22_7;
            public static Func<int, bool> <>9__22_8;
            public static Func<Expression, Expression, Expression> <>9__22_9;
            public static Func<int, bool> <>9__22_10;
            public static Func<Expression, Expression, Expression> <>9__22_11;
            public static Func<int, bool> <>9__22_12;
            public static Func<Expression, Expression, Expression> <>9__22_13;
            public static Func<string, string, string> <>9__22_14;
            public static Func<Expression, Expression, Expression> <>9__22_15;
            public static Func<Expression, Expression, Expression> <>9__22_16;
            public static Func<Expression, Expression, Expression> <>9__22_17;
            public static Func<Expression, Expression, Expression> <>9__22_18;
            public static Func<Expression, Expression, Expression> <>9__22_19;
            public static Func<Expression, Expression, Expression> <>9__22_20;
            public static Func<Expression, Expression, Expression> <>9__22_21;
            public static Func<Expression, Expression, Expression> <>9__22_22;
            public static Func<Expression, Expression, Expression> <>9__22_23;
            public static Func<Expression, Expression, Expression> <>9__22_24;
            public static Func<Expression, Expression, Expression> <>9__22_25;
            public static Func<string> <>9__27_0;
            public static Func<string> <>9__28_0;
            public static Func<int, string> <>9__28_1;
            public static Func<OperandValue, object> <>9__30_0;
            public static Func<int, bool> <>9__30_2;
            public static Func<Expression, Expression, Expression> <>9__30_3;
            public static Func<CriteriaOperator, bool> <>9__31_0;
            public static Func<int, string> <>9__34_0;
            public static Func<Expression, Type> <>9__34_1;
            public static Func<int, string> <>9__35_0;
            public static Func<CriteriaOperator, CriteriaOperator> <>9__35_1;
            public static Func<Expression, Type> <>9__37_0;
            public static Func<Expression, Expression> <>9__37_1;

            static <>c();
            internal Type <DevExpress.Data.Filtering.IClientCriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__20_1(Expression p);
            internal Type <DevExpress.Data.Filtering.IClientCriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__20_3(Expression p);
            internal string <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__21_0();
            internal string <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__21_1();
            internal string <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__21_2();
            internal bool <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__21_3(int cmp);
            internal Expression <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__21_4(Expression x, Expression y);
            internal bool <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__21_5(int cmp);
            internal Expression <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__21_6(Expression x, Expression y);
            internal string <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_0();
            internal string <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_1();
            internal bool <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_10(int cmp);
            internal Expression <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_11(Expression x, Expression y);
            internal bool <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_12(int cmp);
            internal Expression <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_13(Expression x, Expression y);
            internal string <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_14(string l, string r);
            internal Expression <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_15(Expression l, Expression r);
            internal Expression <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_16(Expression l, Expression r);
            internal Expression <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_17(Expression l, Expression r);
            internal Expression <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_18(Expression l, Expression r);
            internal Expression <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_19(Expression l, Expression r);
            internal bool <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_2(int cmp);
            internal Expression <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_20(Expression l, Expression r);
            internal Expression <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_21(Expression l, Expression r);
            internal Expression <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_22(Expression l, Expression r);
            internal Expression <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_23(Expression l, Expression r);
            internal Expression <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_24(Expression l, Expression r);
            internal Expression <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_25(Expression l, Expression r);
            internal Expression <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_3(Expression x, Expression y);
            internal bool <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_4(int cmp);
            internal Expression <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_5(Expression x, Expression y);
            internal bool <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_6(int cmp);
            internal Expression <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_7(Expression x, Expression y);
            internal bool <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_8(int cmp);
            internal Expression <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__22_9(Expression x, Expression y);
            internal string <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__27_0();
            internal string <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__28_0();
            internal string <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__28_1(int i);
            internal bool <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__31_0(CriteriaOperator c);
            internal string <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__34_0(int i);
            internal Type <DevExpress.Data.Filtering.ICriteriaVisitor<System.Linq.Expressions.Expression>.Visit>b__34_1(Expression x);
            internal string <DoFinalAggregate>b__7_0();
            internal string <DoFinalAggregate>b__8_0(int i);
            internal bool <MakeAggregate>b__10_0(int cmp);
            internal Expression <MakeAggregate>b__10_1(Expression x, Expression y);
            internal bool <MakeAggregate>b__10_2(int cmp);
            internal Expression <MakeAggregate>b__10_3(Expression x, Expression y);
            internal Type <MakeCustom>b__37_0(Expression a);
            internal Expression <MakeCustom>b__37_1(Expression p);
            internal object <MakeInSmart>b__30_0(OperandValue op);
            internal bool <MakeInSmart>b__30_2(int i);
            internal Expression <MakeInSmart>b__30_3(Expression x, Expression y);
            internal string <MakeIsSameDay>b__35_0(int i);
            internal CriteriaOperator <MakeIsSameDay>b__35_1(CriteriaOperator op);
        }

        [CompilerGenerated]
        private sealed class <Process>d__44 : IEnumerable<Expression>, IEnumerable, IEnumerator<Expression>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Expression <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable<CriteriaOperator> ops;
            public IEnumerable<CriteriaOperator> <>3__ops;
            public CriteriaCompilerCore <>4__this;
            private IEnumerator<CriteriaOperator> <>7__wrap1;

            [DebuggerHidden]
            public <Process>d__44(int <>1__state);
            private void <>m__Finally1();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<Expression> IEnumerable<Expression>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            Expression IEnumerator<Expression>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }

        private static class AggregatesHelpers
        {
            public static decimal? AvgDecimal(IEnumerable<decimal?> seq);
            public static double? AvgDouble(IEnumerable<double?> seq);
            public static double? AvgInt32(IEnumerable<int?> seq);
            public static double? AvgInt64(IEnumerable<long?> seq);
            public static object AvgObject(IEnumerable<object> seq);
            public static float? AvgSingle(IEnumerable<float?> seq);
            public static double? AvgUInt32(IEnumerable<uint?> seq);
            public static double? AvgUInt64(IEnumerable<ulong?> seq);
            public static IEnumerable<T> Cast<T>(IEnumerable seq);
            public static int? Count<T>(IEnumerable<T> seq);
            public static bool Exists<T>(IEnumerable<T> seq);
            public static T MinMaxClass<T>(IEnumerable<T> seq, Func<T, T, bool> isBetter) where T: class;
            public static T? MinMaxStruct<T>(IEnumerable<T> seq, Func<T, T, bool> isBetter) where T: struct;
            public static IEnumerable<E> Select<T, E>(IEnumerable<T> seq, Func<T, E> selector);
            public static T Single<T>(IEnumerable<T> seq);
            public static decimal? SumDecimal(IEnumerable<decimal?> seq);
            public static double? SumDouble(IEnumerable<double?> seq);
            public static int? SumInt32(IEnumerable<int?> seq);
            public static long? SumInt64(IEnumerable<long?> seq);
            public static object SumObject(IEnumerable<object> seq);
            public static float? SumSingle(IEnumerable<float?> seq);
            public static uint? SumUInt32(IEnumerable<uint?> seq);
            public static ulong? SumUInt64(IEnumerable<ulong?> seq);
            public static IEnumerable<T> Where<T>(IEnumerable<T> seq, Func<T, bool> predicate);
        }

        public static class CompareHelper
        {
            public static int DoCompare<T>(T x, T y);
            public static bool DoEquals<T>(T x, T y);
            public static bool IsUnknown<A, B>(A x, B y);
        }

        public class CriteriaCompilerDownLevelContext : CriteriaCompilerCore.CriteriaCompilerNestedContext
        {
            private readonly CriteriaCompilerLocalContext SelfContext;

            public CriteriaCompilerDownLevelContext(CriteriaCompilerContext parent, CriteriaCompilerLocalContext _SelfContext);
            protected override CriteriaCompilerLocalContext GetParentPair(int upLevels);
        }

        public class CriteriaCompilerInsideAggregateClojureContext : CriteriaCompilerContext
        {
            public readonly CriteriaCompilerContext Donor;
            public CriteriaCompilerLocalContext SelfContext;

            public CriteriaCompilerInsideAggregateClojureContext(CriteriaCompilerContext _Donor, CriteriaCompilerLocalContext _SelfContext);
            public override CriteriaCompilerLocalContext GetLocalContext(int upLevels);

            public override CriteriaCompilerAuxSettings AuxSettings { get; }
        }

        public class CriteriaCompilerLambdaContext : CriteriaCompilerCore.CriteriaCompilerNestedContext
        {
            public CriteriaCompilerLambdaContext(CriteriaCompilerContext parent, params KeyValuePair<ParameterExpression, Expression>[] mandatoryParams);
            protected override CriteriaCompilerLocalContext GetParentPair(int upLevels);
        }

        public abstract class CriteriaCompilerNestedContext : CriteriaCompilerContext
        {
            protected readonly CriteriaCompilerContext Parent;
            private readonly List<Expression> AuxArgs;
            private readonly List<ParameterExpression> AuxParams;
            private Dictionary<int, CriteriaCompilerLocalContext> Map;
            private bool auxExported;

            public CriteriaCompilerNestedContext(CriteriaCompilerContext _Parent, params KeyValuePair<ParameterExpression, Expression>[] mandatoryParams);
            public Expression[] GetArgs();
            public sealed override CriteriaCompilerLocalContext GetLocalContext(int upLevels);
            public ParameterExpression[] GetParams();
            protected abstract CriteriaCompilerLocalContext GetParentPair(int upLevels);

            public override CriteriaCompilerAuxSettings AuxSettings { get; }
        }

        public class CriteriaCompilerUpLevelsContext : CriteriaCompilerCore.CriteriaCompilerNestedContext
        {
            private readonly int UpLevels;

            public CriteriaCompilerUpLevelsContext(CriteriaCompilerContext parent, int upLevels);
            protected override CriteriaCompilerLocalContext GetParentPair(int upLevels);
        }

        public static class InSmartHelper
        {
            public static Expression MakeInAgainstConstants(Expression leftParam, IEnumerable boxedValues, Type t, CriteriaCompilerAuxSettings auxSettings);
            public static Delegate MakeNullablePrimitiveIn(Type type, IEnumerable boxedValues);
            public static Delegate MakePrimitiveIn(Type type, IEnumerable boxedValues);
            private static Func<string, bool> MakeStringIn(IEnumerable boxedValues, CriteriaCompilerAuxSettings auxSettings);
            public static Array ToTypedArray(Type type, IEnumerable boxed);

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly CriteriaCompilerCore.InSmartHelper.<>c <>9;
                public static Func<int, bool> <>9__6_0;
                public static Func<Expression, Expression, Expression> <>9__6_1;

                static <>c();
                internal bool <MakeInAgainstConstants>b__6_0(int i);
                internal Expression <MakeInAgainstConstants>b__6_1(Expression x, Expression y);
            }

            public abstract class AnyBase
            {
                protected AnyBase();
                public static CriteriaCompilerCore.InSmartHelper.AnyBase GetImpl(Type type);
                public abstract Array ToTypedArray(IEnumerable boxed);

                public class Impl<T> : CriteriaCompilerCore.InSmartHelper.AnyBase
                {
                    public override Array ToTypedArray(IEnumerable boxed);
                }
            }

            public abstract class StructBase
            {
                protected StructBase();
                public static CriteriaCompilerCore.InSmartHelper.StructBase GetImpl(Type type);
                public abstract Delegate MakeNullablePrimitiveIn(IEnumerable boxedValues);
                public abstract Delegate MakePrimitiveIn(IEnumerable boxedValues);

                public class Impl<T> : CriteriaCompilerCore.InSmartHelper.StructBase where T: struct
                {
                    public override Delegate MakeNullablePrimitiveIn(IEnumerable boxedValues);
                    public override Delegate MakePrimitiveIn(IEnumerable boxedValues);
                    private static Func<T, bool> MakePrimitiveInCore(IEnumerable boxedValues);
                }
            }
        }

        public class NestedLambdaCompiler
        {
            public readonly CriteriaCompilerCore.CriteriaCompilerNestedContext Context;
            private CriteriaCompilerCore _Core;

            public NestedLambdaCompiler(CriteriaCompilerCore.CriteriaCompilerNestedContext context);
            public Expression Make(Expression body);
            public Expression MakeCompiled(Expression body);
            public Expression Process(CriteriaOperator op);

            protected CriteriaCompilerCore Core { get; }
        }
    }
}

