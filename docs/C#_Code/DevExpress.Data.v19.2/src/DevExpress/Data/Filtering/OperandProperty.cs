namespace DevExpress.Data.Filtering
{
    using System;
    using System.Reflection;
    using System.Xml.Serialization;

    [Serializable]
    public class OperandProperty : CriteriaOperator
    {
        private string propertyName;

        public OperandProperty();
        public OperandProperty(string propertyName);
        public override void Accept(ICriteriaVisitor visitor);
        public override T Accept<T>(ICriteriaVisitor<T> visitor);
        public OperandProperty Clone();
        protected override CriteriaOperator CloneCommon();
        public override bool Equals(object obj);
        public override int GetHashCode();

        [XmlAttribute]
        public string PropertyName { get; set; }

        public AggregateOperand this[CriteriaOperator condition] { get; }
    }
}

