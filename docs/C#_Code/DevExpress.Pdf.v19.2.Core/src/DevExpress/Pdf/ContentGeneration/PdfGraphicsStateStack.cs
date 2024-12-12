namespace DevExpress.Pdf.ContentGeneration
{
    using System;
    using System.Collections.Generic;

    public class PdfGraphicsStateStack
    {
        private Stack<PdfGraphicsExportState> stack = new Stack<PdfGraphicsExportState>();
        private PdfGraphicsExportState current = new PdfGraphicsExportState();

        public void Pop()
        {
            if (this.stack.Count == 0)
            {
                this.current = new PdfGraphicsExportState();
            }
            else
            {
                this.current = this.stack.Pop();
            }
        }

        public void Push()
        {
            this.stack.Push(new PdfGraphicsExportState(this.current));
        }

        public PdfGraphicsExportState Current
        {
            get => 
                this.current;
            set => 
                this.current = value;
        }
    }
}

