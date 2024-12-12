namespace DevExpress.Mvvm
{
    using System.Collections.Generic;

    public interface IGroupedDocumentManagerService
    {
        IEnumerable<IDocumentGroup> Groups { get; }

        IDocumentGroup ActiveGroup { get; }
    }
}

