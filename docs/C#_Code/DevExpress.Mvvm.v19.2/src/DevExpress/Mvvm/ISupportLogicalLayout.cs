namespace DevExpress.Mvvm
{
    using System;
    using System.Collections.Generic;

    public interface ISupportLogicalLayout
    {
        bool CanSerialize { get; }

        IDocumentManagerService DocumentManagerService { get; }

        IEnumerable<object> LookupViewModels { get; }
    }
}

