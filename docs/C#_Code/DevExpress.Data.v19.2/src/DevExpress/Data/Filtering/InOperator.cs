namespace DevExpress.Data.Filtering
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class InOperator : CriteriaOperator
    {
        private CriteriaOperator leftOperand;
        private CriteriaOperatorCollection operands;

        public InOperator();
        public InOperator(CriteriaOperator leftOperand, IEnumerable<CriteriaOperator> operands);
        public InOperator(CriteriaOperator leftOperand, params CriteriaOperator[] operands);
        public InOperator(string propertyName, IEnumerable values);
        public InOperator(string propertyName, params CriteriaOperator[] operands);
        public override void Accept(ICriteriaVisitor visitor);
        public override T Accept<T>(ICriteriaVisitor<T> visitor);
        public InOperator Clone();
        protected override CriteriaOperator CloneCommon();
        public override bool Equals(object obj);
        public override int GetHashCode();

        public CriteriaOperator LeftOperand { get; set; }

        [XmlArrayItem(typeof(CriteriaOperator))]
        public virtual CriteriaOperatorCollection Operands { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly InOperator.<>c <>9;
            public static Func<object, CriteriaOperator> <>9__10_0;

            static <>c();
            internal CriteriaOperator <.ctor>b__10_0(object val);
        }
    }
}

