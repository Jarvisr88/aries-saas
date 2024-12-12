namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;

    public class MDIDocumentElementBehavior : DockLayoutElementBehavior
    {
        public MDIDocumentElementBehavior(MDIDocumentElement element) : base(element)
        {
        }

        public override bool CanDrag(OperationType operation) => 
            (this.Document != null) ? ((operation == OperationType.Resizing) ? (!this.Document.IsMaximized && (!this.Document.IsMinimized && this.AllowResizing)) : (operation == OperationType.Reordering)) : false;

        protected DocumentPanel Document =>
            base.Element.Item as DocumentPanel;

        public override bool AllowDragging =>
            base.AllowDragging || this.AllowResizing;
    }
}

