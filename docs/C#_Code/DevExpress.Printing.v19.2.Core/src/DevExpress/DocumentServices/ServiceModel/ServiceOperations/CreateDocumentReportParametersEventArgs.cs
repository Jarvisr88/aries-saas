namespace DevExpress.DocumentServices.ServiceModel.ServiceOperations
{
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using System;
    using System.Runtime.CompilerServices;

    public class CreateDocumentReportParametersEventArgs : EventArgs
    {
        public CreateDocumentReportParametersEventArgs(ReportParameterContainer reportParameters)
        {
            this.ReportParameters = reportParameters;
        }

        public ReportParameterContainer ReportParameters { get; private set; }
    }
}

