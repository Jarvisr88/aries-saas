namespace DevExpress.Xpo.DB
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class DBStoredProcedureResultSet
    {
        private List<DBNameTypePair> columns;

        public DBStoredProcedureResultSet()
        {
            this.columns = new List<DBNameTypePair>();
        }

        public DBStoredProcedureResultSet(ICollection<DBNameTypePair> columns)
        {
            this.columns = new List<DBNameTypePair>();
            foreach (DBNameTypePair pair in columns)
            {
                this.columns.Add(pair);
            }
        }

        public override string ToString() => 
            (this.columns.Count != 1) ? $"{this.columns.Count} columns" : "1 column";

        [XmlArrayItem(typeof(DBNameTypePair))]
        public List<DBNameTypePair> Columns =>
            this.columns;
    }
}

