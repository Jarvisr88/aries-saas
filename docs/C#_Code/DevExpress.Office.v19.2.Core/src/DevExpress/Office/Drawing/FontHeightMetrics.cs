namespace DevExpress.Office.Drawing
{
    using System;
    using System.Runtime.CompilerServices;

    public class FontHeightMetrics
    {
        public int Height { get; set; }

        public int Ascent { get; set; }

        public int Descent { get; set; }

        public int InternalLeading { get; set; }

        public int ExternalLeading { get; set; }

        public FontPitchAndFamily PitchAndFamily { get; set; }

        public char FirstChar { get; set; }

        public char LastChar { get; set; }

        public int UnderlineDelta { get; set; }

        public int AverageCharWidth { get; set; }

        public int MaxCharWidth { get; set; }
    }
}

