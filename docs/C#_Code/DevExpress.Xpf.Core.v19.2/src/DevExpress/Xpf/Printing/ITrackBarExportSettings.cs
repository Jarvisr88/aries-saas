namespace DevExpress.Xpf.Printing
{
    using System;

    public interface ITrackBarExportSettings : ITextExportSettings, IExportSettings
    {
        int Position { get; }

        int Minimum { get; }

        int Maximum { get; }
    }
}

