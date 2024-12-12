namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Xpo.DB;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public sealed class Query
    {
        private string sqlString;
        private QueryParameterCollection parameters;
        private IList parametersNames;
        private int skipSelectedRecords;
        private int topSelectedRecords;
        private Dictionary<int, OperandValue> constantValues;
        private Dictionary<int, int> operandIndexes;

        public Query(string sql) : this(sql, new QueryParameterCollection(), null)
        {
        }

        public Query(string sql, QueryParameterCollection parameters, IList parametersNames) : this(sql, parameters, parametersNames, 0, 0)
        {
        }

        public Query(string sql, QueryParameterCollection parameters, IList parametersNames, int topSelectedRecords) : this(sql, parameters, parametersNames, 0, topSelectedRecords)
        {
        }

        public Query(string sql, QueryParameterCollection parameters, IList parametersNames, int skipSelectedRecords, int topSelectedRecords)
        {
            this.sqlString = sql;
            this.parameters = parameters;
            this.parametersNames = parametersNames;
            this.skipSelectedRecords = skipSelectedRecords;
            this.topSelectedRecords = topSelectedRecords;
        }

        public Query(string sql, QueryParameterCollection parameters, IList parametersNames, int topSelectedRecords, Dictionary<int, OperandValue> constantValues, Dictionary<int, int> operandIndexes) : this(sql, parameters, parametersNames, 0, topSelectedRecords, constantValues, operandIndexes)
        {
        }

        public Query(string sql, QueryParameterCollection parameters, IList parametersNames, int skipSelectedRecords, int topSelectedRecords, Dictionary<int, OperandValue> constantValues, Dictionary<int, int> operandIndexes) : this(sql, parameters, parametersNames, skipSelectedRecords, topSelectedRecords)
        {
            this.constantValues = constantValues;
            this.operandIndexes = operandIndexes;
        }

        public Dictionary<int, OperandValue> ConstantValues =>
            this.constantValues;

        public Dictionary<int, int> OperandIndexes =>
            this.operandIndexes;

        public string Sql =>
            this.sqlString;

        public QueryParameterCollection Parameters =>
            this.parameters;

        public IList ParametersNames =>
            this.parametersNames;

        public int SkipSelectedRecords =>
            this.skipSelectedRecords;

        public int TopSelectedRecords =>
            this.topSelectedRecords;
    }
}

