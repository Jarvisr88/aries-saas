namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Xpo.DB;
    using System;
    using System.Collections.Generic;

    public class SubQueriesFinder : IQueryCriteriaVisitor, ICriteriaVisitor
    {
        private JoinNodeCollection result = new JoinNodeCollection();

        private SubQueriesFinder()
        {
        }

        void ICriteriaVisitor.Visit(BetweenOperator theOperator)
        {
            this.Process(theOperator.TestExpression);
            this.Process(theOperator.BeginExpression);
            this.Process(theOperator.EndExpression);
        }

        void ICriteriaVisitor.Visit(BinaryOperator theOperator)
        {
            this.Process(theOperator.LeftOperand);
            this.Process(theOperator.RightOperand);
        }

        void ICriteriaVisitor.Visit(FunctionOperator theOperator)
        {
            this.Process(theOperator.Operands);
        }

        void ICriteriaVisitor.Visit(GroupOperator theOperator)
        {
            this.Process(theOperator.Operands);
        }

        void ICriteriaVisitor.Visit(InOperator theOperator)
        {
            this.Process(theOperator.LeftOperand);
            this.Process(theOperator.Operands);
        }

        void ICriteriaVisitor.Visit(OperandValue theOperand)
        {
        }

        void ICriteriaVisitor.Visit(UnaryOperator theOperator)
        {
            this.Process(theOperator.Operand);
        }

        void IQueryCriteriaVisitor.Visit(QueryOperand theOperand)
        {
        }

        void IQueryCriteriaVisitor.Visit(QuerySubQueryContainer theOperand)
        {
            if (theOperand.Node != null)
            {
                this.result.Add(theOperand.Node);
            }
            else
            {
                this.Process(theOperand.AggregateProperty);
            }
        }

        public static JoinNodeCollection FindSubQueries(CriteriaOperator criteria)
        {
            SubQueriesFinder finder = new SubQueriesFinder();
            finder.Process(criteria);
            return finder.result;
        }

        public static JoinNodeCollection FindSubQueries(CriteriaOperatorCollection criterias)
        {
            SubQueriesFinder finder = new SubQueriesFinder();
            foreach (CriteriaOperator @operator in criterias)
            {
                finder.Process(@operator);
            }
            return finder.result;
        }

        public static JoinNodeCollection FindSubQueries(QuerySortingCollection sortings)
        {
            SubQueriesFinder finder = new SubQueriesFinder();
            foreach (SortingColumn column in sortings)
            {
                finder.Process(column.Property);
            }
            return finder.result;
        }

        private void Process(CriteriaOperator criteria)
        {
            if (!criteria.ReferenceEqualsNull())
            {
                criteria.Accept(this);
            }
        }

        private void Process(IEnumerable<CriteriaOperator> criterias)
        {
            if (criterias != null)
            {
                foreach (CriteriaOperator @operator in criterias)
                {
                    this.Process(@operator);
                }
            }
        }
    }
}

