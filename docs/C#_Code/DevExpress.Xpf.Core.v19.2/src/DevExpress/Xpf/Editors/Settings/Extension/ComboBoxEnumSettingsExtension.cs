namespace DevExpress.Xpf.Editors.Settings.Extension
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;

    public class ComboBoxEnumSettingsExtension : PopupBaseSettingsExtension
    {
        public ComboBoxEnumSettingsExtension()
        {
            base.HorizontalContentAlignment = EditSettingsHorizontalAlignment.Left;
            this.UseUnderlyingEnumValue = true;
        }

        protected override PopupBaseEditSettings CreatePopupBaseEditSettings()
        {
            ComboBoxEditSettings settings1 = new ComboBoxEditSettings();
            settings1.ItemsSource = EnumHelper.GetEnumSource(this.EnumType, this.UseUnderlyingEnumValue, null, false, EnumMembersSortMode.Default, true, true, null);
            settings1.ValueMember = EnumSourceHelperCore.ValueMemberName;
            settings1.DisplayMember = EnumSourceHelperCore.DisplayMemberName;
            return settings1;
        }

        protected override bool? GetIsTextEditable() => 
            false;

        public Type EnumType { get; set; }

        public bool UseUnderlyingEnumValue { get; set; }
    }
}

