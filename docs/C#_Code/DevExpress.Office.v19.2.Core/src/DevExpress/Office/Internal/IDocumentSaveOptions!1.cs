namespace DevExpress.Office.Internal
{
    using System;

    public interface IDocumentSaveOptions<TFormat>
    {
        string DefaultFileName { get; set; }

        string CurrentFileName { get; set; }

        TFormat DefaultFormat { get; set; }

        TFormat CurrentFormat { get; set; }
    }
}

