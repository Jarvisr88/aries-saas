namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal sealed class GroupValuesParser<T> : IGroupValuesParser, IClientCriteriaVisitor, ICriteriaVisitor
    {
        private readonly string[] grouping;
        private readonly Type[] groupingTypes;
        private bool? intervalsValid;
        private readonly IList<GroupValuesParser<T>.Interval> intervals;
        private int level;
        private int key;
        private int valueKey;
        private GroupValuesParser<T>.Interval interval;
        private GroupValuesParser<T>.Interval branchRoot;
        private readonly IDictionary<int, GroupValuesParser<T>.Interval> branch;
        private int? branchLevel;
        private bool InvalidCore;
        private readonly IList<GroupValuesParser<T>.Interval> parents;
        private int? inversionCounter;
        private readonly HashSet<int> consequentValues;
        private readonly Stack<GroupOperatorType> groupTypes;

        public GroupValuesParser(string[] grouping, Type[] groupingTypes = null);
        private void CheckIsInverted(GroupValuesParser<T>.Interval interval);
        void IClientCriteriaVisitor.Visit(AggregateOperand theOperand);
        void IClientCriteriaVisitor.Visit(JoinOperand theOperand);
        void IClientCriteriaVisitor.Visit(OperandProperty theOperand);
        void ICriteriaVisitor.Visit(BetweenOperator theOperator);
        void ICriteriaVisitor.Visit(BinaryOperator theOperator);
        void ICriteriaVisitor.Visit(FunctionOperator theOperator);
        void ICriteriaVisitor.Visit(GroupOperator theOperator);
        void ICriteriaVisitor.Visit(InOperator theOperator);
        void ICriteriaVisitor.Visit(OperandValue theOperand);
        void ICriteriaVisitor.Visit(UnaryOperator theOperator);
        void IGroupValuesParser.Accept(Action<ICheckedGroup> visit);
        private void MarkAsInvalid();
        private void QueueInversion();
        private void QueueInversion(int count);
        private void QueueInversionForNextValue();
        private void RestoreRoot();
        private bool TryGetValue(OperandValue theOperand, out object typedValue);
        private bool ValidateIntervals(GroupValuesParser<T>.Interval[] intervals);
        private bool ValidateParentValues(GroupValuesParser<T>.Interval interval);
        private bool ValidateParentValues(int level, GroupOperatorType type);
        private bool ValidateValues(GroupValuesParser<T>.Interval interval);

        bool IGroupValuesParser.Invalid { get; }

        private bool InversionQueued { get; }

        private GroupOperatorType? CurrentGroupType { get; }

        private sealed class Interval : ICheckedGroup, IComparer
        {
            internal readonly int Level;
            internal readonly int Group;
            private readonly object[] Path;
            private readonly GroupOperatorType? groupType;
            private bool invertedCore;
            private readonly List<object> values;
            private int valuesCounter;
            private int parentValuesCounter;
            internal static readonly IComparer<GroupValuesParser<T>.Interval> DefaultComparer;

            static Interval();
            public Interval(int level, int group, object value, GroupOperatorType? groupType);
            public void AddParentValue(GroupValuesParser<T>.Interval interval);
            public void AddValue(object value, bool consequent);
            public bool AreParentValuesInvalid(GroupOperatorType? groupType);
            public bool AreParentValuesInvalid(object value);
            public bool AreParentValuesInvalid(GroupOperatorType? groupType, GroupValuesParser<T>.Interval interval);
            private bool AreValuesInvalid(GroupOperatorType? groupType, int count);
            public bool CanMerge(GroupValuesParser<T>.Interval interval);
            public bool CanMerge(int level, int group, bool inverted);
            private static int CompareValue(object x, object y);
            public bool IsParent(int level, int key);
            public bool IsParentValue(int level, int key);
            public void MarkAsInverted();
            public object RemoveLast();
            public void SetParent(GroupValuesParser<T>.Interval interval, object value);
            int IComparer.Compare(object x, object y);
            public bool TryGetValue(int level, out object value);

            int ICheckedGroup.Group { get; }

            object[] ICheckedGroup.Path { get; }

            object[] ICheckedGroup.Values { get; }

            bool ICheckedGroup.IsInverted { get; }

            public bool HasValues { get; }

            public object LastValue { get; }

            public bool InvalidPath { get; }

            public bool InvalidValues { get; }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly GroupValuesParser<T>.Interval.<>c <>9;
                public static Predicate<object> <>9__26_0;

                static <>c();
                internal bool <get_InvalidPath>b__26_0(object x);
            }

            private sealed class IntervalComparer : IComparer<GroupValuesParser<T>.Interval>
            {
                int IComparer<GroupValuesParser<T>.Interval>.Compare(GroupValuesParser<T>.Interval x, GroupValuesParser<T>.Interval y);
            }
        }
    }
}

