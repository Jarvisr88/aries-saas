namespace DevExpress.Utils.Text
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class HyperlinkSettings
    {
        public HyperlinkSettings()
        {
            this.Color = System.Drawing.Color.Blue;
            this.Underline = true;
        }

        public System.Drawing.Color Color { get; set; }

        public bool Underline { get; set; }
    }
}

