namespace DevExpress.DocumentView
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Runtime.CompilerServices;

    public interface IDocument : IServiceContainer, IServiceProvider
    {
        event EventHandler AfterBuildPages;

        event EventHandler BeforeBuildPages;

        event ExceptionEventHandler CreateDocumentException;

        event EventHandler Disposed;

        event EventHandler DocumentChanged;

        event EventHandler PageBackgrChanged;

        void AfterDrawPages(object syncObj, int[] pageIndices);
        void BeforeDrawPages(object syncObj);

        bool IsEmpty { get; }

        bool IsCreating { get; }

        bool IsRightToLeftLayout { get; }

        IList<IPage> Pages { get; }

        IPageSettings PageSettings { get; }
    }
}

