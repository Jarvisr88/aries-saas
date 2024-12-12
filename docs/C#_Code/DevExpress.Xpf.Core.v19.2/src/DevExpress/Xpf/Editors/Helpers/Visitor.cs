namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;

    public class Visitor : IClientCriteriaVisitor, ICriteriaVisitor
    {
        public HashSet<string> RequestedProperties = new HashSet<string>();

        public void Visit(AggregateOperand theOperand)
        {
            theOperand.AggregatedExpression.Do<CriteriaOperator>(x => x.Accept(this));
        }

        public void Visit(BetweenOperator theOperator)
        {
            theOperator.BeginExpression.Do<CriteriaOperator>(x => x.Accept(this));
            theOperator.EndExpression.Do<CriteriaOperator>(x => x.Accept(this));
        }

        public void Visit(BinaryOperator theOperator)
        {
            theOperator.LeftOperand.Do<CriteriaOperator>(x => x.Accept(this));
            theOperator.RightOperand.Do<CriteriaOperator>(x => x.Accept(this));
        }

        public void Visit(FunctionOperator theOperator)
        {
            theOperator.Operands.Do<CriteriaOperatorCollection>(x => x.ForEach(o => o.Accept(this)));
        }

        public void Visit(GroupOperator theOperator)
        {
            theOperator.Operands.Do<CriteriaOperatorCollection>(x => x.ForEach(o => o.Accept(this)));
        }

        public void Visit(InOperator theOperator)
        {
            theOperator.LeftOperand.Do<CriteriaOperator>(x => x.Accept(this));
            theOperator.Operands.Do<CriteriaOperatorCollection>(x => x.ForEach(o => o.Accept(this)));
        }

        public void Visit(JoinOperand theOperand)
        {
            theOperand.AggregatedExpression.Do<CriteriaOperator>(x => x.Accept(this));
        }

        public void Visit(OperandProperty theOperand)
        {
            string propertyName = theOperand.PropertyName;
            if (!string.IsNullOrEmpty(propertyName) && !this.RequestedProperties.Contains(propertyName))
            {
                this.RequestedProperties.Add(propertyName);
            }
        }

        public void Visit(OperandValue theOperand)
        {
        }

        public void Visit(UnaryOperator theOperator)
        {
            theOperator.Operand.Do<CriteriaOperator>(x => x.Accept(this));
        }
    }
}

