namespace DevExpress.Data
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public sealed class UnboundCriteriaInliner : ClientCriteriaLazyPatcherBase.AggregatesAsIsBase
    {
        private readonly Func<OperandProperty, CriteriaOperator> GetUnboundCriteria;
        private readonly CriteriaOperator TopMostCriteria;
        private Stack<Tuple<string, CriteriaOperator>> recursionWatch;

        private UnboundCriteriaInliner(Func<OperandProperty, CriteriaOperator> _GetUnboundCriteria, CriteriaOperator _TopMostCriteria);
        private static CriteriaOperator GetUnboundCriteriaImpl(DataColumnInfoCollection _columns, OperandProperty prop);
        public static CriteriaOperator Process(CriteriaOperator op, DataColumnInfoCollection columns);
        public override CriteriaOperator Visit(OperandProperty theOperand);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly UnboundCriteriaInliner.<>c <>9;
            public static Func<Tuple<string, CriteriaOperator>, string> <>9__4_1;

            static <>c();
            internal string <Visit>b__4_1(Tuple<string, CriteriaOperator> step);
        }
    }
}

