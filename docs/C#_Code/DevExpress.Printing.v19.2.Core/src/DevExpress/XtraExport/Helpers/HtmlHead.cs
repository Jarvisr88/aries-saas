namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class HtmlHead : HtmlTag
    {
        internal HtmlHead(HtmlDocument document) : base(document)
        {
            this.<Style>k__BackingField = new HtmlStyle(document);
            this.<Title>k__BackingField = new HtmlTitle();
            this.<Meta>k__BackingField = new HtmlMeta();
        }

        protected override string Compile(int level = 0)
        {
            base.WriteOpenTag(false, level);
            this.WriteMeta(level);
            this.WriteTitle(level);
            this.WriteStyle(level);
            base.WriteCloseTag(level);
            return base.Compile(level);
        }

        protected override void PreCompile()
        {
            base.PreCompile(this.Meta);
            base.PreCompile(this.Style);
            base.PreCompile(this.Title);
        }

        private void WriteMeta(int level)
        {
            base.WriteChild(this.Meta, level);
        }

        private void WriteStyle(int level)
        {
            if (this.Style.HasStyles)
            {
                base.WriteChild(this.Style, level);
            }
        }

        private void WriteTitle(int level)
        {
            if (!string.IsNullOrEmpty(this.Title.Title))
            {
                base.WriteChild(this.Title, level);
            }
        }

        protected override string Tag =>
            "head";

        public HtmlMeta Meta { get; }

        public HtmlStyle Style { get; }

        public HtmlTitle Title { get; }
    }
}

