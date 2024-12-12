namespace DevExpress.Xpo.DB
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class InsertStatement : ModificationStatement
    {
        public ParameterValue IdentityParameter;
        [XmlAttribute]
        public string IdentityColumn;
        [XmlAttribute]
        public DBColumnType IdentityColumnType;

        public InsertStatement()
        {
        }

        public InsertStatement(DBTable table, string alias) : base(table, alias)
        {
        }

        public override bool Equals(object obj)
        {
            InsertStatement statement = obj as InsertStatement;
            return ((statement != null) ? (base.Equals(statement) && ((this.IdentityColumn == statement.IdentityColumn) && ((this.IdentityColumnType == statement.IdentityColumnType) && Equals(base.Parameters, statement.Parameters)))) : false);
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        public override string ToString() => 
            $"insert into {base.Table.Name} values ({base.Parameters.ToString()})";
    }
}

