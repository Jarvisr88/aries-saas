namespace DevExpress.Xpf.Editors
{
    using System;

    public class HyperlinkEditPropertyProvider : ActualPropertyProvider
    {
        public HyperlinkEditPropertyProvider(HyperlinkEdit editor) : base(editor)
        {
        }

        public override bool CalcSuppressFeatures() => 
            false;
    }
}

