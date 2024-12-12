namespace DevExpress.Xpf.Editors
{
    using System;

    public class PopupImageEditPropertyProvider : PopupBaseEditPropertyProvider
    {
        public PopupImageEditPropertyProvider(PopupImageEdit editor) : base(editor)
        {
        }

        public override bool CalcSuppressFeatures() => 
            false;
    }
}

