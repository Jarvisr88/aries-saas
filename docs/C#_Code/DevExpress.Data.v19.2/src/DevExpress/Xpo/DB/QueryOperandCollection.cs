namespace DevExpress.Xpo.DB
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    [Serializable]
    public sealed class QueryOperandCollection : List<QueryOperand>
    {
        public override bool Equals(object obj)
        {
            QueryOperandCollection operands = obj as QueryOperandCollection;
            if (operands == null)
            {
                return false;
            }
            if (base.Count != operands.Count)
            {
                return false;
            }
            for (int i = 0; i < base.Count; i++)
            {
                if (!Equals(base[i], operands[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode() => 
            HashCodeHelper.CalculateGenericList<QueryOperand>((IList<QueryOperand>) this);
    }
}

