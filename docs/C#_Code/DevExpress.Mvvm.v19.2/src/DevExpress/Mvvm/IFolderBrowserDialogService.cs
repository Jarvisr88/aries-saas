namespace DevExpress.Mvvm
{
    using System;

    public interface IFolderBrowserDialogService
    {
        bool ShowDialog();

        string StartPath { get; set; }

        string ResultPath { get; }
    }
}

