namespace DevExpress.Xpf.Printing
{
    using System;

    public interface IProgressBarExportSettings : ITextExportSettings, IExportSettings
    {
        int Position { get; }
    }
}

