namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.Collections.Generic;

    public interface ICurrentProcess
    {
        string ExecutablePath { get; }

        string ApplicationId { get; }

        IEnumerable<string> CommandLineArgs { get; }
    }
}

