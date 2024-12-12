namespace DevExpress.Data.Filtering
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class OperandValue : CriteriaOperator
    {
        private object value;

        public OperandValue();
        public OperandValue(object value);
        public override void Accept(ICriteriaVisitor visitor);
        public override T Accept<T>(ICriteriaVisitor<T> visitor);
        public OperandValue Clone();
        protected override CriteriaOperator CloneCommon();
        public override bool Equals(object obj);
        internal static string FormatString(string value);
        public override int GetHashCode();
        protected virtual object GetXmlValue();
        public static explicit operator OperandValue(bool val);
        public static implicit operator OperandValue(byte val);
        public static implicit operator OperandValue(char val);
        public static implicit operator OperandValue(DateTime val);
        public static implicit operator OperandValue(decimal val);
        public static implicit operator OperandValue(double val);
        public static implicit operator OperandValue(Guid val);
        public static implicit operator OperandValue(short val);
        public static implicit operator OperandValue(int val);
        public static implicit operator OperandValue(long val);
        public static implicit operator OperandValue(float val);
        public static implicit operator OperandValue(string val);
        public static implicit operator OperandValue(TimeSpan val);
        public static implicit operator OperandValue(byte[] val);
        internal static string ReformatString(string value);

        [XmlElement(typeof(bool)), XmlElement(typeof(byte)), XmlElement(typeof(sbyte)), XmlElement(typeof(char)), XmlElement(typeof(decimal)), XmlElement(typeof(double)), XmlElement(typeof(float)), XmlElement(typeof(int)), XmlElement(typeof(uint)), XmlElement(typeof(long)), XmlElement(typeof(ulong)), XmlElement(typeof(short)), XmlElement(typeof(ushort)), XmlElement(typeof(Guid)), XmlElement(typeof(string)), XmlElement(typeof(DateTime)), XmlElement(typeof(TimeSpan)), XmlElement(typeof(NullValue)), XmlElement(typeof(byte[]))]
        public virtual object XmlValue { get; set; }

        [XmlIgnore]
        public virtual object Value { get; set; }
    }
}

