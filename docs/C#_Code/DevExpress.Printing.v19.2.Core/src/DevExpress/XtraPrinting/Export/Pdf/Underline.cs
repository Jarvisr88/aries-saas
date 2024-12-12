namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class Underline : FontLine
    {
        public Underline(float ascent, float descent) : base(ascent, descent)
        {
        }

        protected override float CalculateYOffset(float ascent, float descent) => 
            -descent * 0.5f;
    }
}

