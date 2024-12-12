namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class JustifyHtmlStringCreator : HtmlStringCreator
    {
        public JustifyHtmlStringCreator(StringFormat stringFormat, float width, float height);
        protected override void ApplyAlignment(DXHtmlControl control);
        protected override void PostProcessLine(string str, List<DXWebControlBase> result);

        protected override string NBSP { get; }

        protected override string LineBreak { get; }

        protected override bool DesignateNewLines { get; }
    }
}

