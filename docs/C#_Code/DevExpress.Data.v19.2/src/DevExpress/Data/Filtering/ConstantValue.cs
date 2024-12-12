namespace DevExpress.Data.Filtering
{
    using System;

    [Serializable]
    public class ConstantValue : OperandValue
    {
        public ConstantValue();
        public ConstantValue(object value);
        public ConstantValue Clone();
        protected override CriteriaOperator CloneCommon();
        public override bool Equals(object obj);
        public override int GetHashCode();
    }
}

