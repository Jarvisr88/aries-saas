namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [DebuggerDisplay("{Begin}-{End}")]
    public class Interval<T> : IEquatable<Interval<T>> where T: struct
    {
        internal static readonly Interval<T>[] EmptyIntervals;
        private static readonly bool IsDateTime;
        private static readonly bool IsTimeSpan;

        static Interval();
        public Interval(T? begin, T? end);
        protected internal virtual bool CanMerge(Interval<T> interval);
        internal static T? CheckIsAfter(bool isGroup, T? val, bool? inclusive = new bool?());
        internal static T? CheckIsBefore(bool isGroup, T? val, bool? inclusive = new bool?());
        protected internal virtual bool Contains(Interval<T> interval);
        public bool Equals(Interval<T> interval);
        public sealed override bool Equals(object obj);
        protected virtual CriteriaOperator GetBinaryCriteria(string path, T? value, BinaryOperatorType operatorType);
        protected internal virtual CriteriaOperator GetCriteria(string path);
        private CriteriaOperator GetCriteria(string path, T? value, BinaryOperatorType operatorType);
        private CriteriaOperator GetCriteria(string path, T? fromValue, T? toValue);
        public sealed override int GetHashCode();
        protected virtual CriteriaOperator GetRangeCriteria(string path, CriteriaOperator fromCriteria, CriteriaOperator toCriteria);

        public T? Begin { get; private set; }

        public T? End { get; private set; }
    }
}

