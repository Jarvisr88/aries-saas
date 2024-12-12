namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class TextEditSettings : BaseEditSettings, ISupportTextHighlighting
    {
        public static readonly DependencyProperty EditNonEditableTemplateProperty;
        public static readonly DependencyProperty MaskSaveLiteralProperty;
        public static readonly DependencyProperty MaskShowPlaceHoldersProperty;
        public static readonly DependencyProperty MaskPlaceHolderProperty;
        public static readonly DependencyProperty MaskProperty;
        public static readonly DependencyProperty MaskTypeProperty;
        public static readonly DependencyProperty MaskIgnoreBlankProperty;
        public static readonly DependencyProperty MaskUseAsDisplayFormatProperty;
        public static readonly DependencyProperty MaskBeepOnErrorProperty;
        public static readonly DependencyProperty MaskAutoCompleteProperty;
        public static readonly DependencyProperty MaskCultureProperty;
        public static readonly DependencyProperty AcceptsReturnProperty;
        public static readonly DependencyProperty MaxLengthProperty;
        public static readonly DependencyProperty TextDecorationsProperty;
        public static readonly DependencyProperty CharacterCasingProperty;
        public static readonly DependencyProperty TextTrimmingProperty;
        public static readonly DependencyProperty ShowTooltipForTrimmedTextProperty;
        public static readonly DependencyProperty TrimmedTextToolTipContentTemplateProperty;
        public static readonly DependencyProperty TextWrappingProperty;
        public static readonly DependencyProperty PrintTextWrappingProperty;
        public static readonly DependencyProperty ValidateOnEnterKeyPressedProperty;
        public static readonly DependencyProperty ValidateOnTextInputProperty;
        public static readonly DependencyProperty AllowSpinOnMouseWheelProperty;
        private static readonly DependencyPropertyKey HighlightedTextPropertyKey;
        public static readonly DependencyProperty HighlightedTextProperty;
        private static readonly DependencyPropertyKey HighlightedTextCriteriaPropertyKey;
        public static readonly DependencyProperty HighlightedTextCriteriaProperty;
        public static readonly DependencyProperty SelectAllOnMouseUpProperty;

        static TextEditSettings()
        {
            Type ownerType = typeof(TextEditSettings);
            HighlightedTextCriteriaPropertyKey = DependencyPropertyManager.RegisterReadOnly("HighlightedTextCriteria", typeof(DevExpress.Xpf.Editors.HighlightedTextCriteria), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.HighlightedTextCriteria.StartsWith, new PropertyChangedCallback(TextEditSettings.OnPropertyChanged)));
            HighlightedTextCriteriaProperty = HighlightedTextCriteriaPropertyKey.DependencyProperty;
            HighlightedTextPropertyKey = DependencyPropertyManager.RegisterReadOnly("HighlightedText", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(TextEditSettings.OnPropertyChanged)));
            HighlightedTextProperty = HighlightedTextPropertyKey.DependencyProperty;
            AllowSpinOnMouseWheelProperty = DependencyPropertyManager.Register("AllowSpinOnMouseWheel", typeof(bool), ownerType, new PropertyMetadata(true));
            EditNonEditableTemplateProperty = DependencyPropertyManager.Register("EditNonEditableTemplate", typeof(ControlTemplate), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            MaskSaveLiteralProperty = DependencyPropertyManager.Register("MaskSaveLiteral", typeof(bool), ownerType, new PropertyMetadata(true, new PropertyChangedCallback(TextEditSettings.OnPropertyChanged)));
            MaskShowPlaceHoldersProperty = DependencyPropertyManager.Register("MaskShowPlaceHolders", typeof(bool), ownerType, new PropertyMetadata(true, new PropertyChangedCallback(TextEditSettings.OnPropertyChanged)));
            MaskPlaceHolderProperty = DependencyPropertyManager.Register("MaskPlaceHolder", typeof(char), ownerType, new PropertyMetadata('_', new PropertyChangedCallback(TextEditSettings.OnPropertyChanged)));
            MaskProperty = DependencyPropertyManager.Register("Mask", typeof(string), ownerType, new PropertyMetadata(string.Empty, new PropertyChangedCallback(TextEditSettings.OnPropertyChanged)));
            MaskTypeProperty = DependencyPropertyManager.Register("MaskType", typeof(DevExpress.Xpf.Editors.MaskType), ownerType, new PropertyMetadata(DevExpress.Xpf.Editors.MaskType.None, new PropertyChangedCallback(TextEditSettings.OnMaskTypeChanged)));
            MaskIgnoreBlankProperty = DependencyPropertyManager.Register("MaskIgnoreBlank", typeof(bool), ownerType, new PropertyMetadata(true, new PropertyChangedCallback(TextEditSettings.OnPropertyChanged)));
            MaskUseAsDisplayFormatProperty = DependencyPropertyManager.Register("MaskUseAsDisplayFormat", typeof(bool), ownerType, new PropertyMetadata(false, new PropertyChangedCallback(TextEditSettings.OnPropertyChanged)));
            MaskBeepOnErrorProperty = DependencyPropertyManager.Register("MaskBeepOnError", typeof(bool), ownerType, new PropertyMetadata(false, new PropertyChangedCallback(TextEditSettings.OnPropertyChanged)));
            MaskAutoCompleteProperty = DependencyPropertyManager.Register("MaskAutoComplete", typeof(AutoCompleteType), ownerType, new PropertyMetadata(AutoCompleteType.Default, new PropertyChangedCallback(TextEditSettings.OnPropertyChanged)));
            MaskCultureProperty = DependencyPropertyManager.Register("MaskCulture", typeof(CultureInfo), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(TextEditSettings.OnPropertyChanged)));
            AcceptsReturnProperty = TextEditBase.AcceptsReturnProperty.AddOwner(typeof(TextEditSettings), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            TextWrappingProperty = TextBlock.TextWrappingProperty.AddOwner(typeof(TextEditSettings), new PropertyMetadata(new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            PrintTextWrappingProperty = DependencyPropertyManager.Register("PrintTextWrapping", typeof(System.Windows.TextWrapping?), typeof(TextEditSettings), new PropertyMetadata(null));
            MaxLengthProperty = DependencyPropertyManager.Register("MaxLength", typeof(int), ownerType, new FrameworkPropertyMetadata(0));
            TextDecorationsProperty = DependencyPropertyManager.Register("TextDecorations", typeof(TextDecorationCollection), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
            CharacterCasingProperty = DependencyPropertyManager.Register("CharacterCasing", typeof(System.Windows.Controls.CharacterCasing), ownerType, new FrameworkPropertyMetadata(System.Windows.Controls.CharacterCasing.Normal));
            TextTrimmingProperty = TextEditBase.TextTrimmingProperty.AddOwner(typeof(TextEditSettings), new PropertyMetadata(System.Windows.TextTrimming.CharacterEllipsis, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            ShowTooltipForTrimmedTextProperty = TextEditBase.ShowTooltipForTrimmedTextProperty.AddOwner(typeof(TextEditSettings), new PropertyMetadata(true));
            TrimmedTextToolTipContentTemplateProperty = DependencyPropertyManager.Register("TrimmedTextToolTipContentTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null));
            ValidateOnEnterKeyPressedProperty = DependencyPropertyManager.Register("ValidateOnEnterKeyPressed", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            ValidateOnTextInputProperty = DependencyPropertyManager.Register("ValidateOnTextInput", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            SelectAllOnMouseUpProperty = DependencyPropertyManager.Register("SelectAllOnMouseUp", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
        }

        public TextEditSettings()
        {
            this.MaskTypeCore = this.MaskType;
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            IInplaceBaseEdit edit2 = edit as IInplaceBaseEdit;
            if (edit2 != null)
            {
                edit2.TextTrimming = this.TextTrimming;
                edit2.TextWrapping = this.TextWrapping;
                edit2.HighlightedText = this.HighlightedText;
                edit2.HighlightedTextCriteria = this.HighlightedTextCriteria;
                if (!edit2.HasTextDecorations)
                {
                    edit2.TextDecorations = this.TextDecorations;
                }
                edit2.ShowToolTipForTrimmedText = this.ShowTooltipForTrimmedText;
                edit2.DayDuration = TimeSpanMaskGanttOptions.GetDayDuration(this);
            }
            else
            {
                TextEdit te = edit as TextEdit;
                if (te != null)
                {
                    base.SetValueFromSettings(SelectAllOnMouseUpProperty, () => te.SelectAllOnMouseUp = this.SelectAllOnMouseUp);
                    base.SetValueFromSettings(AllowSpinOnMouseWheelProperty, () => te.AllowSpinOnMouseWheel = this.AllowSpinOnMouseWheel);
                    base.SetValueFromSettings(MaxLengthProperty, () => te.MaxLength = this.MaxLength);
                    base.SetValueFromSettings(AcceptsReturnProperty, () => te.AcceptsReturn = this.AcceptsReturn);
                    base.SetValueFromSettings(TextWrappingProperty, () => te.TextWrapping = this.TextWrapping);
                    base.SetValueFromSettings(PrintTextWrappingProperty, () => te.PrintTextWrapping = this.PrintTextWrapping);
                    base.SetValueFromSettings(TextDecorationsProperty, () => te.TextDecorations = this.TextDecorations);
                    base.SetValueFromSettings(MaskAutoCompleteProperty, () => te.MaskAutoComplete = this.MaskAutoComplete);
                    base.SetValueFromSettings(MaskCultureProperty, () => te.MaskCulture = this.MaskCulture);
                    base.SetValueFromSettings(MaskProperty, () => te.Mask = this.Mask);
                    base.SetValueFromSettings(MaskIgnoreBlankProperty, () => te.MaskIgnoreBlank = this.MaskIgnoreBlank);
                    base.SetValueFromSettings(MaskTypeProperty, () => te.MaskType = this.MaskType);
                    base.SetValueFromSettings(MaskPlaceHolderProperty, () => te.MaskPlaceHolder = this.MaskPlaceHolder);
                    base.SetValueFromSettings(MaskSaveLiteralProperty, () => te.MaskSaveLiteral = this.MaskSaveLiteral);
                    base.SetValueFromSettings(MaskShowPlaceHoldersProperty, () => te.MaskShowPlaceHolders = this.MaskShowPlaceHolders);
                    base.SetValueFromSettings(MaskUseAsDisplayFormatProperty, () => te.MaskUseAsDisplayFormat = this.MaskUseAsDisplayFormat);
                    base.SetValueFromSettings(NumericMaskOptions.AlwaysShowDecimalSeparatorProperty, () => NumericMaskOptions.SetAlwaysShowDecimalSeparator(te, NumericMaskOptions.GetAlwaysShowDecimalSeparator(this)));
                    base.SetValueFromSettings(ValidateOnTextInputProperty, () => te.ValidateOnTextInput = this.ValidateOnTextInput);
                    base.SetValueFromSettings(ValidateOnEnterKeyPressedProperty, () => te.ValidateOnEnterKeyPressed = this.ValidateOnEnterKeyPressed);
                    base.SetValueFromSettings(MaskBeepOnErrorProperty, () => te.MaskBeepOnError = this.MaskBeepOnError);
                    base.SetValueFromSettings(CharacterCasingProperty, () => te.CharacterCasing = this.CharacterCasing);
                    base.SetValueFromSettings(TextTrimmingProperty, () => te.TextTrimming = this.TextTrimming);
                    base.SetValueFromSettings(ShowTooltipForTrimmedTextProperty, () => te.ShowTooltipForTrimmedText = this.ShowTooltipForTrimmedText);
                    base.SetValueFromSettings(TrimmedTextToolTipContentTemplateProperty, () => te.TrimmedTextToolTipContentTemplate = this.TrimmedTextToolTipContentTemplate, () => this.ClearEditorPropertyIfNeeded(te, BaseEdit.TrimmedTextToolTipContentTemplateProperty, TrimmedTextToolTipContentTemplateProperty));
                    base.SetValueFromSettings(HighlightedTextCriteriaProperty, () => te.HighlightedTextCriteria = this.HighlightedTextCriteria);
                    base.SetValueFromSettings(HighlightedTextProperty, () => te.HighlightedText = this.HighlightedText);
                    base.SetValueFromSettings(EditNonEditableTemplateProperty, () => te.EditNonEditableTemplate = this.EditNonEditableTemplate, () => this.ClearEditorPropertyIfNeeded(te, TextEditBase.EditNonEditableTemplateProperty, EditNonEditableTemplateProperty));
                    base.SetValueFromSettings(TimeSpanMaskOptions.AssignValueToEnteredLiteralProperty, () => TimeSpanMaskOptions.SetAssignValueToEnteredLiteral(te, TimeSpanMaskOptions.GetAssignValueToEnteredLiteral(this)));
                    base.SetValueFromSettings(TimeSpanMaskOptions.DefaultPartProperty, () => TimeSpanMaskOptions.SetDefaultPart(te, TimeSpanMaskOptions.GetDefaultPart(this)));
                    base.SetValueFromSettings(TimeSpanMaskOptions.AllowNegativeValueProperty, () => TimeSpanMaskOptions.SetAllowNegativeValue(te, TimeSpanMaskOptions.GetAllowNegativeValue(this)));
                    base.SetValueFromSettings(TimeSpanMaskOptions.InputModeProperty, () => TimeSpanMaskOptions.SetInputMode(te, TimeSpanMaskOptions.GetInputMode(this)));
                    base.SetValueFromSettings(TimeSpanMaskOptions.ChangeNextPartOnCycleValueChangeProperty, () => TimeSpanMaskOptions.SetChangeNextPartOnCycleValueChange(te, TimeSpanMaskOptions.GetChangeNextPartOnCycleValueChange(this)));
                    base.SetValueFromSettings(TimeSpanMaskGanttOptions.DayDurationProperty, () => TimeSpanMaskGanttOptions.SetDayDuration(te, TimeSpanMaskGanttOptions.GetDayDuration(this)));
                }
            }
        }

        protected internal override void AssignViewInfoProperties(IBaseEdit edit, IDefaultEditorViewInfo defaultViewInfo)
        {
            base.AssignViewInfoProperties(edit, defaultViewInfo);
            IInplaceBaseEdit edit2 = edit as IInplaceBaseEdit;
            if (edit2 != null)
            {
                edit2.HasTextDecorations = defaultViewInfo.HasTextDecorations;
            }
        }

        void ISupportTextHighlighting.UpdateHighlightedText(string highlightedText, DevExpress.Xpf.Editors.HighlightedTextCriteria criteria)
        {
            this.HighlightedTextCriteria = criteria;
            this.HighlightedText = highlightedText;
        }

        public override string GetDisplayTextFromEditor(object editValue)
        {
            if (this.MaskTypeCore == DevExpress.Xpf.Editors.MaskType.None)
            {
                return base.GetDisplayTextFromEditor(editValue);
            }
            TextEdit editor = (TextEdit) base.Editor;
            editor.OnEditValueChanged(null, editValue);
            return editor.PropertyProvider.DisplayText;
        }

        protected internal override bool IsActivatingKey(Key key, ModifierKeys modifiers) => 
            !this.IsPasteGesture(key, modifiers) ? base.IsActivatingKey(key, modifiers) : true;

        private static void OnMaskTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TextEditSettings) d).MaskTypeCore = (DevExpress.Xpf.Editors.MaskType) e.NewValue;
            OnPropertyChanged(d, e);
        }

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        protected internal override bool UsesFlatBorderTemplate() => 
            false;

        [Category("Behavior")]
        public bool SelectAllOnMouseUp
        {
            get => 
                (bool) base.GetValue(SelectAllOnMouseUpProperty);
            set => 
                base.SetValue(SelectAllOnMouseUpProperty, value);
        }

        [Category("Behavior")]
        public bool AllowSpinOnMouseWheel
        {
            get => 
                (bool) base.GetValue(AllowSpinOnMouseWheelProperty);
            set => 
                base.SetValue(AllowSpinOnMouseWheelProperty, value);
        }

        [Category("Behavior"), Description("Gets or sets a template that defines the in-place button editor's presentation when the editor is active, but its text field is not editable. This is a dependency property.")]
        public ControlTemplate EditNonEditableTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(EditNonEditableTemplateProperty);
            set => 
                base.SetValue(EditNonEditableTemplateProperty, value);
        }

        [Category("Format"), Description("Gets or sets whether constantly displayed mask characters (literals) are included in an editor's value, for the Simple and Regular mask types. This is a dependency property.")]
        public bool MaskSaveLiteral
        {
            get => 
                (bool) base.GetValue(MaskSaveLiteralProperty);
            set => 
                base.SetValue(MaskSaveLiteralProperty, value);
        }

        [Category("Format"), Description("Gets or sets the automatic completion mode used by the editor in the RegEx mask mode. This is a dependency property.")]
        public AutoCompleteType MaskAutoComplete
        {
            get => 
                (AutoCompleteType) base.GetValue(MaskAutoCompleteProperty);
            set => 
                base.SetValue(MaskAutoCompleteProperty, value);
        }

        [Description("Gets or sets whether placeholders are displayed in a masked editor, for the RegEx mask type. This is a dependency property."), Category("Format")]
        public bool MaskShowPlaceHolders
        {
            get => 
                (bool) base.GetValue(MaskShowPlaceHoldersProperty);
            set => 
                base.SetValue(MaskShowPlaceHoldersProperty, value);
        }

        [Description("Gets or sets the character used as the placeholder in a masked editor, for the Simple, Regular and RegEx mask types. This is a dependency property."), Category("Format")]
        public char MaskPlaceHolder
        {
            get => 
                (char) base.GetValue(MaskPlaceHolderProperty);
            set => 
                base.SetValue(MaskPlaceHolderProperty, value);
        }

        private DevExpress.Xpf.Editors.MaskType MaskTypeCore { get; set; }

        [Category("Format"), Description("Gets or sets the mask type. This is a dependency property.")]
        public DevExpress.Xpf.Editors.MaskType MaskType
        {
            get => 
                (DevExpress.Xpf.Editors.MaskType) base.GetValue(MaskTypeProperty);
            set => 
                base.SetValue(MaskTypeProperty, value);
        }

        [Category("Format"), Description("Gets or sets a mask expression. This is a dependency property.")]
        public string Mask
        {
            get => 
                (string) base.GetValue(MaskProperty);
            set => 
                base.SetValue(MaskProperty, value);
        }

        [Description("Gets or sets whether the editor can lose focus when a value hasn't been entered, for the Simple, Regular and RegEx mask types. This is a dependency property."), Category("Format")]
        public bool MaskIgnoreBlank
        {
            get => 
                (bool) base.GetValue(MaskIgnoreBlankProperty);
            set => 
                base.SetValue(MaskIgnoreBlankProperty, value);
        }

        [Category("Format"), Description("Gets or sets whether display values are still formatted using the mask when the editor is not focused. This is a dependency property.")]
        public bool MaskUseAsDisplayFormat
        {
            get => 
                (bool) base.GetValue(MaskUseAsDisplayFormatProperty);
            set => 
                base.SetValue(MaskUseAsDisplayFormatProperty, value);
        }

        [Category("Format"), Description("Gets or sets whether an editor beeps when an end-user tries to enter an invalid character. This is a dependency property.")]
        public bool MaskBeepOnError
        {
            get => 
                (bool) base.GetValue(MaskBeepOnErrorProperty);
            set => 
                base.SetValue(MaskBeepOnErrorProperty, value);
        }

        [TypeConverter(typeof(CultureInfoConverter)), Category("Format"), Description("Gets or sets the culture whose settings are used by masks. This is a dependency property.")]
        public CultureInfo MaskCulture
        {
            get => 
                (CultureInfo) base.GetValue(MaskCultureProperty);
            set => 
                base.SetValue(MaskCultureProperty, value);
        }

        [Description("Gets or sets whether the text wraps when it reaches the edge of the text box. This is a dependency property."), Category("Behavior")]
        public System.Windows.TextWrapping TextWrapping
        {
            get => 
                (System.Windows.TextWrapping) base.GetValue(TextWrappingProperty);
            set => 
                base.SetValue(TextWrappingProperty, value);
        }

        [Description("Gets or sets whether a cell's value is automatically wrapped when it is printed. This is a dependency property.")]
        public System.Windows.TextWrapping? PrintTextWrapping
        {
            get => 
                (System.Windows.TextWrapping?) base.GetValue(PrintTextWrappingProperty);
            set => 
                base.SetValue(PrintTextWrappingProperty, value);
        }

        [Description("Gets or sets whether the edit value should be validated when pressing the ENTER key. This is a dependency property.")]
        public bool ValidateOnEnterKeyPressed
        {
            get => 
                (bool) base.GetValue(ValidateOnEnterKeyPressedProperty);
            set => 
                base.SetValue(ValidateOnEnterKeyPressedProperty, value);
        }

        [Description("Gets or sets whether the edit value should be validated while typing within the editor's text box. This is a dependency property.")]
        public bool ValidateOnTextInput
        {
            get => 
                (bool) base.GetValue(ValidateOnTextInputProperty);
            set => 
                base.SetValue(ValidateOnTextInputProperty, value);
        }

        [Description("Gets or sets whether an end-user can insert return characters into a text. This is a dependency property."), Category("Behavior")]
        public bool AcceptsReturn
        {
            get => 
                (bool) base.GetValue(AcceptsReturnProperty);
            set => 
                base.SetValue(AcceptsReturnProperty, value);
        }

        [Category("Behavior"), Description("Gets or sets the maximum number of characters an end-user can enter into the editor. This is a dependency property.")]
        public int MaxLength
        {
            get => 
                (int) base.GetValue(MaxLengthProperty);
            set => 
                base.SetValue(MaxLengthProperty, value);
        }

        [Description("Gets or sets a value that specifies the text decorations that are applied to the editor's content. This is a dependency property."), Category("Behavior")]
        public TextDecorationCollection TextDecorations
        {
            get => 
                (TextDecorationCollection) base.GetValue(TextDecorationsProperty);
            set => 
                base.SetValue(TextDecorationsProperty, value);
        }

        [Category("Behavior")]
        public string HighlightedText
        {
            get => 
                (string) base.GetValue(HighlightedTextProperty);
            internal set => 
                base.SetValue(HighlightedTextPropertyKey, value);
        }

        [Category("Behavior")]
        public DevExpress.Xpf.Editors.HighlightedTextCriteria HighlightedTextCriteria
        {
            get => 
                (DevExpress.Xpf.Editors.HighlightedTextCriteria) base.GetValue(HighlightedTextCriteriaProperty);
            internal set => 
                base.SetValue(HighlightedTextCriteriaPropertyKey, value);
        }

        [Category("Behavior"), Description("Gets or sets the character casing applied to the editor's content. This is a dependency property.")]
        public System.Windows.Controls.CharacterCasing CharacterCasing
        {
            get => 
                (System.Windows.Controls.CharacterCasing) base.GetValue(CharacterCasingProperty);
            set => 
                base.SetValue(CharacterCasingProperty, value);
        }

        [SkipPropertyAssertion, Category("Behavior"), Description("Gets or sets the text trimming behavior.This is a dependency property.")]
        public System.Windows.TextTrimming TextTrimming
        {
            get => 
                (System.Windows.TextTrimming) base.GetValue(TextBlock.TextTrimmingProperty);
            set => 
                base.SetValue(TextBlock.TextTrimmingProperty, value);
        }

        [Description("Gets or sets whether to invoke a tooltip for the editor whose content is trimmed.This is a dependency property."), Category("Behavior")]
        public bool ShowTooltipForTrimmedText
        {
            get => 
                (bool) base.GetValue(ShowTooltipForTrimmedTextProperty);
            set => 
                base.SetValue(ShowTooltipForTrimmedTextProperty, value);
        }

        [Category("Behavior"), Description("Gets or sets the data template used to display the content of a tooltip invoked for the editor whose text is trimmed. This is a dependency property.")]
        public DataTemplate TrimmedTextToolTipContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(TrimmedTextToolTipContentTemplateProperty);
            set => 
                base.SetValue(TrimmedTextToolTipContentTemplateProperty, value);
        }
    }
}

