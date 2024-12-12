namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    internal class RtfFont : RtfControl
    {
        public RtfFont(string fontName, int index) : this(fontName, index, Keyword.FontFamilyNil)
        {
        }

        public RtfFont(string fontName, int index, Keyword fontFamily)
        {
            this.<FontFamily>k__BackingField = fontFamily;
            this.<FontName>k__BackingField = fontName;
            this.<Index>k__BackingField = index;
        }

        public override string Compile()
        {
            base.WriteOpenBrace();
            base.WriteKeyword(Keyword.Font, new int?(this.Index), false, false);
            int? nullable = null;
            base.WriteKeyword(this.FontFamily, nullable, false, false);
            base.WriteSpace();
            base.WriteValue(this.FontName, true);
            base.WriteCloseBrace();
            return base.Compile();
        }

        public Keyword FontFamily { get; }

        public string FontName { get; }

        public int Index { get; }
    }
}

