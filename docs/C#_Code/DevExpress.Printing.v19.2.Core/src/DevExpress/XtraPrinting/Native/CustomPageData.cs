namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Runtime.CompilerServices;

    public class CustomPageData
    {
        public CustomPageData()
        {
            this.SectionID = -1;
        }

        public int SectionID { get; set; }

        public System.Drawing.Printing.Margins Margins { get; set; }

        public System.Drawing.Printing.PaperKind? PaperKind { get; set; }

        public Size PageSize { get; set; }

        public bool? Landscape { get; set; }
    }
}

