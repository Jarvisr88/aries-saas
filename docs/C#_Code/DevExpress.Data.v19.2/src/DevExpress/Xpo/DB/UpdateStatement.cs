namespace DevExpress.Xpo.DB
{
    using DevExpress.Data.Filtering.Helpers;
    using System;

    [Serializable]
    public class UpdateStatement : ModificationStatement
    {
        public UpdateStatement()
        {
        }

        public UpdateStatement(DBTable table, string alias) : base(table, alias)
        {
        }

        public override bool Equals(object obj)
        {
            UpdateStatement statement = obj as UpdateStatement;
            return ((statement != null) ? (base.Equals(statement) && Equals(base.Parameters, statement.Parameters)) : false);
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        public override string ToString() => 
            $"update {base.Table.Name} set {base.Parameters.ToString()} where {base.Condition.ReferenceEqualsNull() ? string.Empty : base.Condition.ToString()}";
    }
}

