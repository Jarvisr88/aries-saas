namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Xpo.DB;
    using System;
    using System.Collections.Generic;

    public abstract class BaseObjectSqlGenerator : BaseSqlGeneratorWithParameters
    {
        protected BaseObjectSqlGenerator(ISqlGeneratorFormatter formatter, TaggedParametersHolder identities, Dictionary<OperandValue, string> parameters) : base(formatter, identities, parameters)
        {
        }

        public QueryCollection GenerateSql(ModificationStatement[] dmlStatements)
        {
            QueryCollection querys = new QueryCollection();
            foreach (ModificationStatement statement in dmlStatements)
            {
                Query item = base.GenerateSql(statement);
                if (item.Sql != null)
                {
                    querys.Add(item);
                }
            }
            return querys;
        }
    }
}

