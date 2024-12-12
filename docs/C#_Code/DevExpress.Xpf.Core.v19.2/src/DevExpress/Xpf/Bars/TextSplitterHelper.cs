namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.InteropServices;

    public class TextSplitterHelper
    {
        public static void SplitText(string text, out string firstString, out string secondString, SplitTextMode SplitMethod);
        public static void SplitTextAutomatically(string text, out string firstString, out string secondString);
        public static bool SplitTextBy(string text, out string firstString, out string secondString, params string[] splitSymbols);
        public static void SplitTextByBreakLine(string text, out string firstString, out string secondString);
        public static void SplitTextBySpace(string text, out string firstString, out string secondString);
    }
}

