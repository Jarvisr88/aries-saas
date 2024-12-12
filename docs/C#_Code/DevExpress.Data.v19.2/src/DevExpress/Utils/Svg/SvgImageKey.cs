namespace DevExpress.Utils.Svg
{
    using System;
    using System.Drawing;

    public class SvgImageKey
    {
        private const int Prime = 0x1000193;
        private const int Basis = 0x50c5d1f;
        private int hashCode;

        public SvgImageKey(string name, Size size, int? paletteHashCode)
        {
            this.hashCode = 0x50c5d1f ^ name.GetHashCode();
            this.hashCode = (this.hashCode * 0x1000193) ^ ((size.Width * 0x11) ^ size.Height);
            if (paletteHashCode != null)
            {
                this.hashCode = (this.hashCode * 0x1000193) ^ paletteHashCode.Value;
            }
        }

        public override bool Equals(object obj)
        {
            SvgImageKey key = obj as SvgImageKey;
            return ((key != null) ? EqualsCore(this, key) : false);
        }

        private static bool EqualsCore(SvgImageKey svgImageKey1, SvgImageKey svgImageKey2) => 
            ((svgImageKey1 == null) || (svgImageKey2 == null)) ? ((svgImageKey1 == null) && ReferenceEquals(svgImageKey2, null)) : (svgImageKey1.GetHashCode() == svgImageKey2.GetHashCode());

        public override int GetHashCode() => 
            this.hashCode;
    }
}

