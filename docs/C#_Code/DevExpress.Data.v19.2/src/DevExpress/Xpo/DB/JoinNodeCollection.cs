namespace DevExpress.Xpo.DB
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Text;

    [Serializable]
    public sealed class JoinNodeCollection : List<JoinNode>
    {
        public override bool Equals(object obj)
        {
            JoinNodeCollection nodes = obj as JoinNodeCollection;
            if (nodes == null)
            {
                return false;
            }
            if (base.Count != nodes.Count)
            {
                return false;
            }
            for (int i = 0; i < base.Count; i++)
            {
                if (!Equals(base[i], nodes[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode() => 
            HashCodeHelper.CalculateGenericList<JoinNode>((IList<JoinNode>) this);

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (JoinNode node in this)
            {
                builder.Append(node.ToString());
            }
            return builder.ToString();
        }
    }
}

