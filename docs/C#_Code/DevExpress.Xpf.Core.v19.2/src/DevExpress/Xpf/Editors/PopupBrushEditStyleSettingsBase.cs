namespace DevExpress.Xpf.Editors
{
    using System;

    public abstract class PopupBrushEditStyleSettingsBase : PopupBaseEditStyleSettings
    {
        protected PopupBrushEditStyleSettingsBase()
        {
        }

        public abstract BaseEditStyleSettings CreateBrushEditStyleSettings();
        public override bool GetIsTextEditable(ButtonEdit editor) => 
            false;

        public override PopupFooterButtons GetPopupFooterButtons(PopupBaseEdit editor) => 
            PopupFooterButtons.OkCancel;

        protected internal override bool GetShowSizeGrip(PopupBaseEdit editor) => 
            true;
    }
}

