namespace DevExpress.XtraExport.Xlsx
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;

    public class SimpleSharedStringTable
    {
        private const int initialCapacity = 0x4000;
        private readonly Dictionary<string, int> textIndexTable = new Dictionary<string, int>(0x4000);
        private readonly Dictionary<XlRichTextString, int> richTextIndexTable = new Dictionary<XlRichTextString, int>(0x4000);
        private readonly List<IXlString> items = new List<IXlString>(0x4000);

        public void Clear()
        {
            this.textIndexTable.Clear();
            this.richTextIndexTable.Clear();
            this.items.Clear();
        }

        public int RegisterString(XlRichTextString text)
        {
            int count;
            if (text == null)
            {
                return this.RegisterString(string.Empty);
            }
            if (!this.richTextIndexTable.TryGetValue(text, out count))
            {
                count = this.items.Count;
                this.richTextIndexTable.Add(text, count);
                this.items.Add(text);
            }
            return count;
        }

        public int RegisterString(string text)
        {
            int count;
            text ??= string.Empty;
            if (!this.textIndexTable.TryGetValue(text, out count))
            {
                count = this.items.Count;
                this.textIndexTable.Add(text, count);
                this.items.Add(new XlString(text));
            }
            return count;
        }

        public int Count =>
            this.items.Count;

        public IList<IXlString> StringList =>
            this.items;
    }
}

