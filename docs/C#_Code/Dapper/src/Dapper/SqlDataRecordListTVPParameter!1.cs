namespace Dapper
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    internal sealed class SqlDataRecordListTVPParameter<T> : SqlMapper.ICustomQueryParameter where T: IDataRecord
    {
        private readonly IEnumerable<T> data;
        private readonly string typeName;

        public SqlDataRecordListTVPParameter(IEnumerable<T> data, string typeName)
        {
            this.data = data;
            this.typeName = typeName;
        }

        void SqlMapper.ICustomQueryParameter.AddParameter(IDbCommand command, string name)
        {
            IDbDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = name;
            SqlDataRecordListTVPParameter<T>.Set(parameter, this.data, this.typeName);
            command.Parameters.Add(parameter);
        }

        internal static void Set(IDbDataParameter parameter, IEnumerable<T> data, string typeName)
        {
            parameter.Value = ((data == null) || !data.Any<T>()) ? null : data;
            StructuredHelper.ConfigureTVP(parameter, typeName);
        }
    }
}

