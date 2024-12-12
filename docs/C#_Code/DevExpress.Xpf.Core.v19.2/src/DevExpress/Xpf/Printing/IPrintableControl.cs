namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Printing.BrickCollection;
    using DevExpress.XtraPrinting.DataNodes;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public interface IPrintableControl
    {
        event EventHandler<ScalarOperationCompletedEventArgs<IRootDataNode>> CreateRootNodeCompleted;

        IRootDataNode CreateRootNode(Size usablePageSize, Size reportHeaderSize, Size reportFooterSize, Size pageHeaderSize, Size pageFooterSize);
        void CreateRootNodeAsync(Size usablePageSize, Size reportHeaderSize, Size reportFooterSize, Size pageHeaderSize, Size pageFooterSize);
        IVisualTreeWalker GetCustomVisualTreeWalker();
        void PagePrintedCallback(IEnumerator pageBrickEnumerator, Dictionary<IVisualBrick, IOnPageUpdater> brickUpdaters);

        bool CanCreateRootNodeAsync { get; }
    }
}

