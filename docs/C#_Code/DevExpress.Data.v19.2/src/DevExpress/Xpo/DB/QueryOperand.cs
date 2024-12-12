namespace DevExpress.Xpo.DB
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Utils;
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public sealed class QueryOperand : CriteriaOperator
    {
        [XmlAttribute]
        public string ColumnName;
        [XmlAttribute]
        public DBColumnType ColumnType;
        [XmlAttribute]
        public string NodeAlias;
        private static int HashSeed = HashCodeHelper.StartGeneric<string>(typeof(QueryOperand).Name);

        public QueryOperand() : this(null, null, DBColumnType.Unknown)
        {
        }

        public QueryOperand(DBColumn column, string nodeAlias) : this(column.Name, nodeAlias, column.ColumnType)
        {
        }

        public QueryOperand(string columnName, string nodeAlias) : this(columnName, nodeAlias, DBColumnType.Unknown)
        {
        }

        public QueryOperand(string columnName, string nodeAlias, DBColumnType columnType)
        {
            this.ColumnName = columnName;
            this.NodeAlias = nodeAlias;
            this.ColumnType = columnType;
        }

        public override void Accept(ICriteriaVisitor visitor)
        {
            ((IQueryCriteriaVisitor) visitor).Visit(this);
        }

        public override T Accept<T>(ICriteriaVisitor<T> visitor) => 
            ((IQueryCriteriaVisitor<T>) visitor).Visit(this);

        public QueryOperand Clone() => 
            new QueryOperand(this.ColumnName, this.NodeAlias, this.ColumnType);

        protected override CriteriaOperator CloneCommon() => 
            this.Clone();

        public override bool Equals(object obj)
        {
            QueryOperand criterion = obj as QueryOperand;
            return (!criterion.ReferenceEqualsNull() ? ((this.ColumnName == criterion.ColumnName) && (this.NodeAlias == criterion.NodeAlias)) : false);
        }

        public override int GetHashCode() => 
            HashCodeHelper.FinishGeneric<string, string>(HashSeed, this.NodeAlias, this.ColumnName);
    }
}

