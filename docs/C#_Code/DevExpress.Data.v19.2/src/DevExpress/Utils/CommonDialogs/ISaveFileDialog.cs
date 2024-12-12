namespace DevExpress.Utils.CommonDialogs
{
    using System;

    public interface ISaveFileDialog : IFileDialog, ICommonDialog, IDisposable
    {
        bool CreatePrompt { get; set; }

        bool OverwritePrompt { get; set; }
    }
}

