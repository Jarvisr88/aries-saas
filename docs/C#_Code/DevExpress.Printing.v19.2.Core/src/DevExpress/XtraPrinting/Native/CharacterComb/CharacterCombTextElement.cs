namespace DevExpress.XtraPrinting.Native.CharacterComb
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct CharacterCombTextElement
    {
        public static readonly CharacterCombTextElement Empty;
        private string textElement;
        private int textIndex;
        private RectangleF rect;
        public int TextIndex { get; }
        public string TextElement { get; }
        public RectangleF Rect { get; }
        public bool IsEmpty { get; }
        public CharacterCombTextElement(string textElement, RectangleF rect, int textIndex);
        public override bool Equals(object obj);
        public override int GetHashCode();
        static CharacterCombTextElement();
    }
}

