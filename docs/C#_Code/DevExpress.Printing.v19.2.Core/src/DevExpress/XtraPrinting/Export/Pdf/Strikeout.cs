namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class Strikeout : FontLine
    {
        public Strikeout(float ascent, float descent) : base(ascent, descent)
        {
        }

        protected override float CalculateYOffset(float ascent, float descent) => 
            (ascent * 0.5f) - descent;
    }
}

