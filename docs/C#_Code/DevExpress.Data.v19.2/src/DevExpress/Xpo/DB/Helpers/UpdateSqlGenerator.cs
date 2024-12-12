namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Xpo.DB;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    public sealed class UpdateSqlGenerator : BaseObjectSqlGenerator
    {
        public UpdateSqlGenerator(ISqlGeneratorFormatter formatter, TaggedParametersHolder identities, Dictionary<OperandValue, string> parameters) : base(formatter, identities, parameters)
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
                return null;
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < base.Root.Operands.Count; i++)
            {
                object[] args = new object[] { base.Process(base.Root.Operands[i]), this.GetNextParameterName(((UpdateStatement) base.Root).Parameters[i]) };
                builder.AppendFormat(CultureInfo.InvariantCulture, "{0}={1},", args);
            }
            builder.Remove(builder.Length - 1, 1);
            return base.formatter.FormatUpdate(base.formatter.FormatTable(base.formatter.ComposeSafeSchemaName(base.Root.Table.Name), base.formatter.ComposeSafeTableName(base.Root.Table.Name)), builder.ToString(), base.BuildCriteria());
        }
    }
}

