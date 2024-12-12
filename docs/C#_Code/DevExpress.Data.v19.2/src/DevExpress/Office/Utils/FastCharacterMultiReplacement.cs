namespace DevExpress.Office.Utils
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    public class FastCharacterMultiReplacement
    {
        private readonly StringBuilder buffer;

        public FastCharacterMultiReplacement(StringBuilder stringBuilder)
        {
            Guard.ArgumentNotNull(stringBuilder, "stringBuilder");
            this.buffer = stringBuilder;
        }

        public ReplacementInfo CreateReplacementInfo(string text, Dictionary<char, string> replaceTable) => 
            this.CreateReplacementInfo(text, 0, text.Length, replaceTable);

        public ReplacementInfo CreateReplacementInfo(string text, int start, int length, Dictionary<char, string> replaceTable)
        {
            ReplacementInfo info = null;
            for (int i = (start + length) - 1; i >= start; i--)
            {
                string str;
                if (replaceTable.TryGetValue(text[i], out str))
                {
                    info = new ReplacementInfo();
                    info.Add(i, str);
                }
            }
            return info;
        }

        private bool IsUnicodeEscapePattern(string text, int pos)
        {
            int num;
            return (((text.Length - pos) > 6) && ((text[pos] == '_') && ((text[pos + 1] == 'x') && ((text[pos + 6] == '_') && int.TryParse(text.Substring(pos + 2, 4), NumberStyles.HexNumber, null, out num)))));
        }

        public string PerformReplacements(string text, ReplacementInfo replacementInfo) => 
            this.PerformReplacements(text, 0, text.Length, replacementInfo);

        public string PerformReplacements(string text, Dictionary<char, string> replaceTable) => 
            this.PerformReplacements(text, this.CreateReplacementInfo(text, replaceTable));

        public string PerformReplacements(string text, int start, int length, ReplacementInfo replacementInfo)
        {
            if (replacementInfo == null)
            {
                return (((start != 0) || (length != text.Length)) ? text.Substring(start, length) : text);
            }
            this.buffer.Capacity = Math.Max(this.buffer.Capacity, length + replacementInfo.DeltaLength);
            IList<ReplacementItem> items = replacementInfo.Items;
            int startIndex = 0;
            char[] chArray = text.ToCharArray(start, length);
            for (int i = items.Count - 1; i >= 0; i--)
            {
                ReplacementItem item = items[i];
                int num4 = item.CharIndex - start;
                this.buffer.Append(chArray, startIndex, num4 - startIndex);
                this.buffer.Append(item.ReplaceWith);
                startIndex = num4 + 1;
            }
            this.buffer.Append(chArray, startIndex, chArray.Length - startIndex);
            string str = this.buffer.ToString();
            this.buffer.Length = 0;
            return str;
        }
    }
}

