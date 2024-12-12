namespace DevExpress.Data.Filtering
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class OperandParameter : OperandValue
    {
        private string parameterName;

        public OperandParameter();
        public OperandParameter(string parameterName);
        public OperandParameter(string parameterName, object value);
        public OperandParameter Clone();
        protected override CriteriaOperator CloneCommon();
        public override bool Equals(object obj);
        public override int GetHashCode();

        [XmlAttribute]
        public string ParameterName { get; set; }
    }
}

