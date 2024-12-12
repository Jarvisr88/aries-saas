namespace DevExpress.Xpo.DB
{
    using DevExpress.Data.Filtering;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Text;

    [Serializable]
    public sealed class QueryParameterCollection : List<OperandValue>
    {
        public QueryParameterCollection()
        {
        }

        public QueryParameterCollection(params OperandValue[] parameters)
        {
            base.AddRange(parameters);
        }

        public override bool Equals(object obj)
        {
            QueryParameterCollection parameters = obj as QueryParameterCollection;
            if (parameters == null)
            {
                return false;
            }
            if (base.Count != parameters.Count)
            {
                return false;
            }
            for (int i = 0; i < base.Count; i++)
            {
                if (!Equals(base[i], parameters[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode() => 
            HashCodeHelper.CalculateGenericList<OperandValue>((IList<OperandValue>) this);

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (object obj2 in this)
            {
                if (builder.Length > 0)
                {
                    builder.Append(", ");
                }
                builder.Append(obj2.ToString());
            }
            return builder.ToString();
        }
    }
}

