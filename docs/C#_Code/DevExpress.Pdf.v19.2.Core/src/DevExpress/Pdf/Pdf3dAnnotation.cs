namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class Pdf3dAnnotation : PdfAnnotation
    {
        internal const string Type = "3D";
        private const string dataKey = "3DD";
        private const string defaultViewKey = "3DV";
        private const string activationKey = "3DA";
        private const string interactiveKey = "3DI";
        private readonly Pdf3dData data;
        private readonly Pdf3dActivationParameters activationParameters;
        private readonly Pdf3dView defaultView;
        private readonly bool isInteractive;
        private readonly PdfRectangle viewBox;

        internal Pdf3dAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
            PdfReaderStream stream = dictionary.GetStream("3DD");
            if (stream != null)
            {
                this.data = new Pdf3dStream(stream, page);
            }
            if (this.data == null)
            {
                PdfReaderDictionary dictionary3 = dictionary.GetDictionary("3DD");
                if (dictionary3 != null)
                {
                    this.data = new Pdf3dReference(dictionary3, page);
                }
            }
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("3DA");
            if (dictionary2 != null)
            {
                this.activationParameters = new Pdf3dActivationParameters(dictionary2);
            }
            this.defaultView = Pdf3dView.GetDefaultView(this.data, dictionary, page);
            bool? boolean = dictionary.GetBoolean("3DI");
            this.isInteractive = (boolean != null) ? boolean.GetValueOrDefault() : true;
            this.viewBox = dictionary.GetRectangle("3DB");
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(collection);
            dictionary.Add("3DD", this.data);
            dictionary.Add("3DA", this.activationParameters);
            dictionary.Add("3DI", this.isInteractive, true);
            dictionary.Add("3DV", this.defaultView);
            dictionary.Add("3DB", this.viewBox);
            return dictionary;
        }

        protected override string AnnotationType =>
            "3D";

        internal Pdf3dData Data =>
            this.data;

        internal Pdf3dView DefaultView =>
            this.defaultView;

        internal Pdf3dActivationParameters ActivationParameters =>
            this.activationParameters;

        internal bool IsInteractive =>
            this.isInteractive;

        internal PdfRectangle ViewBox =>
            this.viewBox;
    }
}

