namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class JustifiedStringPainter : IDisposable
    {
        private Font font;
        private IGraphics gr;
        private StringFormat stringFormat;
        private Brush brush;
        private RectangleF fitRect;
        private RectangleF clipRect;
        private StringAlignment lineAlignment;
        private string text;
        private bool doClip;
        private Matrix identityMatrix;

        protected JustifiedStringPainter(string text, IGraphics gr, RectangleF rect, Font font, Brush br, StringFormat sf, bool doClip);
        private static void AddWordInfoToLines(JustifiedStringPainter.WordInfo info, Dictionary<float, List<JustifiedStringPainter.WordInfo>> ht);
        private void AdjustWordsYPosition(JustifiedStringPainter.WordInfo[] infos, float dy);
        private void AlignWordsToBottom(JustifiedStringPainter.WordInfo[] infos, RectangleF rect);
        private Region[] CalcRegions(CharacterRange[] ranges);
        private Region[] CalcRegions(CharacterRange[] ranges, StringFormat sf);
        private static float CalcTotalWordsWidth(List<JustifiedStringPainter.WordInfo> infos);
        private void CenterWordsVertically(JustifiedStringPainter.WordInfo[] infos, RectangleF rect);
        private static void CopyRanges(CharacterRange[] from, CharacterRange[] to, int startIndexFrom, int count);
        private JustifiedStringPainter.WordInfo[] CreateWordInfo(Region region, CharacterRange range);
        private JustifiedStringPainter.WordInfo[] CreateWordInfo(Region[] regions, CharacterRange[] ranges);
        public void Dispose();
        private void DoClip();
        protected void DrawJustifiedString();
        public static void DrawString(string text, IGraphics gr, Font font, Brush br, RectangleF rect, StringFormat sf, bool doClip);
        private static CharacterRange[] GetCharacterRanges(string text);
        private float GetWordGap(List<JustifiedStringPainter.WordInfo> infos);
        private static bool IsCR(char ch);
        private bool IsLastWord(CharacterRange range);
        private static bool IsLongHyphen(char ch);
        private static bool IsWhiteSpace(char ch);
        private void JustifyLine(List<JustifiedStringPainter.WordInfo> infos);
        private Region[] MeasureCharacterRangesImpl(StringFormat sf);
        private float MeasureTextHeight();
        private void PerformJustification(JustifiedStringPainter.WordInfo[] infos);
        private void RemeasureWords(List<JustifiedStringPainter.WordInfo> infos);
        private void SplitWord(CharacterRange range, Dictionary<float, JustifiedStringPainter.WordInfo> lines);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly JustifiedStringPainter.<>c <>9;
            public static Func<JustifiedStringPainter.WordInfo, JustifiedStringPainter.WordInfo> <>9__27_0;

            static <>c();
            internal JustifiedStringPainter.WordInfo <CreateWordInfo>b__27_0(JustifiedStringPainter.WordInfo info);
        }

        protected class WordInfo : IComparable<JustifiedStringPainter.WordInfo>
        {
            public string text;
            public RectangleF rect;
            public bool lastInLine;
            public CharacterRange range;

            public int CompareTo(JustifiedStringPainter.WordInfo obj);
        }
    }
}

