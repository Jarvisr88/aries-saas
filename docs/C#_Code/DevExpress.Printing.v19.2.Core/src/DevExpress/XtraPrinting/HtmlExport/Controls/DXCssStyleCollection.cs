namespace DevExpress.XtraPrinting.HtmlExport.Controls
{
    using DevExpress.XtraPrinting.HtmlExport;
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text.RegularExpressions;

    public sealed class DXCssStyleCollection
    {
        private const string StyleKey = "style";
        private static readonly Regex styleAttribRegex = new Regex(@"\G(\s*(;\s*)*(?<stylename>[^:]+?)\s*:\s*(?<styleval>[^;]*))*\s*(;\s*)*$", RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.Multiline);
        private Dictionary<DXHtmlTextWriterStyle, string> htmlStyleTable;
        private Dictionary<string, string> styleTable;
        private DXStateBag state;
        private string style;

        public DXCssStyleCollection() : this(null)
        {
        }

        internal DXCssStyleCollection(DXStateBag state)
        {
            this.state = state;
        }

        public void Add(DXHtmlTextWriterStyle key, string value)
        {
            this.htmlStyleTable ??= new Dictionary<DXHtmlTextWriterStyle, string>();
            this.htmlStyleTable[key] = value;
            string styleName = DXCssTextWriter.GetStyleName(key);
            if (styleName.Length != 0)
            {
                if (this.styleTable == null)
                {
                    this.ParseString();
                }
                this.styleTable.Remove(styleName);
            }
            if (this.state != null)
            {
                this.state["style"] = this.BuildString();
            }
            this.style = null;
        }

        public void Add(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            if (this.styleTable == null)
            {
                this.ParseString();
            }
            this.styleTable[key] = value;
            if (this.htmlStyleTable != null)
            {
                DXHtmlTextWriterStyle styleKey = DXCssTextWriter.GetStyleKey(key);
                if (styleKey != ~DXHtmlTextWriterStyle.BackgroundColor)
                {
                    this.htmlStyleTable.Remove(styleKey);
                }
            }
            if (this.state != null)
            {
                this.state["style"] = this.BuildString();
            }
            this.style = null;
        }

        private string BuildString()
        {
            string str;
            if (((this.styleTable == null) || (this.styleTable.Count == 0)) && ((this.htmlStyleTable == null) || (this.htmlStyleTable.Count == 0)))
            {
                return null;
            }
            using (StringWriter writer = new StringWriter())
            {
                using (DXCssTextWriter writer2 = new DXCssTextWriter(writer))
                {
                    this.Render(writer2);
                    str = writer.ToString();
                }
            }
            return str;
        }

        public void Clear()
        {
            this.styleTable = null;
            this.htmlStyleTable = null;
            if (this.state != null)
            {
                this.state.Remove("style");
            }
            this.style = null;
        }

        private void ParseString()
        {
            this.styleTable = new Dictionary<string, string>();
            string input = (this.state == null) ? this.style : ((string) this.state["style"]);
            if (input != null)
            {
                Match match = styleAttribRegex.Match(input, 0);
                if (match.Success)
                {
                    CaptureCollection captures = match.Groups["stylename"].Captures;
                    CaptureCollection captures2 = match.Groups["styleval"].Captures;
                    for (int i = 0; i < captures.Count; i++)
                    {
                        string str2 = captures[i].ToString();
                        string str3 = captures2[i].ToString();
                        this.styleTable[str2] = str3;
                    }
                }
            }
        }

        public void Remove(DXHtmlTextWriterStyle key)
        {
            if (this.htmlStyleTable != null)
            {
                this.htmlStyleTable.Remove(key);
                if (this.state != null)
                {
                    this.state["style"] = this.BuildString();
                }
                this.style = null;
            }
        }

        public void Remove(string key)
        {
            if (this.styleTable == null)
            {
                this.ParseString();
            }
            if (this.styleTable[key] != null)
            {
                this.styleTable.Remove(key);
                if (this.state != null)
                {
                    this.state["style"] = this.BuildString();
                }
                this.style = null;
            }
        }

        internal void Render(DXCssTextWriter writer)
        {
            if ((this.styleTable != null) && (this.styleTable.Count > 0))
            {
                foreach (KeyValuePair<string, string> pair in this.styleTable)
                {
                    writer.WriteAttribute(pair.Key, pair.Value);
                }
            }
            if ((this.htmlStyleTable != null) && (this.htmlStyleTable.Count > 0))
            {
                foreach (KeyValuePair<DXHtmlTextWriterStyle, string> pair2 in this.htmlStyleTable)
                {
                    writer.WriteAttribute(pair2.Key, pair2.Value);
                }
            }
        }

        internal void Render(DXHtmlTextWriter writer)
        {
            if ((this.styleTable != null) && (this.styleTable.Count > 0))
            {
                foreach (KeyValuePair<string, string> pair in this.styleTable)
                {
                    writer.AddStyleAttribute(pair.Key, pair.Value);
                }
            }
            if ((this.htmlStyleTable != null) && (this.htmlStyleTable.Count > 0))
            {
                foreach (KeyValuePair<DXHtmlTextWriterStyle, string> pair2 in this.htmlStyleTable)
                {
                    writer.AddStyleAttribute(pair2.Key, pair2.Value);
                }
            }
        }

        public int Count
        {
            get
            {
                if (this.styleTable == null)
                {
                    this.ParseString();
                }
                return (this.styleTable.Count + ((this.htmlStyleTable != null) ? this.htmlStyleTable.Count : 0));
            }
        }

        public string this[string key]
        {
            get
            {
                if (this.styleTable == null)
                {
                    this.ParseString();
                }
                string str = null;
                if (!this.styleTable.TryGetValue(key, out str))
                {
                    DXHtmlTextWriterStyle styleKey = DXCssTextWriter.GetStyleKey(key);
                    if (styleKey != ~DXHtmlTextWriterStyle.BackgroundColor)
                    {
                        str = this[styleKey];
                    }
                }
                return str;
            }
            set => 
                this.Add(key, value);
        }

        public string this[DXHtmlTextWriterStyle key]
        {
            get
            {
                if (this.htmlStyleTable == null)
                {
                    return null;
                }
                string str = null;
                this.htmlStyleTable.TryGetValue(key, out str);
                return str;
            }
            set => 
                this.Add(key, value);
        }

        public ICollection Keys
        {
            get
            {
                if (this.styleTable == null)
                {
                    this.ParseString();
                }
                if (this.htmlStyleTable == null)
                {
                    return this.styleTable.Keys;
                }
                string[] strArray = new string[this.styleTable.Count + this.htmlStyleTable.Count];
                int num = 0;
                foreach (string str in this.styleTable.Keys)
                {
                    strArray[num++] = str;
                }
                foreach (DXHtmlTextWriterStyle style in this.htmlStyleTable.Keys)
                {
                    strArray[num++] = DXCssTextWriter.GetStyleName(style);
                }
                return strArray;
            }
        }

        public string Value
        {
            get
            {
                if (this.state != null)
                {
                    return (string) this.state["style"];
                }
                this.style ??= this.BuildString();
                return this.style;
            }
            set
            {
                if (this.state == null)
                {
                    this.style = value;
                }
                else
                {
                    this.state["style"] = value;
                }
                this.styleTable = null;
            }
        }
    }
}

