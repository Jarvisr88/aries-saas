namespace DevExpress.XtraReports.Parameters.Native
{
    using DevExpress.Data;
    using System;

    public interface IParameterExpressionSuppressService
    {
        bool IsExpressionSuppressed(IParameter parameter);
        void SetParameterValueAndSuppressExpression(IParameter parameter, object value);
        void SuppressExpression(IParameter parameter);
    }
}

