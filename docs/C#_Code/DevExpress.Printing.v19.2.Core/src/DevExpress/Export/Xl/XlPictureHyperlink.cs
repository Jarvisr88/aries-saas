namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlPictureHyperlink : XlHyperlinkBase
    {
        public XlPictureHyperlink Clone() => 
            new XlPictureHyperlink { 
                TargetUri = base.TargetUri,
                Tooltip = base.Tooltip,
                TargetFrame = this.TargetFrame
            };

        public string TargetFrame { get; set; }

        public bool IsExternal =>
            !string.IsNullOrEmpty(base.TargetUri) ? (base.TargetUri.IndexOf('#') != 0) : false;
    }
}

