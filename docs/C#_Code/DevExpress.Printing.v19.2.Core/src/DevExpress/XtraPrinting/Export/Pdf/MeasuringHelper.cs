namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public static class MeasuringHelper
    {
        private static StringFormat stringFormat = ((StringFormat) StringFormat.GenericTypographic.Clone());

        static MeasuringHelper()
        {
            stringFormat.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
        }

        public static float MeasureCharWidth(char ch, Font font)
        {
            char[] chArray1 = new char[] { ch };
            return Measurement.Measurer.MeasureString(new string(chArray1), font, (float) 2.147484E+09f, stringFormat, GraphicsUnit.Point).Width;
        }
    }
}

