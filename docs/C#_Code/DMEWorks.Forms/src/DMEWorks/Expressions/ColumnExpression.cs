namespace DMEWorks.Expressions
{
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class ColumnExpression : PrimitiveExpression
    {
        public ColumnExpression(string columnName, System.Type type)
        {
            this.<ColumnName>k__BackingField = columnName;
            if (type == null)
            {
                System.Type local1 = type;
                throw new ArgumentNullException("type");
            }
            this.<Type>k__BackingField = type;
        }

        public string ColumnName { get; }

        public override System.Type Type { get; }
    }
}

