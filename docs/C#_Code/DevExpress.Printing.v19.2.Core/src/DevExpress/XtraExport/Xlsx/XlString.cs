namespace DevExpress.XtraExport.Xlsx
{
    using DevExpress.Export.Xl;
    using System;
    using System.Runtime.CompilerServices;

    internal class XlString : IXlString
    {
        public XlString(string text)
        {
            this.Text = text;
        }

        public string Text { get; private set; }

        public bool IsPlainText =>
            true;
    }
}

