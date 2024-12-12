namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class XlRichTextString : IXlString
    {
        private readonly List<XlRichTextRun> runs = new List<XlRichTextRun>();

        public override bool Equals(object obj)
        {
            XlRichTextString str = obj as XlRichTextString;
            if (str == null)
            {
                return false;
            }
            int count = this.Runs.Count;
            if (count != str.Runs.Count)
            {
                return false;
            }
            for (int i = 0; i < count; i++)
            {
                if (!this.Runs[i].Equals(str.Runs[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            int count = this.Runs.Count;
            int num2 = count;
            for (int i = 0; i < count; i++)
            {
                num2 ^= this.Runs[i].GetHashCode();
            }
            return num2;
        }

        internal XlRichTextString Truncate(int maxLength)
        {
            int num = 0;
            XlRichTextString str = new XlRichTextString();
            foreach (XlRichTextRun run in this.runs)
            {
                if ((num + run.Text.Length) > maxLength)
                {
                    string str2 = run.Text.Substring(0, maxLength - num);
                    if (!string.IsNullOrEmpty(str2))
                    {
                        str.Runs.Add(new XlRichTextRun(str2, run.Font));
                    }
                    break;
                }
                str.Runs.Add(run);
                num += run.Text.Length;
            }
            return str;
        }

        public IList<XlRichTextRun> Runs =>
            this.runs;

        public string Text
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                foreach (XlRichTextRun run in this.runs)
                {
                    builder.Append(run.Text);
                }
                return builder.ToString();
            }
            set
            {
                this.runs.Clear();
                if (!string.IsNullOrEmpty(value))
                {
                    this.runs.Add(new XlRichTextRun(value, null));
                }
            }
        }

        public bool IsPlainText =>
            (this.runs.Count != 0) ? ((this.runs.Count == 1) && ReferenceEquals(this.runs[0].Font, null)) : true;

        internal int Length
        {
            get
            {
                int num = 0;
                foreach (XlRichTextRun run in this.runs)
                {
                    num += run.Text.Length;
                }
                return num;
            }
        }
    }
}

