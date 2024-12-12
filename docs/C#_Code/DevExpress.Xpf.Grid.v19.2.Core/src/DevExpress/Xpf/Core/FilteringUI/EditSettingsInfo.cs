namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class EditSettingsInfo : ImmutableObject
    {
        private EditSettingsInfo(BaseEditSettings editSettings, bool isUserDefined)
        {
            this.<Settings>k__BackingField = editSettings;
            this.<IsUserDefined>k__BackingField = isUserDefined;
        }

        internal static EditSettingsInfo CreateDefault(BaseEditSettings editSettings) => 
            new EditSettingsInfo(editSettings, false);

        internal static EditSettingsInfo CreateUserDefined(BaseEditSettings editSettings) => 
            new EditSettingsInfo(editSettings, true);

        public override bool Equals(object obj)
        {
            EditSettingsInfo info = obj as EditSettingsInfo;
            return ((info != null) && (EqualityComparer<BaseEditSettings>.Default.Equals(this.Settings, info.Settings) && (this.IsUserDefined == info.IsUserDefined)));
        }

        public override int GetHashCode() => 
            (((-627859773 * -1521134295) + EqualityComparer<BaseEditSettings>.Default.GetHashCode(this.Settings)) * -1521134295) + this.IsUserDefined.GetHashCode();

        internal BaseEditSettings Settings { get; }

        internal bool IsUserDefined { get; }
    }
}

