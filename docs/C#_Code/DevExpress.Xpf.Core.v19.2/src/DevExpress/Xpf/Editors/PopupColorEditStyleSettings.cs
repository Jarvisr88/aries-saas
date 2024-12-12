namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Settings;
    using System;

    public class PopupColorEditStyleSettings : PopupBaseEditStyleSettings
    {
        public override bool GetIsTextEditable(ButtonEdit editor) => 
            ((PopupColorEdit) editor).DisplayMode == PopupColorEditDisplayMode.Text;
    }
}

