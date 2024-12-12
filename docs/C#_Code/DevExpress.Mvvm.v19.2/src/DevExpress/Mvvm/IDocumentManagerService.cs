namespace DevExpress.Mvvm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public interface IDocumentManagerService
    {
        event ActiveDocumentChangedEventHandler ActiveDocumentChanged;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        IDocument CreateDocument(string documentType, object viewModel, object parameter, object parentViewModel);

        IDocument ActiveDocument { get; set; }

        IEnumerable<IDocument> Documents { get; }
    }
}

