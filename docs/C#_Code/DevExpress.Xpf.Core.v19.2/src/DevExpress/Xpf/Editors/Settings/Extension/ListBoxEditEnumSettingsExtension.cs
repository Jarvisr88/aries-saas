namespace DevExpress.Xpf.Editors.Settings.Extension
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    public class ListBoxEditEnumSettingsExtension : BaseSettingsExtension
    {
        protected override BaseEditSettings CreateEditSettings()
        {
            ListBoxEditSettings settings1 = new ListBoxEditSettings();
            settings1.ItemsSource = EnumHelper.GetEnumSource(this.EnumType, true, null, false, EnumMembersSortMode.Default, true, true, null);
            settings1.ValueMember = EnumSourceHelperCore.ValueMemberName;
            settings1.DisplayMember = EnumSourceHelperCore.DisplayMemberName;
            settings1.SelectionMode = SelectionMode.Single;
            return settings1;
        }

        public Type EnumType { get; set; }
    }
}

