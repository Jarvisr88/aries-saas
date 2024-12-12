namespace DevExpress.Office.Internal
{
    using DevExpress.Office;
    using DevExpress.Office.Export;
    using System.IO;

    public interface IExporter<TFormat, TResult>
    {
        TResult SaveDocument(IDocumentModel documentModel, Stream stream, IExporterOptions options);
        IExporterOptions SetupSaving();

        FileDialogFilter Filter { get; }

        TFormat Format { get; }
    }
}

