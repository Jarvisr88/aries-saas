namespace DevExpress.Xpo.DB
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.Collections.Generic;

    public class IndeterminateStatmentFinder : IQueryCriteriaVisitor<bool>, ICriteriaVisitor<bool>
    {
        private List<JoinNode> listToNodeCollection;

        public IndeterminateStatmentFinder(List<JoinNode> listToNodeCollection)
        {
            this.listToNodeCollection = listToNodeCollection;
        }

        public bool Process(CriteriaOperator operand) => 
            !operand.ReferenceEqualsNull() ? operand.Accept<bool>(this) : false;

        public bool Visit(BetweenOperator theOperator) => 
            !this.Process(theOperator.BeginExpression) ? this.Process(theOperator.EndExpression) : true;

        public bool Visit(BinaryOperator theOperator) => 
            !this.Process(theOperator.LeftOperand) ? this.Process(theOperator.RightOperand) : true;

        public bool Visit(FunctionOperator theOperator)
        {
            FunctionOperatorType operatorType = theOperator.OperatorType;
            if ((operatorType != FunctionOperatorType.CustomNonDeterministic) && (operatorType != FunctionOperatorType.Rnd))
            {
                switch (operatorType)
                {
                    case FunctionOperatorType.Now:
                    case FunctionOperatorType.UtcNow:
                    case FunctionOperatorType.Today:
                        break;

                    default:
                        bool flag;
                        using (List<CriteriaOperator>.Enumerator enumerator = theOperator.Operands.GetEnumerator())
                        {
                            while (true)
                            {
                                if (enumerator.MoveNext())
                                {
                                    CriteriaOperator current = enumerator.Current;
                                    if (!this.Process(current))
                                    {
                                        continue;
                                    }
                                    flag = true;
                                }
                                else
                                {
                                    return false;
                                }
                                break;
                            }
                        }
                        return flag;
                }
            }
            return true;
        }

        public bool Visit(GroupOperator theOperator)
        {
            bool flag;
            using (List<CriteriaOperator>.Enumerator enumerator = theOperator.Operands.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        CriteriaOperator current = enumerator.Current;
                        if (!this.Process(current))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public bool Visit(InOperator theOperator)
        {
            bool flag;
            if (this.Process(theOperator.LeftOperand))
            {
                return true;
            }
            using (List<CriteriaOperator>.Enumerator enumerator = theOperator.Operands.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        CriteriaOperator current = enumerator.Current;
                        if (!this.Process(current))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public bool Visit(OperandValue theOperand) => 
            false;

        public bool Visit(UnaryOperator theOperator) => 
            this.Process(theOperator.Operand);

        public bool Visit(QueryOperand theOperand) => 
            false;

        public bool Visit(QuerySubQueryContainer theOperand)
        {
            if ((this.listToNodeCollection != null) && (theOperand.Node != null))
            {
                this.listToNodeCollection.Add(theOperand.Node);
            }
            return this.Process(theOperand.AggregateProperty);
        }
    }
}

