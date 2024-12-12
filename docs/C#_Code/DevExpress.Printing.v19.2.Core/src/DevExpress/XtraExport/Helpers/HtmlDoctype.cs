namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class HtmlDoctype : HtmlTag
    {
        public HtmlDoctype(string doctype) : base(null)
        {
            this.<Doctype>k__BackingField = doctype;
        }

        protected override string Compile(int level = 0)
        {
            base.WriteOpenTag(false, 0);
            return base.Compile(level);
        }

        protected override void PreCompile()
        {
        }

        protected override string Tag =>
            "!DOCTYPE " + this.Doctype;

        public string Doctype { get; }
    }
}

