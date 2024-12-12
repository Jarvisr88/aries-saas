namespace DevExpress.Xpo.DB
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Xpo.DB.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Text;

    [Serializable]
    public class SelectStatement : BaseStatement
    {
        public int SkipSelectedRecords;
        public int TopSelectedRecords;
        private QuerySortingCollection sortProperties;
        private CriteriaOperatorCollection groupProperties;
        public CriteriaOperator GroupCondition;

        public SelectStatement()
        {
        }

        public SelectStatement(DBTable table, string alias) : base(table, alias)
        {
        }

        internal override void CollectJoinNodesAndCriteriaInternal(List<JoinNode> nodes, List<CriteriaOperator> criteria)
        {
            base.CollectJoinNodesAndCriteriaInternal(nodes, criteria);
            foreach (SortingColumn column in this.SortProperties)
            {
                criteria.Add(column.Property);
                foreach (JoinNode node in SubQueriesFinder.FindSubQueries(column.Property))
                {
                    node.CollectJoinNodesAndCriteriaInternal(nodes, criteria);
                }
            }
            foreach (CriteriaOperator @operator in this.GroupProperties)
            {
                criteria.Add(@operator);
                foreach (JoinNode node2 in SubQueriesFinder.FindSubQueries(@operator))
                {
                    node2.CollectJoinNodesAndCriteriaInternal(nodes, criteria);
                }
            }
            criteria.Add(this.GroupCondition);
            foreach (JoinNode node3 in SubQueriesFinder.FindSubQueries(this.GroupCondition))
            {
                node3.CollectJoinNodesAndCriteriaInternal(nodes, criteria);
            }
        }

        public override bool Equals(object obj)
        {
            SelectStatement statement = obj as SelectStatement;
            return ((statement != null) ? (base.Equals(statement) && ((this.SkipSelectedRecords == statement.SkipSelectedRecords) && ((this.TopSelectedRecords == statement.TopSelectedRecords) && (Equals(this.SortProperties, statement.SortProperties) && (Equals(this.GroupProperties, statement.GroupProperties) && Equals(this.GroupCondition, statement.GroupCondition)))))) : false);
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder("Select ");
            if (this.SkipSelectedRecords != 0)
            {
                builder.Append("skip(" + this.SkipSelectedRecords.ToString() + ") ");
            }
            if (this.TopSelectedRecords != 0)
            {
                builder.Append("top(" + this.TopSelectedRecords.ToString() + ") ");
            }
            builder.Append(base.Operands.ToString());
            string[] textArray1 = new string[] { "\n  from \"", base.Table.ToString(), "\" ", base.Alias, "\n", base.SubNodes.ToString() };
            builder.Append(string.Concat(textArray1));
            if (!base.Condition.ReferenceEqualsNull())
            {
                builder.Append(" where " + CriteriaOperator.ToString(base.Condition));
            }
            if (this.GroupProperties.Count > 0)
            {
                builder.Append(" group by " + this.GroupProperties.ToString() + "\n");
            }
            if (!this.GroupCondition.ReferenceEqualsNull())
            {
                builder.Append("having " + CriteriaOperator.ToString(this.GroupCondition) + "\n");
            }
            return builder.ToString();
        }

        public QuerySortingCollection SortProperties
        {
            get
            {
                this.sortProperties ??= new QuerySortingCollection();
                return this.sortProperties;
            }
        }

        public CriteriaOperatorCollection GroupProperties
        {
            get
            {
                this.groupProperties ??= new CriteriaOperatorCollection();
                return this.groupProperties;
            }
        }
    }
}

