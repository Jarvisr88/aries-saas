namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class CriteriaProcessingExtensions
    {
        public static void Execute<T>(this CriteriaOperator criterion, Action<T> action) where T: CriteriaOperator;
        public static void Execute<T>(this CriteriaOperator criterion, Action<T> action, Action<CriteriaOperator> otherwise) where T: CriteriaOperator;
        public static void Execute<T>(this CriteriaOperator criterion, Predicate<T> condition, Action<T> action) where T: CriteriaOperator;
        public static void Execute<T>(this CriteriaOperator criterion, Predicate<T> condition, Action<T> action, Action<CriteriaOperator> otherwise) where T: CriteriaOperator;
        public static int GetHashCodeNullSafe(this CriteriaOperator criterion);
        public static bool Is<T>(this CriteriaOperator criterion) where T: CriteriaOperator;
        public static bool Is<T>(this CriteriaOperator criterion, out T t) where T: CriteriaOperator;
        public static bool Is<T>(this CriteriaOperator criterion, Predicate<T> condition) where T: CriteriaOperator;
        public static bool Is<T>(this CriteriaOperator criterion, Predicate<T> condition, out T t) where T: CriteriaOperator;
        public static bool ReferenceEqualsNull(this CriteriaOperator criterion);
        public static CriteriaOperator Transform<T>(this CriteriaOperator criterion, Func<T, CriteriaOperator> func) where T: CriteriaOperator;
        public static R Transform<T, R>(this CriteriaOperator criterion, Func<T, R> func, Func<CriteriaOperator, R> otherwise) where T: CriteriaOperator;
        public static CriteriaOperator Transform<T>(this CriteriaOperator criterion, Predicate<T> condition, Func<T, CriteriaOperator> func) where T: CriteriaOperator;
        public static R Transform<T, R>(this CriteriaOperator criterion, Predicate<T> condition, Func<T, R> func, Func<CriteriaOperator, R> otherwise) where T: CriteriaOperator;
    }
}

