namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class HtmlComment : HtmlTag
    {
        public HtmlComment(string comment) : base(null)
        {
            this.<Comment>k__BackingField = comment;
        }

        protected override string Compile(int level = 0)
        {
            base.WriteOpenTag(false, level);
            return base.Compile(level);
        }

        protected override void PreCompile()
        {
        }

        protected override string Tag =>
            "!--" + this.Comment + "--";

        internal string Compiled =>
            this.Compile(0);

        public string Comment { get; }
    }
}

