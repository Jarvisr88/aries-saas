namespace DevExpress.Text.Fonts
{
    using System;
    using System.Runtime.CompilerServices;

    public class DXFontFileInfo
    {
        public DXFontFileInfo(int index, byte[] data)
        {
            this.<Index>k__BackingField = index;
            this.<Data>k__BackingField = data;
        }

        public int Index { get; }

        public byte[] Data { get; }
    }
}

