namespace DevExpress.Mvvm.UI.Native
{
    using System;

    public interface IJumpAction
    {
        void Execute();
        void SetStartInfo(string applicationPath, string arguments);

        string CommandId { get; }

        string ApplicationPath { get; }

        string Arguments { get; }

        string WorkingDirectory { get; }
    }
}

