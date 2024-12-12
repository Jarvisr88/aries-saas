namespace Dapper
{
    using System;
    using System.Data;

    internal sealed class TableValuedParameter : SqlMapper.ICustomQueryParameter
    {
        private readonly DataTable table;
        private readonly string typeName;

        public TableValuedParameter(DataTable table) : this(table, null)
        {
        }

        public TableValuedParameter(DataTable table, string typeName)
        {
            this.table = table;
            this.typeName = typeName;
        }

        void SqlMapper.ICustomQueryParameter.AddParameter(IDbCommand command, string name)
        {
            IDbDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = name;
            Set(parameter, this.table, this.typeName);
            command.Parameters.Add(parameter);
        }

        internal static void Set(IDbDataParameter parameter, DataTable table, string typeName)
        {
            parameter.Value = SqlMapper.SanitizeParameterValue(table);
            if (string.IsNullOrEmpty(typeName) && (table != null))
            {
                typeName = table.GetTypeName();
            }
            if (!string.IsNullOrEmpty(typeName))
            {
                StructuredHelper.ConfigureTVP(parameter, typeName);
            }
        }
    }
}

