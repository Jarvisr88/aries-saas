namespace DevExpress.Mvvm
{
    using System;

    [Obsolete("Use the IDocumentContent interface instead.")]
    public interface IDocumentViewModel
    {
        bool Close();

        object Title { get; }
    }
}

