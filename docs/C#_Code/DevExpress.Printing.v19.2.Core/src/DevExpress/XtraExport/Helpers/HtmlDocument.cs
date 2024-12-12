namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class HtmlDocument : HtmlTag
    {
        private const string CFHtmlHeader = "Version:0.9\r\nStartHTML:{0:0000000000}\r\nEndHTML:{1:0000000000}\r\nStartFragment:{2:0000000000}\r\nEndFragment:{3:0000000000}\r\n";
        public const string DefaultFont = "Calibri";

        public HtmlDocument() : base(null)
        {
            this.<Doctype>k__BackingField = new HtmlDoctype("html");
            this.<Head>k__BackingField = new HtmlHead(this);
            this.<Body>k__BackingField = new HtmlBody(this);
        }

        private string CalcHeader(string compiled)
        {
            int byteCount = Encoding.UTF8.GetByteCount($"Version:0.9
StartHTML:{0:0000000000}
EndHTML:{0:0000000000}
StartFragment:{0:0000000000}
EndFragment:{0:0000000000}
");
            int num2 = byteCount + Encoding.UTF8.GetByteCount(compiled);
            int num3 = (byteCount + compiled.IndexOf(this.Body.StartFragment.Compiled)) + Encoding.UTF8.GetByteCount(this.Body.StartFragment.Compiled);
            string s = compiled.Substring(0, compiled.IndexOf(this.Body.EndFragment.Compiled));
            int num4 = byteCount + Encoding.UTF8.GetByteCount(s);
            return $"Version:0.9
StartHTML:{byteCount:0000000000}
EndHTML:{num2:0000000000}
StartFragment:{num3:0000000000}
EndFragment:{num4:0000000000}
";
        }

        public string Compile()
        {
            string compiled = base.CompileCore();
            return (this.CalcHeader(compiled) + compiled);
        }

        protected override string Compile(int level = 0)
        {
            this.WriteDoctype();
            base.WriteOpenTag(false, level);
            this.WriteHead(level);
            this.WriteBody(level);
            base.WriteCloseTag(level);
            return base.Compile(level);
        }

        protected override void PreCompile()
        {
            base.PreCompile(this.Doctype);
            base.PreCompile(this.Head);
            base.PreCompile(this.Body);
        }

        private void WriteBody(int level)
        {
            if (this.Body != null)
            {
                base.WriteChild(this.Body, level);
            }
        }

        private void WriteDoctype()
        {
            base.WriteChild(this.Doctype, 0);
        }

        private void WriteHead(int level)
        {
            if (this.Head != null)
            {
                base.WriteChild(this.Head, level);
            }
        }

        protected override string Tag =>
            "html";

        public HtmlBody Body { get; }

        public HtmlDoctype Doctype { get; }

        public HtmlHead Head { get; }
    }
}

