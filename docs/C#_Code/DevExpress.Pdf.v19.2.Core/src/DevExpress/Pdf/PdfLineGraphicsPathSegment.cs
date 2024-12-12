namespace DevExpress.Pdf
{
    using System;
    using System.Collections.Generic;

    public class PdfLineGraphicsPathSegment : PdfGraphicsPathSegment
    {
        public PdfLineGraphicsPathSegment(PdfPoint endPoint) : base(endPoint)
        {
        }

        protected internal override void GeneratePathSegmentCommands(IList<PdfCommand> commands)
        {
            commands.Add(new PdfAppendLineSegmentCommand(base.EndPoint));
        }

        protected internal override bool Flat =>
            true;
    }
}

