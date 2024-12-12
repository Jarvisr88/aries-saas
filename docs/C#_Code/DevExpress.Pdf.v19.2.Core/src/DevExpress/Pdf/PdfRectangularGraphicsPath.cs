namespace DevExpress.Pdf
{
    using System;
    using System.Collections.Generic;

    public class PdfRectangularGraphicsPath : PdfGraphicsPath
    {
        private readonly PdfRectangle rectangle;

        public PdfRectangularGraphicsPath(double left, double bottom, double width, double height) : base(new PdfPoint(left, bottom))
        {
            double x = left + width;
            double y = bottom + height;
            base.AppendLineSegment(new PdfPoint(x, bottom));
            base.AppendLineSegment(new PdfPoint(x, y));
            base.AppendLineSegment(new PdfPoint(left, y));
            base.AppendLineSegment(new PdfPoint(left, bottom));
            this.rectangle = new PdfRectangle(new PdfPoint(left, bottom), new PdfPoint(x, y));
            base.Closed = true;
        }

        protected internal override void GeneratePathCommands(IList<PdfCommand> commands)
        {
            commands.Add(new PdfBeginPathCommand(base.StartPoint));
            commands.Add(new PdfAppendRectangleCommand(this.rectangle.Left, this.rectangle.Bottom, this.rectangle.Right, this.rectangle.Top));
        }

        internal override PdfRectangle GetAxisAlignedRectangle() => 
            this.rectangle;

        protected internal override bool IsFlat(bool forFilling) => 
            true;

        public PdfRectangle Rectangle =>
            this.rectangle;
    }
}

