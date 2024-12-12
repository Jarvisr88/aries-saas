namespace DevExpress.Mvvm
{
    using System.Collections.Generic;

    public interface IDocumentGroup
    {
        IEnumerable<IDocument> Documents { get; }
    }
}

