namespace DevExpress.Printing.Core.NativePdfExport
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public abstract class DeferredAction
    {
        private RectangleF rect;
        private bool executed;

        protected DeferredAction(RectangleF rect)
        {
            this.rect = rect;
        }

        protected abstract void DoAction(PrintingSystemBase ps, IGraphics gr);
        public virtual void Execute(PrintingSystemBase ps, IGraphics gr)
        {
            if (this.executed)
            {
                throw new InvalidOperationException();
            }
            this.DoAction(ps, gr);
            this.executed = true;
        }

        public RectangleF Rect
        {
            get => 
                this.rect;
            set => 
                this.rect = value;
        }
    }
}

