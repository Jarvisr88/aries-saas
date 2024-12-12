namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using System;

    internal static class EditSettingsInfoFactory
    {
        public static EditSettingsInfo Default(FilteringColumn column) => 
            EditSettingsInfo.CreateDefault(FilterModelHelper.GetSupportedEditSettings(column));
    }
}

