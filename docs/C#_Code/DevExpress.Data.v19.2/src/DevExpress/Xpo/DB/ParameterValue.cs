namespace DevExpress.Xpo.DB
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Utils;
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public sealed class ParameterValue : OperandValue
    {
        [XmlAttribute]
        public int Tag;

        public ParameterValue()
        {
        }

        public ParameterValue(int tag)
        {
            this.Tag = tag;
        }

        public override bool Equals(object obj)
        {
            ParameterValue criterion = obj as ParameterValue;
            return (!criterion.ReferenceEqualsNull() ? ((this.Tag == criterion.Tag) && base.Equals(criterion)) : false);
        }

        public override int GetHashCode() => 
            HashCodeHelper.Finish(base.GetHashCode(), this.Tag);
    }
}

