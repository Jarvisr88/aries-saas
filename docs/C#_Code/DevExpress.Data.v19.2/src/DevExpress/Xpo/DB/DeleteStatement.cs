namespace DevExpress.Xpo.DB
{
    using DevExpress.Data.Filtering.Helpers;
    using System;

    [Serializable]
    public class DeleteStatement : ModificationStatement
    {
        public DeleteStatement()
        {
        }

        public DeleteStatement(DBTable table, string alias) : base(table, alias)
        {
        }

        public override bool Equals(object obj)
        {
            DeleteStatement statement = obj as DeleteStatement;
            return ((statement != null) ? base.Equals(statement) : false);
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        public override string ToString() => 
            $"delete from {base.Table.Name} where {base.Condition.ReferenceEqualsNull() ? string.Empty : base.Condition.ToString()}";
    }
}

