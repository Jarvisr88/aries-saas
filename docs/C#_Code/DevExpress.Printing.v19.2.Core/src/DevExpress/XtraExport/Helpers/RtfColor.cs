namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    internal class RtfColor : RtfControl
    {
        public RtfColor(System.Drawing.Color color, int index)
        {
            this.<Index>k__BackingField = index;
            this.<Color>k__BackingField = color;
        }

        public override string Compile()
        {
            base.WriteKeyword(Keyword.ColorRed, new int?(this.Color.R), false, false);
            base.WriteKeyword(Keyword.ColorGreen, new int?(this.Color.G), false, false);
            base.WriteKeyword(Keyword.ColorBlue, new int?(this.Color.B), true, false);
            return base.Compile();
        }

        public System.Drawing.Color Color { get; }

        public int Index { get; }
    }
}

