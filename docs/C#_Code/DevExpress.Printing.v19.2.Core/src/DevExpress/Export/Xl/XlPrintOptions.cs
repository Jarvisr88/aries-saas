namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlPrintOptions
    {
        internal bool IsDefault() => 
            !this.GridLines && (!this.Headings && (!this.HorizontalCentered && !this.VerticalCentered));

        public bool GridLines { get; set; }

        public bool Headings { get; set; }

        public bool HorizontalCentered { get; set; }

        public bool VerticalCentered { get; set; }
    }
}

