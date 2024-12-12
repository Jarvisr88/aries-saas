namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Design;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using System;
    using System.ComponentModel;
    using System.Drawing.Printing;

    [TypeConverter(typeof(MarginsFConverter))]
    public class MarginsF : MarginsFBase
    {
        private static float scale = 3f;

        public MarginsF()
        {
        }

        public MarginsF(System.Drawing.Printing.Margins margins) : base(FromHundredths((float) margins.Left), FromHundredths((float) margins.Right), FromHundredths((float) margins.Top), FromHundredths((float) margins.Bottom))
        {
        }

        public MarginsF(System.Drawing.Printing.Margins margins, float dpi) : base((float) GraphicsUnitConverter.Convert(margins.Left, dpi, 300f), (float) GraphicsUnitConverter.Convert(margins.Right, dpi, 300f), (float) GraphicsUnitConverter.Convert(margins.Top, dpi, 300f), (float) GraphicsUnitConverter.Convert(margins.Bottom, dpi, 300f))
        {
        }

        public MarginsF(float left, float right, float top, float bottom) : base(left, right, top, bottom)
        {
        }

        public override object Clone() => 
            new MarginsF(base.Left, base.Right, base.Top, base.Bottom);

        public static float FromHundredths(float val) => 
            val * scale;

        public static implicit operator MarginsF(MarginsFloat margin) => 
            new MarginsF(margin.Left, margin.Right, margin.Top, margin.Bottom);

        private static int ToDpiValue(float val, float dpi) => 
            Convert.ToInt32(GraphicsUnitConverter.Convert(val, (float) 300f, dpi));

        public static float ToHundredths(float val) => 
            val / scale;

        public static System.Drawing.Printing.Margins ToMargins(MarginsF value) => 
            new System.Drawing.Printing.Margins(ToRoundedHundredths(value.Left), ToRoundedHundredths(value.Right), ToRoundedHundredths(value.Top), ToRoundedHundredths(value.Bottom));

        public static System.Drawing.Printing.Margins ToMargins(MarginsF value, float dpi) => 
            new System.Drawing.Printing.Margins(ToDpiValue(value.Left, dpi), ToDpiValue(value.Right, dpi), ToDpiValue(value.Top, dpi), ToDpiValue(value.Bottom, dpi));

        public static int ToRoundedHundredths(float val) => 
            Convert.ToInt32(ToHundredths(val));

        public System.Drawing.Printing.Margins Margins =>
            ToMargins(this);
    }
}

