namespace DevExpress.XtraReports.Parameters.Native
{
    using DevExpress.Data;
    using DevExpress.XtraReports.Parameters;
    using System;
    using System.Collections.Generic;

    internal class ReportParameterExpressionSuppressService : IParameterExpressionSuppressService
    {
        private readonly HashSet<Parameter> suppressedParameters = new HashSet<Parameter>();

        bool IParameterExpressionSuppressService.IsExpressionSuppressed(IParameter parameter) => 
            this.IsExpressionSuppressed((Parameter) parameter);

        void IParameterExpressionSuppressService.SetParameterValueAndSuppressExpression(IParameter parameter, object value)
        {
            this.SetParameterValueAndSuppressExpression((Parameter) parameter, value);
        }

        void IParameterExpressionSuppressService.SuppressExpression(IParameter parameter)
        {
            this.SuppressExpression((Parameter) parameter);
        }

        public bool IsExpressionSuppressed(Parameter parameter) => 
            this.suppressedParameters.Contains(parameter);

        public void Reset(Parameter parameter)
        {
            this.suppressedParameters.Remove(parameter);
        }

        public void SetParameterValueAndSuppressExpression(Parameter parameter, object value)
        {
            parameter.Value = value;
            this.SuppressExpression(parameter);
        }

        public void SuppressExpression(Parameter parameter)
        {
            this.suppressedParameters.Add(parameter);
        }
    }
}

