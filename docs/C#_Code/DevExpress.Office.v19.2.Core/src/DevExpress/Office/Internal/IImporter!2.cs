namespace DevExpress.Office.Internal
{
    using DevExpress.Office;
    using DevExpress.Office.Import;
    using System;
    using System.IO;

    public interface IImporter<TFormat, TResult>
    {
        TResult LoadDocument(IDocumentModel documentModel, Stream stream, IImporterOptions options);
        TResult LoadDocument(IDocumentModel documentModel, Stream stream, IImporterOptions options, bool leaveOpen);
        IImporterOptions SetupLoading();

        FileDialogFilter Filter { get; }

        TFormat Format { get; }
    }
}

