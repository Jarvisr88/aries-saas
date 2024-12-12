namespace DevExpress.XtraPrinting.Native.CharacterComb
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.InteropServices;

    public class CharacterCombInfo
    {
        private float width;
        private float height;
        private float horzSpacing;
        private float vertSpacing;
        private DevExpress.XtraPrinting.SizeMode sizeMode;
        private BrickStyle style;
        private bool rightToLeftLayout;

        public CharacterCombInfo(CharacterCombInfo source);
        public CharacterCombInfo(float width, float height, float horzSpacing, float vertSpacing, DevExpress.XtraPrinting.SizeMode sizeMode, BrickStyle style, bool rightToLeftLayout = false);

        public float Width { get; }

        public float Height { get; }

        public float HorzSpacing { get; }

        public float VertSpacing { get; }

        public DevExpress.XtraPrinting.SizeMode SizeMode { get; }

        public BrickStyle Style { get; }

        public bool RightToLeftLayout { get; }
    }
}

