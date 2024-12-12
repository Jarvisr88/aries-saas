namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class ErrorsEvaluatorCriteriaValidator : BoundPropertyList, IClientCriteriaVisitor, ICriteriaVisitor
    {
        protected List<CriteriaValidatorError> errors;
        private const string IncorrectDataAssignedToProperty = "A value with incorrect data type was assigned to property '{0}'";

        public ErrorsEvaluatorCriteriaValidator(List<IBoundProperty> properties);
        public void Clear();
        protected virtual object GetCorrectFiltereColumnValue(IBoundProperty filterProperty, object value);
        protected virtual Type GetFilterColumnType(IBoundProperty filterProperty);
        protected virtual object GetFilterColumnValueForValidation(IBoundProperty filterProperty, object value);
        protected IBoundProperty GetFilterPropertyByPropertyName(OperandProperty property);
        protected bool IsListProperty(IBoundProperty property);
        public virtual void Validate(CriteriaOperator criteria);
        public void Validate(IList operands);
        public void Validate(string filter);
        protected void ValidateOperandType(CriteriaOperator op1, CriteriaOperator op2);
        protected bool verifyOperandType(CriteriaOperator op, Type type);
        protected virtual bool verifyOperandType(CriteriaOperator op1, CriteriaOperator op2, out IBoundProperty filterProperty);
        public virtual void Visit(AggregateOperand theOperand);
        public virtual void Visit(BetweenOperator theOperator);
        public virtual void Visit(BinaryOperator theOperator);
        public virtual void Visit(FunctionOperator theOperator);
        public virtual void Visit(GroupOperator theOperator);
        public virtual void Visit(InOperator theOperator);
        public virtual void Visit(JoinOperand theOperand);
        public virtual void Visit(OperandProperty theOperand);
        public virtual void Visit(OperandValue theOperand);
        public virtual void Visit(UnaryOperator theOperator);

        public CriteriaValidatorError this[int index] { get; }

        public int Count { get; }

        protected virtual string InvalidPropertyMessage { get; }
    }
}

