namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfAnnotationActions
    {
        internal const string CursorEnteredDictionaryKey = "E";
        internal const string CursorExitedDictionaryKey = "X";
        internal const string MouseButtonPressedDictionaryKey = "D";
        internal const string MouseButtonReleasedDictionaryKey = "U";
        internal const string InputFocusReceivedDictionaryKey = "Fo";
        internal const string InputFocusLostDictionaryKey = "Bl";
        internal const string PageOpenedDictionaryKey = "PO";
        internal const string PageClosedDictionaryKey = "PC";
        internal const string PageBecameVisibleDictionaryKey = "PV";
        internal const string PageBecameInvisibleDictionaryKey = "PI";
        private readonly PdfAction cursorEntered;
        private readonly PdfAction cursorExited;
        private readonly PdfAction mouseButtonPressed;
        private readonly PdfAction mouseButtonReleased;
        private readonly PdfAction inputFocusReceived;
        private readonly PdfAction inputFocusLost;
        private readonly PdfAction pageOpened;
        private readonly PdfAction pageClosed;
        private readonly PdfAction pageBecameVisible;
        private readonly PdfAction pageBecameInvisible;

        internal PdfAnnotationActions(PdfReaderDictionary dictionary)
        {
            this.cursorEntered = dictionary.GetAction("E");
            this.cursorExited = dictionary.GetAction("X");
            this.mouseButtonPressed = dictionary.GetAction("D");
            this.mouseButtonReleased = dictionary.GetAction("U");
            this.inputFocusReceived = dictionary.GetAction("Fo");
            this.inputFocusLost = dictionary.GetAction("Bl");
            this.pageOpened = dictionary.GetAction("PO");
            this.pageClosed = dictionary.GetAction("PC");
            this.pageBecameVisible = dictionary.GetAction("PV");
            this.pageBecameInvisible = dictionary.GetAction("PI");
        }

        internal PdfWriterDictionary FillDictionary(PdfWriterDictionary dictionary)
        {
            dictionary.Add("E", this.cursorEntered);
            dictionary.Add("X", this.cursorExited);
            dictionary.Add("D", this.mouseButtonPressed);
            dictionary.Add("U", this.mouseButtonReleased);
            dictionary.Add("Fo", this.inputFocusReceived);
            dictionary.Add("Bl", this.inputFocusLost);
            dictionary.Add("PO", this.pageOpened);
            dictionary.Add("PC", this.pageClosed);
            dictionary.Add("PV", this.pageBecameVisible);
            dictionary.Add("PI", this.pageBecameInvisible);
            return dictionary;
        }

        public PdfAction CursorEntered =>
            this.cursorEntered;

        public PdfAction CursorExited =>
            this.cursorExited;

        public PdfAction MouseButtonPressed =>
            this.mouseButtonPressed;

        public PdfAction MouseButtonReleased =>
            this.mouseButtonReleased;

        public PdfAction InputFocusReceived =>
            this.inputFocusReceived;

        public PdfAction InputFocusLost =>
            this.inputFocusLost;

        public PdfAction PageOpened =>
            this.pageOpened;

        public PdfAction PageClosed =>
            this.pageClosed;

        public PdfAction PageBecameVisible =>
            this.pageBecameVisible;

        public PdfAction PageBecameInvisible =>
            this.pageBecameInvisible;
    }
}

