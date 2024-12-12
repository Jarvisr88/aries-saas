namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Xpo.DB;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class InsertSqlGenerator : BaseObjectSqlGenerator
    {
        public InsertSqlGenerator(ISqlGeneratorFormatter formatter, TaggedParametersHolder identities, Dictionary<OperandValue, string> parameters) : base(formatter, identities, parameters)
        {
        }

        protected override string InternalGenerateSql()
        {
            if (base.Root.Table is DBProjection)
            {
                throw new InvalidOperationException();
            }
            if (base.Root.Operands.Count == 0)
            {
                return base.formatter.FormatInsertDefaultValues(base.formatter.FormatTable(base.formatter.ComposeSafeSchemaName(base.Root.Table.Name), base.formatter.ComposeSafeTableName(base.Root.Table.Name)));
            }
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            for (int i = 0; i < base.Root.Operands.Count; i++)
            {
                builder.Append(base.Process(base.Root.Operands[i]));
                builder.Append(",");
                builder2.Append(this.GetNextParameterName(((InsertStatement) base.Root).Parameters[i]));
                builder2.Append(",");
            }
            return base.formatter.FormatInsert(base.formatter.FormatTable(base.formatter.ComposeSafeSchemaName(base.Root.Table.Name), base.formatter.ComposeSafeTableName(base.Root.Table.Name)), builder.ToString(0, builder.Length - 1), builder2.ToString(0, builder2.Length - 1));
        }
    }
}

