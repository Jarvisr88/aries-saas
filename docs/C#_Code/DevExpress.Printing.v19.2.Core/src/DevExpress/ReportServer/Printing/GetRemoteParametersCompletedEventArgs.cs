namespace DevExpress.ReportServer.Printing
{
    using DevExpress.XtraReports.Parameters.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class GetRemoteParametersCompletedEventArgs : EventArgs
    {
        public GetRemoteParametersCompletedEventArgs(IList<ClientParameter> parameters, ILookUpValuesProvider lookUpValuesProvider)
        {
            this.Parameters = parameters;
            this.LookUpValuesProvider = lookUpValuesProvider;
        }

        public IList<ClientParameter> Parameters { get; private set; }

        public ILookUpValuesProvider LookUpValuesProvider { get; private set; }
    }
}

