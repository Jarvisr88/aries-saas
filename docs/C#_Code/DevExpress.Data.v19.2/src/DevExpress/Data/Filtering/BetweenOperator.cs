namespace DevExpress.Data.Filtering
{
    using System;

    [Serializable]
    public sealed class BetweenOperator : CriteriaOperator
    {
        private CriteriaOperator testExpression;
        private CriteriaOperator beginExpression;
        private CriteriaOperator endExpression;

        public BetweenOperator();
        public BetweenOperator(CriteriaOperator testExpression, CriteriaOperator beginExpression, CriteriaOperator endExpression);
        public BetweenOperator(string testPropertyName, CriteriaOperator beginExpression, CriteriaOperator endExpression);
        public BetweenOperator(string testPropertyName, object beginValue, object endValue);
        public override void Accept(ICriteriaVisitor visitor);
        public override T Accept<T>(ICriteriaVisitor<T> visitor);
        public BetweenOperator Clone();
        protected override CriteriaOperator CloneCommon();
        public override bool Equals(object obj);
        public override int GetHashCode();

        public CriteriaOperator BeginExpression { get; set; }

        public CriteriaOperator EndExpression { get; set; }

        public CriteriaOperator TestExpression { get; set; }

        [Obsolete("Use BeginExpression property instead")]
        public CriteriaOperator LeftOperand { get; set; }

        [Obsolete("Use EndExpression property instead")]
        public CriteriaOperator RightOperand { get; set; }

        [Obsolete("Use TestExpression property instead")]
        public CriteriaOperator Property { get; set; }
    }
}

