namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    internal class RtfColorTable : RtfControl
    {
        private readonly Dictionary<Color, RtfColor> colors;

        public RtfColorTable()
        {
            Dictionary<Color, RtfColor> dictionary1 = new Dictionary<Color, RtfColor>();
            dictionary1.Add(Color.Black, new RtfColor(Color.Black, 0));
            dictionary1.Add(Color.White, new RtfColor(Color.White, 1));
            this.colors = dictionary1;
        }

        public void CheckColor(Color color)
        {
            this.GetOrCreateColor(color);
        }

        public override string Compile()
        {
            base.WriteOpenBrace();
            int? nullable = null;
            base.WriteKeyword(Keyword.ColorTable, nullable, false, false);
            this.WriteColors();
            base.WriteCloseBrace();
            return base.Compile();
        }

        public int GetColorIndex(Color color) => 
            this.GetOrCreateColor(color).Index;

        private RtfColor GetOrCreateColor(Color color)
        {
            RtfColor color2;
            if (!this.colors.TryGetValue(color, out color2))
            {
                color2 = new RtfColor(color, this.colors.Count);
                this.colors.Add(color, color2);
            }
            return color2;
        }

        private void WriteColors()
        {
            foreach (RtfColor color in this.colors.Values)
            {
                base.WriteChild(color, false);
            }
        }
    }
}

