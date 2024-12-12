namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpo.DB;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public abstract class BaseSqlGeneratorWithParameters : BaseSqlGenerator
    {
        private QueryParameterCollection queryParams;
        private List<string> queryParamsNames;
        private TaggedParametersHolder identitiesByTag;
        private Dictionary<OperandValue, string> parameters;

        protected BaseSqlGeneratorWithParameters(ISqlGeneratorFormatter formatter, TaggedParametersHolder identitiesByTag, Dictionary<OperandValue, string> parameters) : base(formatter)
        {
            this.identitiesByTag = identitiesByTag;
            this.parameters = parameters;
        }

        protected virtual Query CreateQuery(string sql, QueryParameterCollection parameters, IList parametersNames) => 
            new Query(sql, parameters, parametersNames);

        public Query GenerateSql(BaseStatement node)
        {
            base.SetUpRootQueryStatement(node);
            this.SetUpParameters();
            return this.CreateQuery(this.InternalGenerateSql(), this.queryParams, this.queryParamsNames);
        }

        public override string GetNextParameterName(OperandValue parameter)
        {
            string str;
            parameter = this.identitiesByTag.ConsolidateParameter(parameter);
            if (parameter.Value == null)
            {
                return "null";
            }
            if (!base.formatter.SupportNamedParameters)
            {
                bool createParameter = true;
                string item = base.formatter.GetParameterName(parameter, this.queryParams.Count, ref createParameter);
                if (createParameter)
                {
                    this.queryParams.Add(parameter);
                    this.queryParamsNames.Add(item);
                }
                return item;
            }
            if (!this.parameters.TryGetValue(parameter, out str))
            {
                bool createParameter = true;
                str = base.formatter.GetParameterName(parameter, this.parameters.Count, ref createParameter);
                if (createParameter)
                {
                    this.parameters.Add(parameter, str);
                    this.queryParams.Add(parameter);
                    this.queryParamsNames.Add(str);
                }
            }
            return str;
        }

        protected abstract string InternalGenerateSql();
        private void SetUpParameters()
        {
            this.queryParams = new QueryParameterCollection();
            this.queryParamsNames = new List<string>(0);
        }
    }
}

