namespace DevExpress.Printing.Core.ReportServer.Services
{
    using System;
    using System.Runtime.CompilerServices;

    internal interface ICredentialsService
    {
        event EventHandler RequestCredentialsFailed;

        void RequestCredentials(Action<string, string> onCredentialsRecieved);
    }
}

