namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class HtmlMeta : HtmlTag
    {
        public HtmlMeta() : base(null)
        {
            this.<Charset>k__BackingField = "utf-8";
            this.<Content>k__BackingField = "text/html";
        }

        protected override string Compile(int level = 0)
        {
            base.WriteOpenTag(true, level);
            return base.Compile(level);
        }

        private string GetContentType()
        {
            switch (this.ContentType)
            {
                case MetaContentType.ContentType:
                    return "content-type";

                case MetaContentType.DefaultStyle:
                    return "default-style";

                case MetaContentType.Refresh:
                    return "refresh";
            }
            return string.Empty;
        }

        protected override void PreCompile()
        {
            base.attributes.Clear();
            base.WriteAttribute("http-equiv", this.GetContentType());
            base.WriteAttribute("content", this.Content);
            base.WriteAttribute("charset", this.Charset);
        }

        protected override string Tag =>
            "meta";

        public string Charset { get; set; }

        public string Content { get; set; }

        public MetaContentType ContentType { get; set; }

        public enum MetaContentType
        {
            ContentType,
            DefaultStyle,
            Refresh
        }
    }
}

