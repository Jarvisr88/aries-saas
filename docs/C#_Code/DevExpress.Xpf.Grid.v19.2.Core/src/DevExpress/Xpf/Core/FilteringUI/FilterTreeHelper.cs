namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    internal static class FilterTreeHelper
    {
        public static bool AllowDateTimeFilters(FilterRestrictions filterRestrictions) => 
            filterRestrictions.AllowedDateTimeFilters.HasFlag(AllowedDateTimeFilters.MultipleDateRanges);

        public static CriteriaOperator BuildCustomValuesFilter(GroupNode<NodeValueInfo> node, string propertyName, Func<object, bool> checkIsCustomValue)
        {
            List<object> items = new List<object>();
            List<CriteriaOperator> list2 = new List<CriteriaOperator>();
            CriteriaOperator @operator = null;
            foreach (object obj2 in GetCheckedCustomValues(node, checkIsCustomValue))
            {
                if (obj2 is CriteriaOperator)
                {
                    @operator |= (CriteriaOperator) obj2;
                    continue;
                }
                items.Add(obj2);
            }
            Func<object, object> getValue = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Func<object, object> local1 = <>c.<>9__7_0;
                getValue = <>c.<>9__7_0 = x => x;
            }
            return (ColumnFilterInfoHelper.CreateCriteriaOperator(items, propertyName, false, getValue) | @operator);
        }

        public static CriteriaOperator BuildSelectAllNodesSkipBlankFilter(CriteriaOperator isNullCriteria) => 
            new UnaryOperator(UnaryOperatorType.Not, isNullCriteria);

        public static Func<object, bool> CreateContainsValueFunc(string propertyName, Type propertyType, CriteriaOperator filter, Func<CriteriaOperator, CriteriaOperator> substituteFilter)
        {
            filter = substituteFilter(filter);
            if (filter == null)
            {
                return (<>c.<>9__9_0 ??= x => false);
            }
            List<string> source = FilteringUIContext.ExtractProperties(filter);
            if ((source.Count > 1) || (source.SingleOrDefault<string>() != propertyName))
            {
                return (<>c.<>9__9_1 ??= x => false);
            }
            try
            {
                return CriteriaCompiler.ToPredicate<object>(filter, new FilterParserDescriptor(propertyName), new CriteriaCompilerAuxSettings(true));
            }
            catch
            {
                Func<object, bool> func1 = <>c.<>9__9_2;
                if (<>c.<>9__9_2 == null)
                {
                    Func<object, bool> local4 = <>c.<>9__9_2;
                    func1 = <>c.<>9__9_2 = _ => false;
                }
                return func1;
            }
        }

        private static List<object> GetCheckedCustomValues(NodeBase<NodeValueInfo> node, Func<object, bool> checkIsCustomValue) => 
            GetValueNodesValuesCore(node, true, checkIsCustomValue);

        public static List<object> GetCheckedRegularValues(NodeBase<NodeValueInfo> node, Func<object, bool> checkIsRegularValue) => 
            GetValueNodesValuesCore(node, true, checkIsRegularValue);

        public static BlankOperations GetStringBlankOperations(bool implyEmptyStringLikeBlank)
        {
            Func<object, bool> isEmptyValue = <>c.<>9__15_2;
            if (<>c.<>9__15_2 == null)
            {
                Func<object, bool> local1 = <>c.<>9__15_2;
                isEmptyValue = <>c.<>9__15_2 = value => (value as string) == string.Empty;
            }
            return new BlankOperations(value => ObjectBlankOperations.IsBlankValue(value) || (implyEmptyStringLikeBlank && ((value as string) == string.Empty)), delegate (string propertyName, AllowedUnaryFilters allowedFilters) {
                if (!(allowedFilters.HasFlag(AllowedUnaryFilters.IsNullOrEmpty) & implyEmptyStringLikeBlank))
                {
                    return ObjectBlankOperations.CreateIsNullOperator(propertyName, allowedFilters);
                }
                CriteriaOperator[] operands = new CriteriaOperator[] { new OperandProperty(propertyName) };
                return new FunctionOperator(FunctionOperatorType.IsNullOrEmpty, operands);
            }, isEmptyValue);
        }

        public static List<object> GetUncheckedRegularValues(NodeBase<NodeValueInfo> node, Func<object, bool> checkIsRegularValue) => 
            GetValueNodesValuesCore(node, false, checkIsRegularValue);

        private static List<object> GetValueNodesValuesCore(NodeBase<NodeValueInfo> node, bool isChecked, Func<object, bool> takeValue)
        {
            Func<ValueNode<NodeValueInfo>, object> selector = <>c.<>9__1_1;
            if (<>c.<>9__1_1 == null)
            {
                Func<ValueNode<NodeValueInfo>, object> local1 = <>c.<>9__1_1;
                selector = <>c.<>9__1_1 = x => x.Value.Value;
            }
            return (from x in CheckedTreeHelper.GetDepthFirstNodes<NodeValueInfo>(node).OfType<ValueNode<NodeValueInfo>>().Where<ValueNode<NodeValueInfo>>(delegate (ValueNode<NodeValueInfo> x) {
                bool? actualIsChecked = x.ActualIsChecked;
                bool flag = isChecked;
                return ((actualIsChecked.GetValueOrDefault() == flag) ? (actualIsChecked != null) : false);
            }).Select<ValueNode<NodeValueInfo>, object>(selector)
                where takeValue(x)
                select x).ToList<object>();
        }

        public static bool IsDateTimeProperty(Type propertyType) => 
            (propertyType == typeof(DateTime)) || (propertyType == typeof(DateTime?));

        public static bool IsSelectAllNodesExceptBlank(GroupNode<NodeValueInfo> node, ValueNode<NodeValueInfo> blanksNode)
        {
            if ((blanksNode == null) || blanksNode.ActualIsChecked.Value)
            {
                return false;
            }
            List<object> source = GetValueNodesValuesCore(node, false, n => n != blanksNode);
            return ((source.Count == 1) && (source.Single<object>() == blanksNode.Value.Value));
        }

        public static List<CriteriaOperator> SplitCriteriaOperator(CriteriaOperator op)
        {
            if (op == null)
            {
                return new List<CriteriaOperator>();
            }
            GroupOperator @operator = op as GroupOperator;
            if ((@operator != null) && (@operator.OperatorType == GroupOperatorType.Or))
            {
                return @operator.Operands.ToList<CriteriaOperator>();
            }
            List<CriteriaOperator> list1 = new List<CriteriaOperator>();
            list1.Add(op);
            return list1;
        }

        public static BlankOperations ObjectBlankOperations { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterTreeHelper.<>c <>9 = new FilterTreeHelper.<>c();
            public static Func<ValueNode<NodeValueInfo>, object> <>9__1_1;
            public static Func<object, object> <>9__7_0;
            public static Func<object, bool> <>9__9_0;
            public static Func<object, bool> <>9__9_1;
            public static Func<object, bool> <>9__9_2;
            public static Func<object, bool> <>9__15_2;

            internal bool <.cctor>b__16_0(object value) => 
                (value == null) || (value is DBNull);

            internal CriteriaOperator <.cctor>b__16_1(string propertyName, AllowedUnaryFilters allowedFilters) => 
                allowedFilters.HasFlag(AllowedUnaryFilters.IsNull) ? new UnaryOperator(UnaryOperatorType.IsNull, propertyName) : null;

            internal bool <.cctor>b__16_2(object value) => 
                false;

            internal object <BuildCustomValuesFilter>b__7_0(object x) => 
                x;

            internal bool <CreateContainsValueFunc>b__9_0(object x) => 
                false;

            internal bool <CreateContainsValueFunc>b__9_1(object x) => 
                false;

            internal bool <CreateContainsValueFunc>b__9_2(object _) => 
                false;

            internal bool <GetStringBlankOperations>b__15_2(object value) => 
                (value as string) == string.Empty;

            internal object <GetValueNodesValuesCore>b__1_1(ValueNode<NodeValueInfo> x) => 
                x.Value.Value;
        }

        private class FilterParserDescriptor : CriteriaCompilerDescriptor
        {
            protected readonly string PropertyName;

            public FilterParserDescriptor(string propertyName)
            {
                this.PropertyName = propertyName;
            }

            public override Expression MakePropertyAccess(Expression baseExpression, string propertyPath)
            {
                if (propertyPath != this.PropertyName)
                {
                    throw new InvalidOperationException();
                }
                return baseExpression;
            }

            public override Type ObjectType =>
                typeof(object);
        }
    }
}

