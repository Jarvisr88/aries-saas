namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;

    public class SearchPanelHighlightingResolver : IClientCriteriaVisitor<List<string>>, ICriteriaVisitor<List<string>>
    {
        private List<string> cache = new List<string>();

        private string ExtractOperandValue(OperandValue theOperand) => 
            theOperand.Value?.ToString();

        public List<string> Visit(AggregateOperand theOperand)
        {
            throw new NotImplementedException();
        }

        public List<string> Visit(BetweenOperator theOperator) => 
            this.cache;

        public List<string> Visit(BinaryOperator theOperator)
        {
            BinaryOperatorType operatorType = theOperator.OperatorType;
            return ((operatorType == BinaryOperatorType.Equal) ? theOperator.RightOperand.Accept<List<string>>(this) : ((operatorType == BinaryOperatorType.Like) ? (!(theOperator.RightOperand is OperandValue) ? this.cache : this.VisitLike((OperandValue) theOperator.RightOperand)) : this.cache));
        }

        public List<string> Visit(FunctionOperator theOperator)
        {
            if (theOperator.Operands.Count == 2)
            {
                switch (theOperator.OperatorType)
                {
                    case FunctionOperatorType.StartsWith:
                    case FunctionOperatorType.EndsWith:
                    case FunctionOperatorType.Contains:
                        return theOperator.Operands[1].Accept<List<string>>(this);
                }
            }
            return this.cache;
        }

        public List<string> Visit(GroupOperator theOperator)
        {
            foreach (CriteriaOperator @operator in theOperator.Operands)
            {
                @operator.Accept<List<string>>(this);
            }
            return this.cache;
        }

        public List<string> Visit(InOperator theOperator)
        {
            foreach (CriteriaOperator @operator in theOperator.Operands)
            {
                @operator.Accept<List<string>>(this);
            }
            return this.cache;
        }

        public List<string> Visit(JoinOperand theOperand)
        {
            throw new NotImplementedException();
        }

        public List<string> Visit(OperandProperty theOperand) => 
            this.cache;

        public List<string> Visit(OperandValue theOperand) => 
            this.VisitOperandValueCore(this.ExtractOperandValue(theOperand));

        public List<string> Visit(UnaryOperator theOperator) => 
            this.cache;

        public List<string> VisitLike(OperandValue theOperand) => 
            this.VisitOperandValueCore(this.ExtractOperandValue(theOperand).Replace("%", string.Empty));

        private List<string> VisitOperandValueCore(string value)
        {
            if (!string.IsNullOrEmpty(value) && !this.cache.Contains(value))
            {
                this.cache.Add(value);
            }
            return this.cache;
        }
    }
}

