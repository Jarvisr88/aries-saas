namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class HtmlTitle : HtmlTag
    {
        public HtmlTitle() : base(null)
        {
        }

        protected override string Compile(int level = 0)
        {
            base.WriteOpenTag(false, level);
            base.WriteValue(this.Title, level);
            base.WriteCloseTag(level);
            return base.Compile(level);
        }

        protected override void PreCompile()
        {
        }

        protected override string Tag =>
            "title";

        public string Title { get; set; }
    }
}

