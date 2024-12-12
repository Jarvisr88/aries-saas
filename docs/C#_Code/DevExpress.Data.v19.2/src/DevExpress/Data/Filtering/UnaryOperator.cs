namespace DevExpress.Data.Filtering
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class UnaryOperator : CriteriaOperator
    {
        public CriteriaOperator Operand;
        [XmlAttribute]
        public UnaryOperatorType OperatorType;

        public UnaryOperator();
        public UnaryOperator(UnaryOperatorType operatorType, CriteriaOperator operand);
        public UnaryOperator(UnaryOperatorType operatorType, string propertyName);
        public override void Accept(ICriteriaVisitor visitor);
        public override T Accept<T>(ICriteriaVisitor<T> visitor);
        public UnaryOperator Clone();
        protected override CriteriaOperator CloneCommon();
        public override bool Equals(object obj);
        public override int GetHashCode();
    }
}

