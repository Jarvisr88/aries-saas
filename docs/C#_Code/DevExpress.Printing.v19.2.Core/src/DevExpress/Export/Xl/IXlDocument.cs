namespace DevExpress.Export.Xl
{
    using System;

    public interface IXlDocument : IDisposable
    {
        IXlSheet CreateSheet();
        void SetSheetPosition(string name, int position);

        IXlDocumentOptions Options { get; }

        XlDocumentProperties Properties { get; }

        XlDocumentTheme Theme { get; set; }

        XlDocumentView View { get; }
    }
}

