namespace DevExpress.XtraPrinting.Native.CharacterComb
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class CharacterCombPainter
    {
        private CharacterCombInfo cellInfo;

        public CharacterCombPainter(CharacterCombInfo cellInfo);
        protected virtual void DrawBorders(IGraphicsBase gr, float grDpi, RectangleF rect, BorderSide sides);
        public void DrawContent(IGraphicsBase gr, float grDpi, RectangleF clientRect, string text);
        public void DrawPdfContent(IPdfGraphics gr, float grDpi, RectangleF clientRect, TextEditingField editingField);
        protected virtual void DrawText(IGraphicsBase gr, RectangleF bounds, StringFormat sf, string text);
        protected virtual void FillRect(IGraphicsBase gr, RectangleF fillRect);
        private RectangleF GetFillRect(RectangleF baseRect, float borderWidth, BorderSide borderSides, int rowIndex, int columnIndex);
        private RectangleF GetRowRectangle(CharacterCombTextElement[,] textElementsData, int rowIndex, float borderWidth);
        private string GetRowText(CharacterCombTextElement[,] textElementsData, int rowIndex);

        protected BrickStyle Style { get; }

        protected int CurrentIndex { get; private set; }

        protected CharacterCombTextElement CurrentElement { get; private set; }
    }
}

