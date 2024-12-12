namespace DevExpress.Xpf.Editors.Settings.Extension
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class TextSettingsExtension : BaseSettingsExtension
    {
        public TextSettingsExtension()
        {
            this.SelectAllOnMouseUp = (bool) TextEditSettings.SelectAllOnMouseUpProperty.DefaultMetadata.DefaultValue;
            this.AllowSpinOnMouseWheel = (bool) TextEditSettings.AllowSpinOnMouseWheelProperty.DefaultMetadata.DefaultValue;
            this.AcceptsReturn = (bool) TextEditSettings.AcceptsReturnProperty.DefaultMetadata.DefaultValue;
            this.CharacterCasing = (System.Windows.Controls.CharacterCasing) TextEditSettings.CharacterCasingProperty.DefaultMetadata.DefaultValue;
            this.MaxLength = (int) TextEditSettings.MaxLengthProperty.DefaultMetadata.DefaultValue;
            this.DisplayFormat = (string) BaseEditSettings.DisplayFormatProperty.DefaultMetadata.DefaultValue;
            this.TextWrapping = (System.Windows.TextWrapping) TextEditSettings.TextWrappingProperty.DefaultMetadata.DefaultValue;
            this.TextTrimming = (System.Windows.TextTrimming) TextEditSettings.TextTrimmingProperty.GetMetadata(typeof(TextEditSettings)).DefaultValue;
            this.ShowTooltipForTrimmedText = (bool) TextEditSettings.ShowTooltipForTrimmedTextProperty.GetMetadata(typeof(TextEditSettings)).DefaultValue;
            this.MaskSaveLiteral = (bool) TextEditSettings.MaskSaveLiteralProperty.DefaultMetadata.DefaultValue;
            this.MaskShowPlaceHolders = (bool) TextEditSettings.MaskShowPlaceHoldersProperty.DefaultMetadata.DefaultValue;
            this.MaskPlaceHolder = (char) TextEditSettings.MaskPlaceHolderProperty.DefaultMetadata.DefaultValue;
            this.Mask = (string) TextEditSettings.MaskProperty.DefaultMetadata.DefaultValue;
            this.MaskType = (DevExpress.Xpf.Editors.MaskType) TextEditSettings.MaskTypeProperty.DefaultMetadata.DefaultValue;
            this.MaskIgnoreBlank = (bool) TextEditSettings.MaskIgnoreBlankProperty.DefaultMetadata.DefaultValue;
            this.MaskUseAsDisplayFormat = (bool) TextEditSettings.MaskUseAsDisplayFormatProperty.DefaultMetadata.DefaultValue;
            this.MaskBeepOnError = (bool) TextEditSettings.MaskBeepOnErrorProperty.DefaultMetadata.DefaultValue;
            this.MaskAutoComplete = (AutoCompleteType) TextEditSettings.MaskAutoCompleteProperty.DefaultMetadata.DefaultValue;
            this.MaskCulture = (CultureInfo) TextEditSettings.MaskCultureProperty.DefaultMetadata.DefaultValue;
            this.ValidateOnTextInput = (bool) TextEditSettings.ValidateOnTextInputProperty.DefaultMetadata.DefaultValue;
        }

        protected sealed override BaseEditSettings CreateEditSettings()
        {
            TextEditSettings settings = this.CreateTextEditSettings();
            settings.SelectAllOnMouseUp = this.SelectAllOnMouseUp;
            settings.AllowSpinOnMouseWheel = this.AllowSpinOnMouseWheel;
            settings.DisplayFormat = this.DisplayFormat;
            settings.TextWrapping = this.TextWrapping;
            settings.TextTrimming = this.TextTrimming;
            settings.ShowTooltipForTrimmedText = this.ShowTooltipForTrimmedText;
            settings.DisplayTextConverter = this.DisplayTextConverter;
            settings.AcceptsReturn = this.AcceptsReturn;
            settings.MaxLength = this.MaxLength;
            settings.CharacterCasing = this.CharacterCasing;
            settings.Mask = this.Mask;
            settings.MaskAutoComplete = this.MaskAutoComplete;
            settings.MaskBeepOnError = this.MaskBeepOnError;
            settings.MaskCulture = this.MaskCulture;
            settings.MaskIgnoreBlank = this.MaskIgnoreBlank;
            settings.MaskPlaceHolder = this.MaskPlaceHolder;
            settings.MaskSaveLiteral = this.MaskSaveLiteral;
            settings.MaskShowPlaceHolders = this.MaskShowPlaceHolders;
            settings.MaskType = this.MaskType;
            settings.MaskUseAsDisplayFormat = this.MaskUseAsDisplayFormat;
            settings.TextDecorations = this.TextDecorations;
            settings.ValidateOnTextInput = this.ValidateOnTextInput;
            settings.DisplayFormat = this.DisplayFormat;
            return settings;
        }

        protected virtual TextEditSettings CreateTextEditSettings() => 
            new TextEditSettings();

        public bool AllowSpinOnMouseWheel { get; set; }

        public bool AcceptsReturn { get; set; }

        public int MaxLength { get; set; }

        public System.Windows.Controls.CharacterCasing CharacterCasing { get; set; }

        public string EditFormat { get; set; }

        public System.Windows.TextWrapping TextWrapping { get; set; }

        public System.Windows.TextTrimming TextTrimming { get; set; }

        public bool ShowTooltipForTrimmedText { get; set; }

        public IValueConverter DisplayTextConverter { get; set; }

        public bool MaskSaveLiteral { get; set; }

        public bool MaskShowPlaceHolders { get; set; }

        public char MaskPlaceHolder { get; set; }

        public string Mask { get; set; }

        public DevExpress.Xpf.Editors.MaskType MaskType { get; set; }

        public bool MaskIgnoreBlank { get; set; }

        public bool MaskUseAsDisplayFormat { get; set; }

        public bool MaskBeepOnError { get; set; }

        public AutoCompleteType MaskAutoComplete { get; set; }

        [TypeConverter(typeof(CultureInfoConverter))]
        public CultureInfo MaskCulture { get; set; }

        public TextDecorationCollection TextDecorations { get; set; }

        public bool ValidateOnTextInput { get; set; }

        public string DisplayFormat { get; set; }

        public bool SelectAllOnMouseUp { get; set; }
    }
}

