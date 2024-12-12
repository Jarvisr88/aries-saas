namespace DevExpress.Mvvm
{
    using System;
    using System.Runtime.CompilerServices;

    public class ActiveDocumentChangedEventArgs : EventArgs
    {
        public ActiveDocumentChangedEventArgs(IDocument oldDocument, IDocument newDocument)
        {
            this.OldDocument = oldDocument;
            this.NewDocument = newDocument;
        }

        public IDocument OldDocument { get; private set; }

        public IDocument NewDocument { get; private set; }
    }
}

