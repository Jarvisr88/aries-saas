namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfRemoteGoToAction : PdfJumpAction
    {
        internal const string Name = "GoToR";
        private const string fileDictionaryKey = "F";
        private const string newWindowKey = "NewWindow";
        private readonly PdfFileSpecification fileSpecification;
        private readonly bool openInNewWindow;

        internal PdfRemoteGoToAction(PdfReaderDictionary dictionary) : base(dictionary)
        {
            this.fileSpecification = PdfFileSpecification.Parse(dictionary, "F", true);
            bool? boolean = dictionary.GetBoolean("NewWindow");
            this.openInNewWindow = (boolean != null) ? boolean.GetValueOrDefault() : false;
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            dictionary.Add("F", objects.AddObject((PdfObject) this.fileSpecification));
            dictionary.Add("NewWindow", this.openInNewWindow, false);
            return dictionary;
        }

        protected internal override void Execute(IPdfInteractiveOperationController interactiveOperationController, IList<PdfPage> pages)
        {
            PdfDestination destination = base.Destination;
            interactiveOperationController.OpenDocument(this.fileSpecification.FileName, destination?.CreateTarget(pages), this.openInNewWindow);
        }

        protected override string ActionType =>
            "GoToR";

        protected override bool IsInternal =>
            false;

        public PdfFileSpecification FileSpecification =>
            this.fileSpecification;

        public bool OpenInNewWindow =>
            this.openInNewWindow;
    }
}

