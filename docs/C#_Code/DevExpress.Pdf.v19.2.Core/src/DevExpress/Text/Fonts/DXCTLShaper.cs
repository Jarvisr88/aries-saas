namespace DevExpress.Text.Fonts
{
    using System;
    using System.Collections.Generic;

    public abstract class DXCTLShaper : DXShaper
    {
        protected DXCTLShaper()
        {
        }

        public abstract IEnumerable<IDXTextRun> Itemize(string text, bool directionRightToLeft);
    }
}

