namespace DevExpress.Mvvm
{
    using System;

    public interface IDocumentInfo
    {
        DocumentState State { get; }

        string DocumentType { get; }
    }
}

