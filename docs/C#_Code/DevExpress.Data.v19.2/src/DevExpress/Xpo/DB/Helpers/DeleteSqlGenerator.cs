namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Xpo.DB;
    using System;
    using System.Collections.Generic;

    public sealed class DeleteSqlGenerator : BaseObjectSqlGenerator
    {
        public DeleteSqlGenerator(ISqlGeneratorFormatter formatter, TaggedParametersHolder identities, Dictionary<OperandValue, string> parameters) : base(formatter, identities, parameters)
        {
        }

        protected override string InternalGenerateSql()
        {
            if (base.Root.Table is DBProjection)
            {
                throw new InvalidOperationException();
            }
            return base.formatter.FormatDelete(base.formatter.FormatTable(base.formatter.ComposeSafeSchemaName(base.Root.Table.Name), base.formatter.ComposeSafeTableName(base.Root.Table.Name)), base.BuildCriteria());
        }
    }
}

