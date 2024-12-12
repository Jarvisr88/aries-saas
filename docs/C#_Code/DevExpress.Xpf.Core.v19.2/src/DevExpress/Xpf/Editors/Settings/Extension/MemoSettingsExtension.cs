namespace DevExpress.Xpf.Editors.Settings.Extension
{
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class MemoSettingsExtension : PopupBaseSettingsExtension
    {
        public MemoSettingsExtension()
        {
            base.IsTextEditable = false;
            base.PopupFooterButtons = 1;
            base.ShowSizeGrip = true;
            base.PopupMinHeight = 100.0;
            this.MemoTextWrapping = (TextWrapping) MemoEditSettings.MemoTextWrappingProperty.DefaultMetadata.DefaultValue;
            this.MemoAcceptsReturn = (bool) MemoEditSettings.MemoAcceptsReturnProperty.DefaultMetadata.DefaultValue;
            this.MemoAcceptsTab = (bool) MemoEditSettings.MemoAcceptsTabProperty.DefaultMetadata.DefaultValue;
            this.MemoHorizontalScrollBarVisibility = (ScrollBarVisibility) MemoEditSettings.MemoHorizontalScrollBarVisibilityProperty.DefaultMetadata.DefaultValue;
            this.MemoVerticalScrollBarVisibility = (ScrollBarVisibility) MemoEditSettings.MemoVerticalScrollBarVisibilityProperty.DefaultMetadata.DefaultValue;
            this.ShowIcon = (bool) MemoEditSettings.ShowIconProperty.DefaultMetadata.DefaultValue;
        }

        protected virtual MemoEditSettings CreateMemoEditSettings() => 
            new MemoEditSettings();

        protected sealed override PopupBaseEditSettings CreatePopupBaseEditSettings()
        {
            MemoEditSettings settings = this.CreateMemoEditSettings();
            settings.MemoTextWrapping = this.MemoTextWrapping;
            settings.MemoAcceptsReturn = this.MemoAcceptsReturn;
            settings.MemoAcceptsTab = this.MemoAcceptsTab;
            settings.MemoHorizontalScrollBarVisibility = this.MemoHorizontalScrollBarVisibility;
            settings.MemoVerticalScrollBarVisibility = this.MemoVerticalScrollBarVisibility;
            settings.ShowIcon = this.ShowIcon;
            return settings;
        }

        public TextWrapping MemoTextWrapping { get; set; }

        public bool MemoAcceptsReturn { get; set; }

        public bool MemoAcceptsTab { get; set; }

        public ScrollBarVisibility MemoHorizontalScrollBarVisibility { get; set; }

        public ScrollBarVisibility MemoVerticalScrollBarVisibility { get; set; }

        public bool ShowIcon { get; set; }
    }
}

