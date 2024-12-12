namespace DevExpress.Xpf.Editors
{
    using System;

    public class PopupSolidColorBrushEditStyleSettings : PopupBrushEditStyleSettingsBase
    {
        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            PopupBrushEditBase base2 = editor as PopupBrushEditBase;
            if (base2 != null)
            {
                base2.BrushType = BrushType.SolidColorBrush;
            }
        }

        public override BaseEditStyleSettings CreateBrushEditStyleSettings() => 
            new SolidColorBrushEditStyleSettings();
    }
}

