namespace DevExpress.Pdf
{
    using System;
    using System.IO;

    public class PdfDocumentProcessorHelper
    {
        private readonly PdfFormData formData;
        private readonly Func<Stream, PdfSaveOptions, bool> saveToStreamAction;
        private readonly Func<string, PdfSaveOptions, bool> saveToPathAction;

        internal PdfDocumentProcessorHelper(Func<Stream, PdfSaveOptions, bool> saveToStreamAction, Func<string, PdfSaveOptions, bool> saveToPathAction, PdfFormData formData)
        {
            this.formData = formData;
            this.saveToStreamAction = saveToStreamAction;
            this.saveToPathAction = saveToPathAction;
        }

        internal PdfFormData GetFormData() => 
            this.formData;

        internal bool Save(Stream stream, PdfSaveOptions options) => 
            (this.saveToStreamAction == null) ? false : this.saveToStreamAction(stream, options);

        internal bool Save(string path, PdfSaveOptions options) => 
            (this.saveToPathAction == null) ? false : this.saveToPathAction(path, options);
    }
}

