namespace DevExpress.XtraReports.Parameters.Native
{
    using DevExpress.Utils;
    using DevExpress.XtraReports.Parameters;
    using System;
    using System.Runtime.CompilerServices;

    public class ParameterLookUpValuesContainer
    {
        public ParameterLookUpValuesContainer(DevExpress.XtraReports.Parameters.Parameter parameter, LookUpValueCollection lookUpValues)
        {
            Guard.ArgumentNotNull(parameter, "parameter");
            this.Parameter = parameter;
            this.LookUpValues = lookUpValues;
        }

        public DevExpress.XtraReports.Parameters.Parameter Parameter { get; private set; }

        public LookUpValueCollection LookUpValues { get; private set; }
    }
}

