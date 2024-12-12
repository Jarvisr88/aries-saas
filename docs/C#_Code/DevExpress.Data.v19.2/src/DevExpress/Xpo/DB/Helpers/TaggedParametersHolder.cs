namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Xpo.DB;
    using System;
    using System.Collections.Generic;

    public class TaggedParametersHolder
    {
        private Dictionary<int, ParameterValue> parametersByTag = new Dictionary<int, ParameterValue>();

        public void ConsolidateIdentity(ParameterValue identityInsertParameter)
        {
            OperandValue value2 = this.ConsolidateParameter(identityInsertParameter);
        }

        public OperandValue ConsolidateParameter(OperandValue deserializedParameter)
        {
            ParameterValue value2;
            ParameterValue value3;
            if (!deserializedParameter.Is<ParameterValue>(out value2))
            {
                return deserializedParameter;
            }
            if (this.parametersByTag.TryGetValue(value2.Tag, out value3))
            {
                return value3;
            }
            this.parametersByTag.Add(value2.Tag, value2);
            return value2;
        }
    }
}

