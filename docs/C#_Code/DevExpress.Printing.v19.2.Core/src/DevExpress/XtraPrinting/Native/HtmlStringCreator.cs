namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class HtmlStringCreator
    {
        private readonly StringFormat stringFormat;
        private float width;
        private float height;

        public HtmlStringCreator(StringFormat stringFormat, float width, float height);
        public void AddHtmlControls(DXHtmlControl parent, string text, Measurer measurer, Font font);
        protected virtual void ApplyAlignment(DXHtmlControl control);
        private void ApplyLineAlignment(DXHtmlControl control);
        private DXHtmlLiteralControl EncodeStringToHtmlControl(string s);
        private static int GetLineHeight(string[] strings, Measurer measurer, Font font, StringFormat stringFormat);
        protected virtual void PostProcessLine(string str, List<DXWebControlBase> result);
        private static StringAlignment RTLAlignment(StringAlignment alignment);
        private static void SetParent(DXHtmlControl parent, DXWebControlBase[] controls);
        private string[] SplitText(string text, Measurer measurer, Font font, StringFormat stringFormat);
        private DXWebControlBase[] ToHtmlControls(string[] strings);

        private bool RightToLeft { get; }

        private float PixWidth { get; }

        private float PixHeight { get; }

        protected virtual string NBSP { get; }

        protected virtual string LineBreak { get; }

        protected virtual string NoLineBreakStart { get; }

        protected virtual string NoLineBreakEnd { get; }

        protected virtual bool DesignateNewLines { get; }
    }
}

