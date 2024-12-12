namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    public class GridDragDropStrategy : BaseDragDropStrategy
    {
        public GridDragDropStrategy(ISupportDragDropColumnHeader supportDragDrop, DragDropElementHelper helper) : base(supportDragDrop, helper)
        {
        }

        public override FrameworkElement GetDragElement() => 
            this.SupportDragDropColumnHeader.RelativeDragElement;

        public override FrameworkElement GetSourceElement() => 
            (FrameworkElement) base.Helper.GetDropTargetByHitElement(this.SupportDragDropColumnHeader.SourceElement);

        public override FrameworkElement GetTopVisual(FrameworkElement node) => 
            this.SupportDragDropColumnHeader.TopVisual;

        public override void UpdateLocation(IndependentMouseEventArgs e)
        {
            this.SupportDragDropColumnHeader.UpdateLocation(e);
        }

        private ISupportDragDropColumnHeader SupportDragDropColumnHeader =>
            (ISupportDragDropColumnHeader) base.SupportDragDrop;

        public override FrameworkElement SubscribedElement =>
            base.SupportDragDrop.SourceElement;
    }
}

