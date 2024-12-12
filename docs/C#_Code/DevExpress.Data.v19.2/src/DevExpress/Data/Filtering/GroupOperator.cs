namespace DevExpress.Data.Filtering
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public sealed class GroupOperator : CriteriaOperator
    {
        private CriteriaOperatorCollection operands;
        [XmlAttribute]
        public GroupOperatorType OperatorType;

        public GroupOperator();
        public GroupOperator(params CriteriaOperator[] operands);
        public GroupOperator(GroupOperatorType type, IEnumerable<CriteriaOperator> operands);
        public GroupOperator(GroupOperatorType type, params CriteriaOperator[] operands);
        public override void Accept(ICriteriaVisitor visitor);
        public override T Accept<T>(ICriteriaVisitor<T> visitor);
        public GroupOperator Clone();
        protected override CriteriaOperator CloneCommon();
        public static CriteriaOperator Combine(GroupOperatorType opType, IEnumerable<CriteriaOperator> operands);
        public static CriteriaOperator Combine(GroupOperatorType opType, params CriteriaOperator[] operands);
        public static CriteriaOperator Combine(GroupOperatorType opType, CriteriaOperator left, CriteriaOperator right);
        [IteratorStateMachine(typeof(GroupOperator.<CombineCore>d__15))]
        private static IEnumerable<CriteriaOperator> CombineCore(GroupOperatorType opType, IEnumerable<CriteriaOperator> operands);
        public override bool Equals(object obj);
        public override int GetHashCode();

        [XmlArrayItem(typeof(CriteriaOperator))]
        public CriteriaOperatorCollection Operands { get; }

        [CompilerGenerated]
        private sealed class <CombineCore>d__15 : IEnumerable<CriteriaOperator>, IEnumerable, IEnumerator<CriteriaOperator>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private CriteriaOperator <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable<CriteriaOperator> operands;
            public IEnumerable<CriteriaOperator> <>3__operands;
            private GroupOperatorType opType;
            public GroupOperatorType <>3__opType;
            private GroupOperator <gop>5__1;
            private IEnumerator<CriteriaOperator> <>7__wrap1;
            private List<CriteriaOperator>.Enumerator <>7__wrap2;

            [DebuggerHidden]
            public <CombineCore>d__15(int <>1__state);
            private void <>m__Finally1();
            private void <>m__Finally2();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<CriteriaOperator> IEnumerable<CriteriaOperator>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            CriteriaOperator IEnumerator<CriteriaOperator>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

