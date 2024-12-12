namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections.Generic;

    public class CustomPalette : ColorPalette
    {
        public CustomPalette(string name, IEnumerable<Color> colors) : base(name, colors)
        {
        }

        public CustomPalette(string name, IEnumerable<Color> colors, bool calcBorder) : base(name, colors, calcBorder)
        {
        }
    }
}

