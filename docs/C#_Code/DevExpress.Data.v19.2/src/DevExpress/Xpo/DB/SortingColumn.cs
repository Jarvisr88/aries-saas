namespace DevExpress.Xpo.DB
{
    using DevExpress.Data.Filtering;
    using DevExpress.Utils;
    using System;

    [Serializable]
    public sealed class SortingColumn
    {
        private SortingDirection direction;
        private CriteriaOperator property;

        public SortingColumn() : this(null, null, SortingDirection.Ascending)
        {
        }

        public SortingColumn(CriteriaOperator property, SortingDirection direction)
        {
            this.property = property;
            this.direction = direction;
        }

        public SortingColumn(string columnName, string nodeAlias, SortingDirection direction) : this(new QueryOperand(columnName, nodeAlias), direction)
        {
        }

        public override bool Equals(object obj)
        {
            SortingColumn column = obj as SortingColumn;
            return ((column != null) ? (Equals(this.Property, column.Property) && (this.Direction == column.Direction)) : false);
        }

        public override int GetHashCode() => 
            HashCodeHelper.CalculateGeneric<CriteriaOperator, SortingDirection>(this.Property, this.Direction);

        public SortingDirection Direction
        {
            get => 
                this.direction;
            set => 
                this.direction = value;
        }

        public CriteriaOperator Property
        {
            get => 
                this.property;
            set => 
                this.property = value;
        }
    }
}

