namespace DevExpress.Xpf.Editors
{
    using System;

    public class SolidColorBrushEditStyleSettings : BrushEditStyleSettingsBase
    {
        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            BrushEditBase base2 = editor as BrushEditBase;
            if (base2 != null)
            {
                base2.BrushType = BrushType.SolidColorBrush;
            }
        }
    }
}

