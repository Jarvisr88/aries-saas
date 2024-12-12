namespace DevExpress.Xpf.Editors
{
    using System;

    public class MemoEditStyleSettings : PopupBaseEditStyleSettings
    {
        protected internal override PopupCloseMode GetClosePopupOnClickMode(PopupBaseEdit editor) => 
            PopupCloseMode.Normal;

        public override bool GetIsTextEditable(ButtonEdit editor) => 
            false;
    }
}

