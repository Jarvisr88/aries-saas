namespace DevExpress.Utils.CommonDialogs
{
    using System;

    public interface IFolderBrowserDialog : ICommonDialog, IDisposable
    {
        string Description { get; set; }

        Environment.SpecialFolder RootFolder { get; set; }

        bool ShowNewFolderButton { get; set; }

        string SelectedPath { get; set; }
    }
}

