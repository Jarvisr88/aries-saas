namespace DevExpress.Export
{
    using DevExpress.Export.Xl;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class XlCellFont
    {
        public System.Drawing.Color Color { get; set; }

        public string Name { get; set; }

        public double Size { get; set; }

        public XlUnderlineType Underline { get; set; }

        public bool Bold { get; set; }

        public bool StrikeThrough { get; set; }

        public bool Italic { get; set; }
    }
}

