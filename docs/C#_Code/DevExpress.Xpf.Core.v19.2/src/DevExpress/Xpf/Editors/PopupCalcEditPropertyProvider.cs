namespace DevExpress.Xpf.Editors
{
    using System;

    public class PopupCalcEditPropertyProvider : PopupBaseEditPropertyProvider
    {
        public PopupCalcEditPropertyProvider(PopupCalcEdit editor) : base(editor)
        {
        }

        protected override bool GetFocusPopupOnOpenInternal() => 
            true;
    }
}

