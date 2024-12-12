namespace DevExpress.Xpo.DB
{
    using DevExpress.Data.Filtering;
    using DevExpress.Utils;
    using DevExpress.Xpo.DB.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Xml.Serialization;

    [Serializable]
    public class JoinNode
    {
        public JoinNodeCollection SubNodes;
        [XmlAttribute]
        public string Alias;
        [XmlAttribute, DefaultValue(0)]
        public JoinType Type;
        public DBTable Table;
        public CriteriaOperator Condition;

        public JoinNode()
        {
            this.SubNodes = new JoinNodeCollection();
        }

        public JoinNode(DBTable table, string alias, JoinType type)
        {
            this.SubNodes = new JoinNodeCollection();
            this.Type = type;
            this.Alias = alias;
            this.Table = table;
        }

        public void CollectJoinNodesAndCriteria(out List<JoinNode> nodes, out List<CriteriaOperator> criteria)
        {
            nodes = new List<JoinNode>();
            criteria = new List<CriteriaOperator>();
            this.CollectJoinNodesAndCriteriaInternal(nodes, criteria);
        }

        internal virtual void CollectJoinNodesAndCriteriaInternal(List<JoinNode> nodes, List<CriteriaOperator> criteria)
        {
            nodes.Add(this);
            criteria.Add(this.Condition);
            foreach (JoinNode node in SubQueriesFinder.FindSubQueries(this.Condition))
            {
                node.CollectJoinNodesAndCriteriaInternal(nodes, criteria);
            }
            foreach (JoinNode node2 in this.SubNodes)
            {
                node2.CollectJoinNodesAndCriteriaInternal(nodes, criteria);
            }
        }

        public override bool Equals(object obj)
        {
            JoinNode node = obj as JoinNode;
            return ((node != null) ? (Equals(this.Alias, node.Alias) && (Equals(this.Type, node.Type) && (Equals(this.Table, node.Table) && (Equals(this.Condition, node.Condition) && Equals(this.SubNodes, node.SubNodes))))) : false);
        }

        public DBColumn GetColumn(string name)
        {
            if (this.Table == null)
            {
                throw new InvalidOperationException();
            }
            return this.Table.GetColumn(name);
        }

        public override int GetHashCode() => 
            HashCodeHelper.CalculateGeneric<string, JoinType, DBTable, CriteriaOperator, JoinNodeCollection>(this.Alias, this.Type, this.Table, this.Condition, this.SubNodes);

        public override string ToString()
        {
            object[] args = new object[] { this.Type, this.Table, this.Alias, this.Condition, this.SubNodes };
            return string.Format(CultureInfo.InvariantCulture, "\t{0} join \"{1}\" {2} on {3}\n{4}", args);
        }
    }
}

