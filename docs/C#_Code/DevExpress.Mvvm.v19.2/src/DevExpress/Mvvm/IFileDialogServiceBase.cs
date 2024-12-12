namespace DevExpress.Mvvm
{
    using System;
    using System.ComponentModel;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public interface IFileDialogServiceBase
    {
        void Reset();

        bool CheckFileExists { get; set; }

        bool AddExtension { get; set; }

        bool AutoUpgradeEnabled { get; set; }

        bool CheckPathExists { get; set; }

        bool DereferenceLinks { get; set; }

        string InitialDirectory { get; set; }

        bool RestoreDirectory { get; set; }

        bool ShowHelp { get; set; }

        bool SupportMultiDottedExtensions { get; set; }

        string Title { get; set; }

        bool ValidateNames { get; set; }
    }
}

