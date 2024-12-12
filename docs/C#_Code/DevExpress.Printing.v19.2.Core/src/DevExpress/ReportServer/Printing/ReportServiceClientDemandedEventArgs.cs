namespace DevExpress.ReportServer.Printing
{
    using DevExpress.DocumentServices.ServiceModel.Client;
    using System;
    using System.Runtime.CompilerServices;

    public class ReportServiceClientDemandedEventArgs : EventArgs
    {
        public IReportServiceClient Client { get; set; }
    }
}

