namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfNamedAction : PdfAction
    {
        internal const string Name = "Named";
        private const string actionNameDictionaryKey = "N";
        private readonly string actionName;

        internal PdfNamedAction(PdfReaderDictionary dictionary) : base(dictionary)
        {
            this.actionName = dictionary.GetName("N");
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            dictionary.AddName("N", this.actionName);
            return dictionary;
        }

        protected internal override void Execute(IPdfInteractiveOperationController interactiveOperationController, IList<PdfPage> pages)
        {
            string actionName = this.actionName;
            if (actionName == "NextPage")
            {
                interactiveOperationController.GoToNextPage();
            }
            else if (actionName == "PrevPage")
            {
                interactiveOperationController.GoToPreviousPage();
            }
            else if (actionName == "FirstPage")
            {
                interactiveOperationController.GoToFirstPage();
            }
            else if (actionName == "LastPage")
            {
                interactiveOperationController.GoToLastPage();
            }
            else if (actionName == "Print")
            {
                interactiveOperationController.ShowPrintDialog();
            }
        }

        public string ActionName =>
            this.actionName;

        protected override string ActionType =>
            "Named";
    }
}

