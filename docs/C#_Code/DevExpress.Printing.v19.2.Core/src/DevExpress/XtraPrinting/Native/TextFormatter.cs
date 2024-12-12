namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class TextFormatter : ITextFormatter
    {
        private const float measuringInaccuracy = 0.05f;
        private static string[] emptyStringArray;

        static TextFormatter();
        public TextFormatter(GraphicsUnit pageUnit, DevExpress.XtraPrinting.Native.Measurer measurer);
        public static float CalculateHeightOfLines(Font font, int lineCount, GraphicsUnit pageUnit);
        public static TextFormatter CreateInstance(GraphicsUnit pageUnit, DevExpress.XtraPrinting.Native.Measurer measurer);
        public string[] FormatHtmlMultilineText(string multilineText, Font font, float width, float height, StringFormat stringFormat);
        public string[] FormatHtmlMultilineText(string multilineText, Font font, float width, float height, StringFormat stringFormat, bool designateNewLines);
        public virtual string[] FormatMultilineText(string multilineText, Font font, float width, float height, StringFormat stringFormat);
        private string[] FormatText(string text, Font font, float width, float height, StringFormat stringFormat, int additionalLineCount, int totalTextLineCount, bool oneWord);
        internal static void RotateBasis(StringFormat sf, ref float width, ref float height);
        [IteratorStateMachine(typeof(TextFormatter.<SplitTextByNewLine>d__3))]
        internal static IEnumerable<string> SplitTextByNewLine(string text);
        internal static bool TrimByCharacter(StringFormat stringFormat);

        protected GraphicsUnit PageUnit { get; private set; }

        protected DevExpress.XtraPrinting.Native.Measurer Measurer { get; private set; }

    }
}

