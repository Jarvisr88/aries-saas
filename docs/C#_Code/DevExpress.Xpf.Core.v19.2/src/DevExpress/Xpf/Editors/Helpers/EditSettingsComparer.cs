namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;

    public static class EditSettingsComparer
    {
        public static bool IsCompatibleEditSettings(IBaseEdit edit, BaseEditSettings settings) => 
            IsCompatibleEditSettings(edit.Settings, settings);

        public static bool IsCompatibleEditSettings(BaseEditSettings settings1, BaseEditSettings settings2) => 
            settings1.IsCompatibleWith(settings2);
    }
}

