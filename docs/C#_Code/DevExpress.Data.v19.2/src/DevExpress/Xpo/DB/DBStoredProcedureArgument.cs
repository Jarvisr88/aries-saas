namespace DevExpress.Xpo.DB
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class DBStoredProcedureArgument : DBNameTypePair
    {
        [XmlAttribute]
        public DBStoredProcedureArgumentDirection Direction;

        public DBStoredProcedureArgument()
        {
        }

        public DBStoredProcedureArgument(string name, DBColumnType type) : base(name, type)
        {
        }

        public DBStoredProcedureArgument(string name, DBColumnType type, DBStoredProcedureArgumentDirection direction) : this(name, type)
        {
            this.Direction = direction;
        }

        public override string ToString() => 
            $"[{this.Direction.ToString().ToUpper()}] {base.Name} {base.Type}";
    }
}

