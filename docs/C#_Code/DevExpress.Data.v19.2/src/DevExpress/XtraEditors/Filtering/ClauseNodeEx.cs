namespace DevExpress.XtraEditors.Filtering
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.XtraEditors;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ClauseNodeEx : ClauseNode, IClauseNodeEx, IClauseNode, INode
    {
        public ClauseNodeEx(FilterTreeNodeModel model);
        public ClauseNodeEx(FilterTreeNodeModel model, object functionType);
        protected override void ChangeElement(NodeEditableElement element, object value);
        public IEnumerable<string> GetAvailableCustomFunctions();
        public IEnumerable<string> GetAvailableGlobalCustomFunctions();
        public IEnumerable<FunctionOperatorType> GetAvailablePredefinedFunctions();
        protected override string GetOperationString();
        protected override OperandValue GetUpdatedOperandValue(Type editingPropertyType, CriteriaOperator operand, int valueIndex);
        protected override Type GetValueType();
        private bool IsFunctionType(object value);
        protected override void NodeChanged(FilterChangedAction action, Node node);
        private bool TryGetCustomFunctionOperandType(int valueIndex, out Type operandType);
        public bool TryGetCustomFunctionsFromAttributes(out IEnumerable<string> functionNames);
        public bool TryGetFunctionImageFromAttribute(string functionName, out string image);
        public override bool TryGetOperandValueType(out Type operandValueType);
        public override void ValidateAdditionalOperands();

        public object FunctionType { get; set; }
    }
}

