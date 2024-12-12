namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.Editors;
    using System;

    public class InvalidateThumbnailsViewerRenderingAction : IAggregateAction, IAction
    {
        private readonly ThumbnailsViewerPresenterControl control;

        public InvalidateThumbnailsViewerRenderingAction(ThumbnailsViewerPresenterControl control)
        {
            this.control = control;
        }

        public bool CanAggregate(IAction action) => 
            action is InvalidateThumbnailsViewerRenderingAction;

        public void Execute()
        {
            this.control.InvalidateRenderingOnIdle();
        }
    }
}

