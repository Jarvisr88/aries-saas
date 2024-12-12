namespace DevExpress.XtraReports.Native.Parameters
{
    using DevExpress.Data.Filtering;
    using DevExpress.XtraReports.Native;
    using System;
    using System.Collections.Generic;

    public class ParametersValueSetter : ClientCriteriaVisitorBase
    {
        private IEnumerable<IParameter> parameters;
        private Action<string> onInvalidParameterName;

        public ParametersValueSetter(IEnumerable<IParameter> parameters);
        public ParametersValueSetter(IEnumerable<IParameter> parameters, Action<string> onInvalidParameterName);
        private bool IsMultivalueParameter(CriteriaOperator operand);
        public static void Process(CriteriaOperator criteriaOperator, IEnumerable<IParameter> parameters);
        public static void Process(ParametersValueSetter setter, CriteriaOperator criteriaOperator);
        private void SubstituteMultivalueParameters(CriteriaOperatorCollection operands);
        public override CriteriaOperator Visit(InOperator theOperator);
        public override CriteriaOperator Visit(OperandValue theOperand);

        protected IEnumerable<IParameter> Parameters { get; }
    }
}

