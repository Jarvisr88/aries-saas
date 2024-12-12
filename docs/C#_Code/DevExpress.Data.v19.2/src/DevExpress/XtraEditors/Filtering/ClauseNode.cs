namespace DevExpress.XtraEditors.Filtering
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class ClauseNode : Node, IClauseNode, INode
    {
        private OperandProperty _FirstOperand;
        private ClauseType _Operation;
        private readonly IList<CriteriaOperator> _AdditionalOperands;
        private bool disableValueChangedNotification;
        private const int MaxLength = 100;

        public ClauseNode(FilterTreeNodeModel model);
        protected override object Accept(INodeVisitor visitor);
        public override void AddElement();
        public void AdditionalOperands_AddRange(IEnumerable<CriteriaOperator> operands);
        private void AdditionalOperandsChaged(int index, CriteriaOperator item);
        protected void BuildOperationAndValueElements();
        protected void BuildValueElements();
        protected override void ChangeElement(NodeEditableElement element, object value);
        public void ChangeValue(int index, object value);
        protected virtual void ClauseNodeFirstOperandChanged(OperandProperty newProp, int elementIndex);
        public OperandParameter CreateDefaultParameter();
        public OperandProperty CreateDefaultProperty();
        public override void DeleteElement();
        public override CriteriaOperator GetAdditionalOperand(int elementIndex);
        public virtual List<ClauseType> GetAvailableOperations();
        protected List<ClauseType> GetAvailableOperations(IBoundProperty forProperty);
        private string GetCollectionValuesString();
        private object GetCorrectedValueType(object value);
        public override object GetCurrentValue(int elementIndex);
        protected string GetDisplayText(OperandProperty property);
        protected string GetDisplayText(OperandProperty firstOperand, CriteriaOperator op);
        public override IBoundProperty GetFocusedFilterProperty(int elementIndex);
        protected virtual string GetOperationString();
        private string GetParameterName(string parameterName);
        public virtual IBoundProperty GetPropertyForEditing();
        protected virtual OperandValue GetUpdatedOperandValue(Type propertyType, CriteriaOperator operand);
        protected virtual OperandValue GetUpdatedOperandValue(Type editingPropertyType, CriteriaOperator operand, int valueIndex);
        public object GetValue(int index);
        protected virtual Type GetValueType();
        protected virtual bool IsRequireChangeNodeType(IBoundProperty newProperty);
        public override void RebuildElements();
        protected string StringAdaptation(string text);
        public void SwapAdditionalOperandType(int index, OperandProperty defaultProperty);
        public void SwapAdditionalOperandType(int index, OperandProperty defaultProperty, OperandParameter defaultParameter);
        public virtual bool TryGetOperandValueType(out Type operandValueType);
        public void UpdateAdditionalOperands();
        public virtual void ValidateAdditionalOperands();

        public bool IsCollectionValues { get; }

        protected bool IsTwoFieldsClause { get; }

        protected bool CanAddCollectionItem { get; }

        public IBoundProperty Property { get; }

        public bool IsShowCollectionValueAsOnEditor { get; }

        public override int LastTabElementIndex { get; }

        public OperandProperty FirstOperand { get; set; }

        public ClauseType Operation { get; set; }

        public IList<CriteriaOperator> AdditionalOperands { get; }

        public virtual bool IsList { get; }

        public override string Text { get; }

        public bool ShowOperandTypeIcon { get; }

        public bool ShowParameterTypeIcon { get; }

        public bool ShowTypeIcon { get; }
    }
}

