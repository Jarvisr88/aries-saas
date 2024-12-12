namespace DevExpress.Xpf.Editors
{
    using System;

    public class PopupLinearGradientBrushEditStyleSettings : PopupBrushEditStyleSettingsBase
    {
        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            PopupBrushEditBase base2 = editor as PopupBrushEditBase;
            if (base2 != null)
            {
                base2.BrushType = BrushType.LinearGradientBrush;
                base2.IsTextEditable = false;
            }
        }

        public override BaseEditStyleSettings CreateBrushEditStyleSettings() => 
            null;
    }
}

