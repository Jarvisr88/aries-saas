namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.Collections.Generic;

    public class CurrentProcess : ICurrentProcess
    {
        public string ExecutablePath =>
            NativeResourceManager.ApplicationExecutablePath;

        public IEnumerable<string> CommandLineArgs =>
            Environment.GetCommandLineArgs();

        public string ApplicationId =>
            NativeResourceManager.ApplicationIdHash;
    }
}

