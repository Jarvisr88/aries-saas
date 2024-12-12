namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class HtmlStyle : HtmlTag
    {
        private Dictionary<string, string> styles;

        public HtmlStyle(HtmlDocument document) : base(document)
        {
            this.styles = new Dictionary<string, string>();
        }

        public void AddStyle(StyleSheet styleSheet)
        {
            if (styleSheet != null)
            {
                string className = this.GetClassName(styleSheet);
                if (!this.styles.ContainsKey(className))
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append('.');
                    builder.Append(className);
                    builder.Append(" { ");
                    foreach (KeyValuePair<string, string> pair in styleSheet.Styles)
                    {
                        builder.Append(pair.Key);
                        builder.Append(":");
                        builder.Append(pair.Value);
                        builder.Append("; ");
                    }
                    builder.Append('}');
                    this.styles.Add(className, builder.ToString());
                }
            }
        }

        protected override string Compile(int level = 0)
        {
            base.WriteOpenTag(false, level);
            this.WriteStyles(level);
            base.WriteCloseTag(level);
            return base.Compile(level);
        }

        public string GetClassName(StyleSheet styleSheet) => 
            "cs" + styleSheet.GetHashCode().ToString("X");

        protected override void PreCompile()
        {
            base.WriteAttribute("type", "text/css");
        }

        private void WriteStyles(int level)
        {
            foreach (string str in this.styles.Values)
            {
                base.WriteLevelIndent(level + 1);
                base.builder.AppendLine(str);
            }
        }

        protected override string Tag =>
            "style";

        internal bool HasStyles =>
            this.styles.Any<KeyValuePair<string, string>>();
    }
}

