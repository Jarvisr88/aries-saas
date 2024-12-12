namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;

    internal abstract class HtmlTag
    {
        protected List<HtmlAttribute> attributes = new List<HtmlAttribute>();
        protected StringBuilder builder = new StringBuilder();
        protected StyleSheet styleSheet;
        protected internal HtmlDocument document;

        internal HtmlTag(HtmlDocument document = null)
        {
            this.document = document;
        }

        protected virtual string Compile(int level = 0) => 
            this.builder.ToString();

        protected string CompileCore()
        {
            this.PreCompile();
            return this.Compile(0);
        }

        protected abstract void PreCompile();
        protected void PreCompile(HtmlTag tag)
        {
            if (tag != null)
            {
                tag.PreCompile();
            }
        }

        protected void WriteAttribute(string name, string value)
        {
            this.attributes.Add(new HtmlAttribute(name, value));
        }

        private void WriteAttributes()
        {
            this.WriteClassAttribute();
            if (this.attributes.Count != 0)
            {
                this.builder.Append(' ');
                foreach (HtmlAttribute attribute in this.attributes)
                {
                    this.builder.Append(attribute.Name);
                    this.builder.Append("=\"");
                    this.builder.Append(attribute.Value);
                    this.builder.Append("\" ");
                }
            }
        }

        protected void WriteChild(HtmlTag tag, int level = 0)
        {
            if (tag != null)
            {
                this.builder.Append(tag.Compile(level + 1));
            }
        }

        private void WriteClassAttribute()
        {
            if (this.styleSheet != null)
            {
                string className = this.document.Head.Style.GetClassName(this.styleSheet);
                this.attributes.Add(new HtmlAttribute("class", className));
            }
        }

        protected void WriteCloseTag(int level = 0)
        {
            this.WriteLevelIndent(level);
            this.builder.Append("</");
            this.builder.Append(this.Tag);
            this.builder.AppendLine(">");
        }

        protected void WriteLevelIndent(int level)
        {
            if (level > 0)
            {
                this.builder.Append(new string('\t', level));
            }
        }

        protected void WriteOpenTag(bool selfClosed = false, int level = 0)
        {
            this.WriteLevelIndent(level);
            this.builder.Append("<");
            this.builder.Append(this.Tag);
            this.WriteAttributes();
            if (selfClosed)
            {
                this.builder.AppendLine("/>");
            }
            else
            {
                this.builder.AppendLine(">");
            }
        }

        protected void WriteStyle(string style, string value)
        {
            this.styleSheet ??= new StyleSheet();
            this.styleSheet.AddStyle(style, value);
        }

        protected void WriteValue(string value, int level = 0)
        {
            this.WriteLevelIndent(level + 1);
            this.builder.AppendLine(value);
        }

        protected abstract string Tag { get; }
    }
}

