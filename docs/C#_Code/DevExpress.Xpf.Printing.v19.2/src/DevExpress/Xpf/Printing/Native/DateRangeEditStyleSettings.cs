namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Editors;
    using System;

    internal class DateRangeEditStyleSettings : PopupBaseEditStyleSettings
    {
        protected override PopupCloseMode GetClosePopupOnClickMode(PopupBaseEdit editor) => 
            PopupCloseMode.Normal;

        public override bool GetIsTextEditable(ButtonEdit editor) => 
            false;
    }
}

