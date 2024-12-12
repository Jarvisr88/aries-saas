namespace DevExpress.XtraPrinting.Native.CharacterComb
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class CharacterCombHelper
    {
        private CharacterCombInfo cellInfo;

        public CharacterCombHelper(CharacterCombInfo cellInfo);
        private static List<CharacterCombHelper.TextElement[]> FormatText(string multilineText, int horzLength, bool wordWrap);
        public static float GetActualBorderWidth(BrickStyle style);
        public float GetBottomSplitValue(float pageBottom, RectangleF rect);
        private static int GetCellCount(float size, float cellSize, float spacing, BrickStyle style);
        private static float GetCellOffset(float cellSize, float spacing, float borderWidth);
        public SizeF GetCellSize();
        private static SizeF GetContentSize(CharacterCombInfo cellInfo, SizeF cellSize, int vertCellCount, int horzCellCount);
        private static int GetFirstElementIndex(int length, int fullLength, StringAlignment alignment);
        private static float GetGridOffset(float clientSize, float contentSize, StringAlignment alignment);
        public float GetRightSplitValue(float pageRight, RectangleF rect);
        private static float GetSplitValue(float startPosition, float cellSize, float spacing, float limit, float borderWidth, int cellsCount);
        public CharacterCombTextElement[,] GetTextElementsData(RectangleF clientRect, string text);
        private static CharacterCombHelper.TextElement[,] GetTextElementsMatrix(string multilineText, int horzLength, int vertLength, BrickStringFormat stringFormat);
        public SizeF GetTextSize(string text, float clientWidth);
        private static List<CharacterCombHelper.TextElement[]> SplitTextElementsByLength(CharacterCombHelper.TextElement[] textElements, int length);
        private static CharacterCombHelper.TextElement[] SplitTextIntoTextElements(string text, int startIndex);

        protected virtual float FontHeight { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CharacterCombHelper.<>c <>9;
            public static Func<CharacterCombHelper.TextElement, int, <>f__AnonymousType0<int, CharacterCombHelper.TextElement>> <>9__14_0;
            public static Func<<>f__AnonymousType0<int, CharacterCombHelper.TextElement>, CharacterCombHelper.TextElement> <>9__14_3;
            public static Func<IGrouping<int, <>f__AnonymousType0<int, CharacterCombHelper.TextElement>>, CharacterCombHelper.TextElement[]> <>9__14_2;
            public static Func<CharacterCombHelper.TextElement[], int> <>9__16_0;

            static <>c();
            internal int <GetTextSize>b__16_0(CharacterCombHelper.TextElement[] x);
            internal <>f__AnonymousType0<int, CharacterCombHelper.TextElement> <SplitTextElementsByLength>b__14_0(CharacterCombHelper.TextElement x, int i);
            internal CharacterCombHelper.TextElement[] <SplitTextElementsByLength>b__14_2(IGrouping<int, <>f__AnonymousType0<int, CharacterCombHelper.TextElement>> x);
            internal CharacterCombHelper.TextElement <SplitTextElementsByLength>b__14_3(<>f__AnonymousType0<int, CharacterCombHelper.TextElement> v);
        }

        private class TextElement
        {
            public TextElement(string text, int startIndex);

            public string Text { get; private set; }

            public int StartIndex { get; private set; }
        }
    }
}

