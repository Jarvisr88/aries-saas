namespace DevExpress.Xpf.DocumentViewer
{
    using System;
    using System.Windows.Input;

    public interface IDocumentMapItem
    {
        int Id { get; }

        int ParentId { get; }

        string Title { get; }

        bool IsExpanded { get; set; }

        ICommand Command { get; }
    }
}

