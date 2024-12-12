namespace DevExpress.Xpf.Printing.PreviewControl.Native.Rendering
{
    using DevExpress.Xpf.Editors;
    using System;

    public class InvalidateDocumentPreviewRenderingAction : IAggregateAction, IAction
    {
        private readonly ISupportInvalidateRenderingOnIdle control;

        public InvalidateDocumentPreviewRenderingAction(ISupportInvalidateRenderingOnIdle control)
        {
            this.control = control;
        }

        public bool CanAggregate(IAction action) => 
            action is InvalidateDocumentPreviewRenderingAction;

        public void Execute()
        {
            this.control.InvalidateRenderingOnIdle();
        }
    }
}

