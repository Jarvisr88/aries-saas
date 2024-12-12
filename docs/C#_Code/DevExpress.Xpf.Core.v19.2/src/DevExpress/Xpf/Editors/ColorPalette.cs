namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Media;

    public abstract class ColorPalette
    {
        private readonly ColorCollection colors;
        private readonly string name;
        private readonly bool calcBorder;

        protected ColorPalette()
        {
            this.colors = new ColorCollection();
        }

        protected ColorPalette(IEnumerable<Color> source)
        {
            this.colors = new ColorCollection();
            this.CopyToColors(source);
        }

        protected ColorPalette(string name, IEnumerable<Color> source) : this(source)
        {
            this.name = name;
        }

        protected ColorPalette(string name, IEnumerable<Color> source, bool calcBorder) : this(name, source)
        {
            this.calcBorder = calcBorder;
        }

        private void CopyToColors(IEnumerable<Color> source)
        {
            this.colors.Clear();
            if (source != null)
            {
                foreach (Color color in source)
                {
                    this.colors.Add(color);
                }
            }
        }

        public static ColorPalette CreateGradientPalette(string name, ColorCollection source) => 
            ColorHelper.CreateGradientPalette(name, source);

        [Description("Gets the palette's name.")]
        public string Name =>
            this.name;

        [Description("Gets the colors displayed within the palette.")]
        public IList<Color> Colors =>
            this.colors.AsReadOnly();

        [Description("Gets whether to paint top/bottom borders between neighboring color chips.")]
        public bool CalcBorder =>
            this.calcBorder;
    }
}

