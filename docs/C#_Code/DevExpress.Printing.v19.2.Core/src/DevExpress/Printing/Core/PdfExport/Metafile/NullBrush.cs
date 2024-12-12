namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing;

    public class NullBrush : Brush
    {
        public override object Clone() => 
            new NullBrush();
    }
}

