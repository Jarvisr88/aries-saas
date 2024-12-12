namespace DevExpress.Xpf.Editors
{
    using System;

    public class PopupBrushEditStyleSettings : PopupBrushEditStyleSettingsBase
    {
        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            PopupBrushEditBase base2 = editor as PopupBrushEditBase;
            if (base2 != null)
            {
                base2.BrushType = BrushType.AutoDetect;
            }
        }

        public override BaseEditStyleSettings CreateBrushEditStyleSettings() => 
            null;

        protected internal override PopupCloseMode GetClosePopupOnClickMode(PopupBaseEdit editor) => 
            PopupCloseMode.Normal;
    }
}

