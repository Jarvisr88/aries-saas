namespace DevExpress.Data.Filtering
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class FunctionOperator : CriteriaOperator
    {
        private CriteriaOperatorCollection operands;
        [XmlAttribute]
        public FunctionOperatorType OperatorType;

        public FunctionOperator();
        public FunctionOperator(FunctionOperatorType type, IEnumerable<CriteriaOperator> operands);
        public FunctionOperator(FunctionOperatorType type, params CriteriaOperator[] operands);
        public FunctionOperator(string customFunctionName, IEnumerable<CriteriaOperator> operands);
        public FunctionOperator(string customFunctionName, params CriteriaOperator[] operands);
        public override void Accept(ICriteriaVisitor visitor);
        public override T Accept<T>(ICriteriaVisitor<T> visitor);
        public FunctionOperator Clone();
        protected override CriteriaOperator CloneCommon();
        internal static CriteriaOperator CreateNormalized(FunctionOperatorType opType, IEnumerable<CriteriaOperator> operands);
        private static CriteriaOperator CreateNormalizedIifCore(IList<CriteriaOperator> operands);
        public override bool Equals(object obj);
        public override int GetHashCode();
        public static bool GuessIsLogicalCustomFunction(FunctionOperator theOperator);
        internal static CriteriaOperator NormalizeConditional(FunctionOperator fop);

        [XmlArrayItem(typeof(CriteriaOperator))]
        public CriteriaOperatorCollection Operands { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FunctionOperator.<>c <>9;
            public static Predicate<FunctionOperator> <>9__17_0;

            static <>c();
            internal bool <CreateNormalizedIifCore>b__17_0(FunctionOperator f);
        }
    }
}

