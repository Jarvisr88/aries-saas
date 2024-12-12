namespace DevExpress.Data.Utils
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public static class AlignmentConverter
    {
        private static StringAlignment[] hAlign;
        private static StringAlignment[] vAlign;

        static AlignmentConverter();
        public static StringAlignment HorzAlignmentToStringAlignment(HorzAlignment align);
        public static StringAlignment VertAlignmentToStringAlignment(VertAlignment align);
    }
}

