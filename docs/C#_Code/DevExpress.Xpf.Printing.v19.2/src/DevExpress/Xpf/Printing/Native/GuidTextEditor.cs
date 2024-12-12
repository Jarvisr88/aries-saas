namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows.Input;

    public class GuidTextEditor : TextEdit
    {
        protected override TextInputSettingsBase CreateTextInputSettings() => 
            new GuidTextEditSettings(this);

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            base.MaskType = MaskType.RegEx;
            base.MaskUseAsDisplayFormat = true;
            base.Mask = "[0-9a-fA-F]{8}(-[0-9a-fA-F]{4}){3}-[0-9a-fA-F]{12}";
        }

        private class GuidTextEditSettings : TextInputMaskSettings
        {
            public GuidTextEditSettings(TextEditBase editor) : base(editor)
            {
            }

            protected override void ProcessPreviewKeyDown(KeyEventArgs e)
            {
                base.ProcessPreviewKeyDown(e);
                if (base.OwnerEdit.AllowNullInput && (string.IsNullOrEmpty(base.OwnerEdit.EditValue as string) && !(base.OwnerEdit.EditValue is Guid)))
                {
                    base.OwnerEdit.EditValue = null;
                }
            }
        }
    }
}

