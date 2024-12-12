namespace DevExpress.Xpo.DB
{
    using DevExpress.Data.Filtering;
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [Serializable, XmlInclude(typeof(InsertStatement)), XmlInclude(typeof(DeleteStatement)), XmlInclude(typeof(UpdateStatement))]
    public abstract class ModificationStatement : BaseStatement
    {
        [XmlArrayItem(typeof(OperandValue)), XmlArrayItem(typeof(ConstantValue)), XmlArrayItem(typeof(ParameterValue))]
        public QueryParameterCollection Parameters;
        [XmlAttribute, DefaultValue(0)]
        public int RecordsAffected;

        protected ModificationStatement()
        {
            this.Parameters = new QueryParameterCollection();
        }

        protected ModificationStatement(DBTable table, string alias) : base(table, alias)
        {
            this.Parameters = new QueryParameterCollection();
        }

        public override string ToString() => 
            base.GetType().ToString();
    }
}

