namespace DevExpress.Mvvm
{
    using System;
    using System.ComponentModel;

    public interface IDocumentContent
    {
        void OnClose(CancelEventArgs e);
        void OnDestroy();

        IDocumentOwner DocumentOwner { get; set; }

        object Title { get; }
    }
}

