namespace DevExpress.Xpf.Editors
{
    using System;

    public sealed class NullPalette : ColorPalette
    {
        private static readonly NullPalette instance = new NullPalette();

        private NullPalette()
        {
        }

        public static NullPalette Instance =>
            instance;
    }
}

