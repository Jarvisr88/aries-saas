namespace DevExpress.Xpf.Printing.PreviewControl.Native.Models
{
    using DevExpress.XtraReports.Parameters.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class ReportParametersReceivedEventArgs : EventArgs
    {
        public ReportParametersReceivedEventArgs(IList<ParameterModel> parameters, ILookUpValuesProvider lookUpValuesProvider)
        {
            this.Parameters = parameters;
            this.LookUpValuesProvider = lookUpValuesProvider;
        }

        public IList<ParameterModel> Parameters { get; private set; }

        public ILookUpValuesProvider LookUpValuesProvider { get; private set; }
    }
}

