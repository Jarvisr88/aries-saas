namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class JoinEvaluationContextCache
    {
        private readonly Stack<IEnumerable> collectionContextStack;
        private readonly Stack<Dictionary<JoinEvaluationContextCache.JoinEvaluationContextCacheKey, JoinEvaluationContextCache.JoinEvaluationCacheInfo>> cacheStack;
        private Dictionary<JoinEvaluationContextCache.JoinEvaluationContextCacheKey, JoinEvaluationContextCache.JoinEvaluationCacheInfo> cacheDict;

        public JoinEvaluationContextCache();
        public IEnumerable GetQueryContexts(EvaluatorContext[] contexts, string queryTypeName, CriteriaOperator condition, int top, out bool filtered);
        public IEnumerable PopCollectionContext();
        public void PushCollectionContext(IEnumerable context);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly JoinEvaluationContextCache.<>c <>9;
            public static Func<JoinEvaluationContextCache.CriteriaOperatorKey, CriteriaOperator> <>9__6_0;
            public static Func<JoinEvaluationContextCache.CriteriaOperatorKey, CriteriaOperator> <>9__6_1;

            static <>c();
            internal CriteriaOperator <GetQueryContexts>b__6_0(JoinEvaluationContextCache.CriteriaOperatorKey key);
            internal CriteriaOperator <GetQueryContexts>b__6_1(JoinEvaluationContextCache.CriteriaOperatorKey key);
        }

        private class CriteriaOperatorKey
        {
            public readonly CriteriaOperator Condition;

            public CriteriaOperatorKey(CriteriaOperator condition);
            public override bool Equals(object obj);
            public override int GetHashCode();
        }

        private class JoinEvaluationCacheChunk
        {
            private bool isEmpty;
            private IEnumerable objects;
            private CriteriaOperator criteria;

            public JoinEvaluationCacheChunk(CriteriaOperator criteria);
            public void Fill(EvaluatorContext context, string queryTypeName);

            public bool IsEmpty { get; }

            public IEnumerable Objects { get; }

            public CriteriaOperator Criteria { get; }
        }

        private class JoinEvaluationCacheInfo
        {
            private readonly List<JoinEvaluationContextCache.JoinEvaluationCacheChunk> chunks;
            private readonly Dictionary<object, int> allChunksObjects;

            public JoinEvaluationCacheInfo(List<JoinEvaluationContextCache.JoinEvaluationCacheChunk> chunks, Dictionary<object, int> allChunksObjects);

            public List<JoinEvaluationContextCache.JoinEvaluationCacheChunk> Chunks { get; }

            public Dictionary<object, int> AllChunksObjects { get; }
        }

        private class JoinEvaluationContextCacheKey
        {
            public readonly string QueryTypeName;
            public readonly CriteriaOperator Condition;

            public JoinEvaluationContextCacheKey(string queryTypeName, CriteriaOperator condition);
            public override bool Equals(object obj);
            public override int GetHashCode();
        }
    }
}

