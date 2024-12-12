namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Xpo.DB;
    using System;
    using System.Collections.Generic;

    public class TopLevelQueryOperandsFinder : IQueryCriteriaVisitor, ICriteriaVisitor
    {
        private List<QueryOperand> result = new List<QueryOperand>();

        private TopLevelQueryOperandsFinder()
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
            this.result.Add(theOperand);
        }

        void IQueryCriteriaVisitor.Visit(QuerySubQueryContainer theOperand)
        {
            this.Process(theOperand.AggregateProperty);
        }

        public static List<QueryOperand> Find(CriteriaOperator criteria)
        {
            TopLevelQueryOperandsFinder finder = new TopLevelQueryOperandsFinder();
            finder.Process(criteria);
            return finder.result;
        }

        public static List<QueryOperand> Find(IEnumerable<CriteriaOperator> criteria)
        {
            TopLevelQueryOperandsFinder finder = new TopLevelQueryOperandsFinder();
            foreach (CriteriaOperator @operator in criteria)
            {
                finder.Process(@operator);
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

