namespace DevExpress.XtraReports.Parameters.Native
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.XtraReports.Native.Parameters;
    using DevExpress.XtraReports.Parameters;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class CascadingParametersValueSetter : ParametersValueSetter
    {
        private readonly IParameterEditorValueProvider valueProvider;

        public CascadingParametersValueSetter(IEnumerable<Parameter> parameters, IParameterEditorValueProvider valueProvider) : base(parameters)
        {
            this.valueProvider = valueProvider;
        }

        public static void Process(CriteriaOperator criteriaOperator, IEnumerable<Parameter> parameters, IParameterEditorValueProvider parameterValueProvider)
        {
            new CascadingParametersValueSetter(parameters, parameterValueProvider).Process(criteriaOperator);
        }

        public override CriteriaOperator Visit(OperandValue theOperand)
        {
            OperandParameter operandParameter = theOperand as OperandParameter;
            if ((operandParameter == null) || (this.valueProvider == null))
            {
                return base.Visit(theOperand);
            }
            IParameter parameterIdentity = base.Parameters.FirstOrDefault<IParameter>(x => x.Name == operandParameter.ParameterName);
            if (parameterIdentity == null)
            {
                return base.Visit(operandParameter);
            }
            try
            {
                operandParameter.Value = this.valueProvider.GetValue(parameterIdentity);
            }
            catch
            {
                return base.Visit(operandParameter);
            }
            return operandParameter;
        }
    }
}

