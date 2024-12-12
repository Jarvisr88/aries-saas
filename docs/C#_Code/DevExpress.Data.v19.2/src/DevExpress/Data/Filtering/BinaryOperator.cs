namespace DevExpress.Data.Filtering
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class BinaryOperator : CriteriaOperator
    {
        [XmlAttribute]
        public BinaryOperatorType OperatorType;
        public CriteriaOperator LeftOperand;
        public CriteriaOperator RightOperand;

        public BinaryOperator();
        public BinaryOperator(string propertyName, bool value);
        public BinaryOperator(string propertyName, byte value);
        public BinaryOperator(string propertyName, char value);
        public BinaryOperator(string propertyName, DateTime value);
        public BinaryOperator(string propertyName, decimal value);
        public BinaryOperator(string propertyName, double value);
        public BinaryOperator(string propertyName, Guid value);
        public BinaryOperator(string propertyName, short value);
        public BinaryOperator(string propertyName, int value);
        public BinaryOperator(string propertyName, long value);
        public BinaryOperator(string propertyName, float value);
        public BinaryOperator(string propertyName, TimeSpan value);
        public BinaryOperator(string propertyName, byte[] value);
        public BinaryOperator(string propertyName, object value);
        public BinaryOperator(string propertyName, string value);
        public BinaryOperator(CriteriaOperator opLeft, CriteriaOperator opRight, BinaryOperatorType type);
        public BinaryOperator(string propertyName, bool value, BinaryOperatorType type);
        public BinaryOperator(string propertyName, byte value, BinaryOperatorType type);
        public BinaryOperator(string propertyName, char value, BinaryOperatorType type);
        public BinaryOperator(string propertyName, DateTime value, BinaryOperatorType type);
        public BinaryOperator(string propertyName, decimal value, BinaryOperatorType type);
        public BinaryOperator(string propertyName, double value, BinaryOperatorType type);
        public BinaryOperator(string propertyName, Guid value, BinaryOperatorType type);
        public BinaryOperator(string propertyName, short value, BinaryOperatorType type);
        public BinaryOperator(string propertyName, int value, BinaryOperatorType type);
        public BinaryOperator(string propertyName, long value, BinaryOperatorType type);
        public BinaryOperator(string propertyName, float value, BinaryOperatorType type);
        public BinaryOperator(string propertyName, string value, BinaryOperatorType type);
        public BinaryOperator(string propertyName, byte[] value, BinaryOperatorType type);
        public BinaryOperator(string propertyName, object value, BinaryOperatorType type);
        public BinaryOperator(string propertyName, TimeSpan value, BinaryOperatorType type);
        public override void Accept(ICriteriaVisitor visitor);
        public override T Accept<T>(ICriteriaVisitor<T> visitor);
        public BinaryOperator Clone();
        protected override CriteriaOperator CloneCommon();
        public override bool Equals(object obj);
        public override int GetHashCode();
    }
}

