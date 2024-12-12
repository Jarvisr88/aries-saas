namespace DevExpress.Xpf.Editors.EditStrategy
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;

    internal static class TextEditStrategyTextInputHelper
    {
        private static readonly List<string> IgnoredStrings = new List<string>();

        static TextEditStrategyTextInputHelper()
        {
            string item = ((char) KeyboardHelper.KeySystemCodeEnter).ToString();
            IgnoredStrings.Add(((char) KeyboardHelper.KeySystemCodeEscape).ToString());
            IgnoredStrings.Add(item);
            IgnoredStrings.Add(item + ((char) KeyboardHelper.KeySystemCodeLineFeed).ToString());
        }

        public static bool ShouldIgnoreTextInput(string s) => 
            IgnoredStrings.Contains(s);
    }
}

