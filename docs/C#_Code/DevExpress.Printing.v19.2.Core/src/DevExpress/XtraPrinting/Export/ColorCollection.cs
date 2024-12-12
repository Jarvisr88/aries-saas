namespace DevExpress.XtraPrinting.Export
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Reflection;

    public class ColorCollection : CollectionBase
    {
        public int Add(Color color) => 
            base.List.Add(color);

        public int IndexOf(Color color)
        {
            for (int i = 0; i < base.Count; i++)
            {
                if (IsColorEqual(this[i], color))
                {
                    return i;
                }
            }
            return -1;
        }

        internal static bool IsColorEqual(Color firstColor, Color secondColor) => 
            (firstColor.R == secondColor.R) && ((firstColor.G == secondColor.G) && ((firstColor.B == secondColor.B) && (firstColor.A == secondColor.A)));

        public Color this[int index]
        {
            get => 
                (Color) base.List[index];
            set => 
                base.List[index] = value;
        }
    }
}

