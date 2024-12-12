namespace DevExpress.Data.PLinq.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    internal abstract class PLinqCallerCore
    {
        private static PLinqCallerCore.ReaderWriterLockSlim cacheLock;
        private static Dictionary<Tuple<Type, Type>, PLinqCallerCore> cache;

        static PLinqCallerCore();
        protected PLinqCallerCore();
        protected abstract ParallelQuery AsParallel(IEnumerable source, int? degreeOfParallelism);
        public static ParallelQuery AsParallel(Type sourceType, int? degreeOfParallelism, IEnumerable source);
        protected abstract bool Contains(ParallelQuery source, object value);
        public static bool Contains(Type sourceType, ParallelQuery source, object value);
        protected abstract int Count(ParallelQuery source);
        public static int Count(Type sourceType, ParallelQuery source);
        private static PLinqCallerCore GetCore(Type sourceType);
        private static PLinqCallerCore GetCore(Type sourceType, Type type2);
        protected abstract ParallelQuery GroupBy(ParallelQuery source, Delegate func);
        public static ParallelQuery GroupBy(Type sourceType, ParallelQuery source, Delegate func, Type resultType);
        protected abstract IEnumerable MakeReverse(IEnumerable source);
        public static IEnumerable MakeReverse(Type sourceType, IEnumerable source);
        protected abstract ParallelQuery OrderBy(ParallelQuery source, Delegate func);
        public static ParallelQuery OrderBy(Type sourceType, ParallelQuery source, Delegate func, Type resultType);
        protected abstract ParallelQuery OrderByDescending(ParallelQuery source, Delegate func);
        public static ParallelQuery OrderByDescending(Type sourceType, ParallelQuery source, Delegate func, Type resultType);
        protected abstract ParallelQuery Select(ParallelQuery source, Delegate func);
        public static ParallelQuery Select(Type sourceType, ParallelQuery source, Delegate func, Type resultType);
        protected abstract ParallelQuery Take(ParallelQuery source, int count);
        public static ParallelQuery Take(Type sourceType, ParallelQuery source, int count);
        protected abstract ParallelQuery ThenBy(ParallelQuery source, Delegate func);
        public static ParallelQuery ThenBy(Type sourceType, ParallelQuery source, Delegate func, Type resultType);
        protected abstract ParallelQuery ThenByDescending(ParallelQuery source, Delegate func);
        public static ParallelQuery ThenByDescending(Type sourceType, ParallelQuery source, Delegate func, Type resultType);
        protected abstract object[] ToArray(ParallelQuery source);
        public static object[] ToArray(Type sourceType, ParallelQuery source);
        protected abstract IList ToList(IEnumerable source);
        protected abstract IList ToList(ParallelQuery source);
        public static IList ToList(Type sourceType, IEnumerable source);
        public static IList ToList(Type sourceType, ParallelQuery source);
        protected abstract ParallelQuery Where(ParallelQuery source, Delegate func);
        public static ParallelQuery Where(Type sourceType, ParallelQuery source, Delegate func);

        private class PLinqCaller<TSource, TResult> : PLinqCallerCore
        {
            protected override ParallelQuery AsParallel(IEnumerable source, int? degreeOfParallelism);
            protected override bool Contains(ParallelQuery source, object value);
            protected override int Count(ParallelQuery source);
            protected override ParallelQuery GroupBy(ParallelQuery source, Delegate func);
            protected override IEnumerable MakeReverse(IEnumerable source);
            protected override ParallelQuery OrderBy(ParallelQuery source, Delegate func);
            protected override ParallelQuery OrderByDescending(ParallelQuery source, Delegate func);
            protected override ParallelQuery Select(ParallelQuery source, Delegate func);
            protected override ParallelQuery Take(ParallelQuery source, int count);
            protected override ParallelQuery ThenBy(ParallelQuery source, Delegate func);
            protected override ParallelQuery ThenByDescending(ParallelQuery source, Delegate func);
            protected override object[] ToArray(ParallelQuery source);
            protected override IList ToList(IEnumerable source);
            protected override IList ToList(ParallelQuery source);
            private static object ToObject(TSource obj);
            protected override ParallelQuery Where(ParallelQuery source, Delegate func);
        }

        private class ReaderWriterLockSlim
        {
            private object SyncRoot;

            public ReaderWriterLockSlim();
            public void EnterReadLock();
            public void EnterWriteLock();
            public void ExitReadLock();
            public void ExitWriteLock();
        }
    }
}

