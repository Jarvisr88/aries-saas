namespace DevExpress.XtraReports.Parameters.Native
{
    using DevExpress.XtraReports.Parameters;
    using System;

    public static class ParameterInfoFactory
    {
        public static ParameterInfo CreateWithoutEditor(Parameter parameter) => 
            new ParameterInfo(parameter, null);
    }
}

