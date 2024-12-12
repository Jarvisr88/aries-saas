namespace DevExpress.ReportServer.Printing
{
    using DevExpress.ReportServer.ServiceModel.Client;
    using System;
    using System.Runtime.CompilerServices;

    public class AuthenticationServiceClientDemandedEventArgs : EventArgs
    {
        public IAuthenticationServiceClient Client { get; set; }
    }
}

