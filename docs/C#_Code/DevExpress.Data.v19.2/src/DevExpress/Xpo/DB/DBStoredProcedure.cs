namespace DevExpress.Xpo.DB
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class DBStoredProcedure
    {
        [XmlAttribute]
        public string Name;
        private List<DBStoredProcedureArgument> arguments = new List<DBStoredProcedureArgument>();
        private List<DBStoredProcedureResultSet> resultSets = new List<DBStoredProcedureResultSet>();

        public override string ToString() => 
            this.Name;

        [XmlArrayItem(typeof(DBStoredProcedureArgument))]
        public List<DBStoredProcedureArgument> Arguments =>
            this.arguments;

        [XmlArrayItem(typeof(DBStoredProcedureResultSet))]
        public List<DBStoredProcedureResultSet> ResultSets =>
            this.resultSets;
    }
}

