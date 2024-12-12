namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class XlHyperlinkBase
    {
        protected XlHyperlinkBase()
        {
        }

        public string TargetUri { get; set; }

        public string Tooltip { get; set; }
    }
}

