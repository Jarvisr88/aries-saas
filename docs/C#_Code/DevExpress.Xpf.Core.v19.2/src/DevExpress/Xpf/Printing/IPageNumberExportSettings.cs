namespace DevExpress.Xpf.Printing
{
    using System;

    public interface IPageNumberExportSettings : ITextExportSettings, IExportSettings
    {
        string Format { get; }

        PageNumberKind Kind { get; }
    }
}

