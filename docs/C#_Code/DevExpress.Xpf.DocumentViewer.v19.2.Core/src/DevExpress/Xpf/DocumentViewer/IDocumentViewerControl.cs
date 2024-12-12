namespace DevExpress.Xpf.DocumentViewer
{
    using System;

    public interface IDocumentViewerControl
    {
        void AttachDocumentPresenterControl(DocumentPresenterControl presenter);

        DevExpress.Xpf.DocumentViewer.UndoRedoManager UndoRedoManager { get; }

        CommandProvider ActualCommandProvider { get; }

        BehaviorProvider ActualBehaviorProvider { get; }

        double HorizontalPageSpacing { get; }

        double VerticalPageSpacing { get; }

        double ZoomFactor { get; }
    }
}

