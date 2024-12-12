namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Automation;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Validation.Native;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Input;

    [DXToolboxBrowsable(DXToolboxItemKind.Free), LicenseProvider(typeof(DX_WPFEditors_LicenseProvider))]
    public class TextEdit : TextEditBase, ISupportTextHighlighting
    {
        public static readonly DependencyProperty MaskSaveLiteralProperty;
        public static readonly DependencyProperty MaskShowPlaceHoldersProperty;
        public static readonly DependencyProperty MaskPlaceHolderProperty;
        public static readonly DependencyProperty MaskProperty;
        public static readonly DependencyProperty MaskTypeProperty;
        public static readonly DependencyProperty MaskIgnoreBlankProperty;
        public static readonly DependencyProperty MaskUseAsDisplayFormatProperty;
        public static readonly DependencyProperty MaskAutoCompleteProperty;
        public static readonly DependencyProperty MaskCultureProperty;
        public static readonly DependencyProperty TextDecorationsProperty;
        public static readonly DependencyProperty AllowSpinOnMouseWheelProperty;
        private static readonly DependencyPropertyKey HighlightedTextPropertyKey;
        public static readonly DependencyProperty HighlightedTextProperty;
        private static readonly DependencyPropertyKey HighlightedTextCriteriaPropertyKey;
        public static readonly DependencyProperty HighlightedTextCriteriaProperty;
        public static readonly DependencyProperty ShowNullTextIfFocusedProperty;
        public static readonly RoutedEvent SpinEvent;
        public static readonly DependencyProperty CharacterCasingProperty;
        public static readonly DependencyProperty MaskBeepOnErrorProperty;

        public event SpinEventHandler Spin
        {
            add
            {
                base.AddHandler(SpinEvent, value);
            }
            remove
            {
                base.RemoveHandler(SpinEvent, value);
            }
        }

        static TextEdit()
        {
            Type ownerType = typeof(TextEdit);
            HighlightedTextCriteriaPropertyKey = DependencyPropertyManager.RegisterReadOnly("HighlightedTextCriteria", typeof(DevExpress.Xpf.Editors.HighlightedTextCriteria), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.HighlightedTextCriteria.StartsWith, (d, e) => ((TextEdit) d).HighlightedTextCriteriaChanged((DevExpress.Xpf.Editors.HighlightedTextCriteria) e.NewValue)));
            HighlightedTextCriteriaProperty = HighlightedTextCriteriaPropertyKey.DependencyProperty;
            HighlightedTextPropertyKey = DependencyPropertyManager.RegisterReadOnly("HighlightedText", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty, (d, e) => ((TextEdit) d).HighlightedTextChanged((string) e.NewValue)));
            HighlightedTextProperty = HighlightedTextPropertyKey.DependencyProperty;
            AllowSpinOnMouseWheelProperty = DependencyPropertyManager.Register("AllowSpinOnMouseWheel", typeof(bool), ownerType, new PropertyMetadata(true));
            MaskSaveLiteralProperty = DependencyPropertyManager.Register("MaskSaveLiteral", typeof(bool), ownerType, new PropertyMetadata(true, new PropertyChangedCallback(TextEdit.OnMaskPropertyChanged)));
            MaskShowPlaceHoldersProperty = DependencyPropertyManager.Register("MaskShowPlaceHolders", typeof(bool), ownerType, new PropertyMetadata(true, new PropertyChangedCallback(TextEdit.OnMaskPropertyChanged)));
            MaskPlaceHolderProperty = DependencyPropertyManager.Register("MaskPlaceHolder", typeof(char), ownerType, new PropertyMetadata('_', new PropertyChangedCallback(TextEdit.OnMaskPropertyChanged)));
            MaskProperty = DependencyPropertyManager.Register("Mask", typeof(string), ownerType, new PropertyMetadata(string.Empty, new PropertyChangedCallback(TextEdit.OnMaskPropertyChanged)));
            MaskTypeProperty = DependencyPropertyManager.Register("MaskType", typeof(DevExpress.Xpf.Editors.MaskType), ownerType, new PropertyMetadata(DevExpress.Xpf.Editors.MaskType.None, new PropertyChangedCallback(TextEdit.OnMaskTypePropertyChanged), new CoerceValueCallback(TextEdit.OnCoerceMaskType)));
            MaskIgnoreBlankProperty = DependencyPropertyManager.Register("MaskIgnoreBlank", typeof(bool), ownerType, new PropertyMetadata(true, new PropertyChangedCallback(TextEdit.OnMaskPropertyChanged)));
            MaskUseAsDisplayFormatProperty = DependencyPropertyManager.Register("MaskUseAsDisplayFormat", typeof(bool), ownerType, new PropertyMetadata(false, new PropertyChangedCallback(TextEdit.OnMaskPropertyChanged)));
            MaskAutoCompleteProperty = DependencyPropertyManager.Register("MaskAutoComplete", typeof(AutoCompleteType), ownerType, new PropertyMetadata(AutoCompleteType.Default, new PropertyChangedCallback(TextEdit.OnMaskPropertyChanged)));
            MaskCultureProperty = DependencyPropertyManager.Register("MaskCulture", typeof(CultureInfo), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(TextEdit.OnMaskPropertyChanged)));
            TextDecorationsProperty = DependencyPropertyManager.Register("TextDecorations", typeof(TextDecorationCollection), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(TextEdit.OnTextDecorationsChanged)));
            SpinEvent = EventManager.RegisterRoutedEvent("Spin", RoutingStrategy.Direct, typeof(SpinEventHandler), ownerType);
            MaskBeepOnErrorProperty = DependencyPropertyManager.Register("MaskBeepOnError", typeof(bool), ownerType, new PropertyMetadata(false, new PropertyChangedCallback(TextEdit.OnMaskPropertyChanged)));
            CharacterCasingProperty = DependencyPropertyManager.Register("CharacterCasing", typeof(System.Windows.Controls.CharacterCasing), ownerType, new FrameworkPropertyMetadata(System.Windows.Controls.CharacterCasing.Normal, (d, e) => ((TextEdit) d).OnCharacterCasingChanged((System.Windows.Controls.CharacterCasing) e.NewValue)));
            ShowNullTextIfFocusedProperty = DependencyProperty.Register("ShowNullTextIfFocused", typeof(bool), ownerType, new PropertyMetadata(false, (o, args) => ((TextEdit) o).ShowNullTextIfFocusedChanged((bool) args.OldValue, (bool) args.NewValue)));
        }

        public TextEdit()
        {
            this.SetDefaultStyleKey(typeof(TextEdit));
            this.<SpinUpCommand>k__BackingField = DelegateCommandFactory.Create<object>(parameter => this.SpinUp(), new Func<object, bool>(this.CanSpinUp), false);
            this.<SpinDownCommand>k__BackingField = DelegateCommandFactory.Create<object>(parameter => this.SpinDown(), new Func<object, bool>(this.CanSpinDown), false);
        }

        private bool CanSpinDown(object parameter) => 
            base.EditStrategy.CanSpinDown();

        private bool CanSpinUp(object parameter) => 
            base.EditStrategy.CanSpinUp();

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new TextEditPropertyProvider(this);

        protected override EditBoxWrapper CreateEditBoxWrapper() => 
            new TextBoxWrapper(this);

        protected override EditStrategyBase CreateEditStrategy()
        {
            Func<TextInputSettingsBase, EditStrategyBase> evaluator = <>c.<>9__110_0;
            if (<>c.<>9__110_0 == null)
            {
                Func<TextInputSettingsBase, EditStrategyBase> local1 = <>c.<>9__110_0;
                evaluator = <>c.<>9__110_0 = x => x.CreateEditStrategy();
            }
            return base.TextInputSettings.Return<TextInputSettingsBase, EditStrategyBase>(evaluator, () => this.CreateEditStrategy(null));
        }

        protected internal EditStrategyBase CreateEditStrategy(object parameter) => 
            new TextEditStrategy(this);

        protected internal override TextInputSettingsBase CreateTextInputSettings() => 
            (this.MaskType == DevExpress.Xpf.Editors.MaskType.None) ? ((TextInputSettingsBase) new TextInputSettings(this)) : ((TextInputSettingsBase) new TextInputMaskSettings(this));

        void ISupportTextHighlighting.UpdateHighlightedText(string highlightedText, DevExpress.Xpf.Editors.HighlightedTextCriteria criteria)
        {
            this.HighlightedText = highlightedText;
            this.HighlightedTextCriteria = criteria;
        }

        protected internal override System.Windows.Controls.CharacterCasing GetActualCharactedCasing() => 
            this.PropertyProvider.CharacterCasing;

        public int GetCharacterIndexFromLineIndex(int lineIndex) => 
            base.EditBox.GetCharacterIndexFromLineIndex(lineIndex);

        public int GetCharacterIndexFromPoint(Point point, bool snapToText) => 
            base.EditBox.GetCharacterIndexFromPoint(point, snapToText);

        public int GetFirstVisibleLineIndex() => 
            base.EditBox.GetFirstVisibleLineIndex();

        public int GetLastVisibleLineIndex() => 
            base.EditBox.GetLastVisibleLineIndex();

        public int GetLineIndexFromCharacterIndex(int charIndex) => 
            base.EditBox.GetLineIndexFromCharacterIndex(charIndex);

        public int GetLineLength(int lineIndex) => 
            base.EditBox.GetLineLength(lineIndex);

        public string GetLineText(int lineIndex) => 
            base.EditBox.GetLineText(lineIndex);

        protected override TextDecorationCollection GetPrintTextDecorations() => 
            (this.TextDecorations == null) ? base.GetPrintTextDecorations() : this.TextDecorations;

        protected internal virtual DevExpress.Xpf.Editors.MaskType[] GetSupportedMaskTypes() => 
            new DevExpress.Xpf.Editors.MaskType[] { DevExpress.Xpf.Editors.MaskType.None, DevExpress.Xpf.Editors.MaskType.DateTime, DevExpress.Xpf.Editors.MaskType.DateTimeAdvancingCaret, DevExpress.Xpf.Editors.MaskType.Numeric, DevExpress.Xpf.Editors.MaskType.RegEx, DevExpress.Xpf.Editors.MaskType.Regular, DevExpress.Xpf.Editors.MaskType.Simple, DevExpress.Xpf.Editors.MaskType.TimeSpan, DevExpress.Xpf.Editors.MaskType.TimeSpanAdvancingCaret };

        protected virtual void HighlightedTextChanged(string text)
        {
            base.EditStrategy.HighlightedTextChanged(text);
        }

        protected virtual void HighlightedTextCriteriaChanged(DevExpress.Xpf.Editors.HighlightedTextCriteria criteria)
        {
            base.EditStrategy.HighlightedTextCriteriaChanged(criteria);
        }

        protected internal virtual void MaskPropertiesChanged()
        {
            if (!base.IsInSupportInitializing)
            {
                this.UpdateTextInputSettings();
                base.EditStrategy.SyncWithValue();
                this.DoValidate();
            }
        }

        protected internal virtual void MaskTypeChanged(DevExpress.Xpf.Editors.MaskType maskType)
        {
            this.PropertyProvider.SetMaskType(maskType);
            if (!base.IsInSupportInitializing)
            {
                this.UpdateTextInputSettings();
                base.EditStrategy.SyncWithValue();
            }
        }

        protected override void OnAllowNullInputChanged()
        {
            base.OnAllowNullInputChanged();
            this.MaskPropertiesChanged();
        }

        protected virtual void OnCharacterCasingChanged(System.Windows.Controls.CharacterCasing characterCasing)
        {
            this.PropertyProvider.SetCharacterCasing(characterCasing);
            base.EditStrategy.UpdateDisplayText();
        }

        protected virtual object OnCoerceMaskType(DevExpress.Xpf.Editors.MaskType maskType) => 
            base.EditStrategy.CoerceMaskType(maskType);

        private static object OnCoerceMaskType(DependencyObject d, object maskType) => 
            ((TextEdit) d).OnCoerceMaskType((DevExpress.Xpf.Editors.MaskType) maskType);

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new TextEditAutomationPeer(this);

        protected override void OnEditCoreAssigned()
        {
            this.SyncTextBlockTextDecorations();
            base.OnEditCoreAssigned();
        }

        private static void OnMaskPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TextEdit) d).MaskPropertiesChanged();
        }

        private static void OnMaskTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TextEdit) d).MaskTypeChanged((DevExpress.Xpf.Editors.MaskType) e.NewValue);
        }

        protected virtual void OnTextDecorationsChanged()
        {
            this.SyncTextBlockTextDecorations();
        }

        private static void OnTextDecorationsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TextEdit) d).OnTextDecorationsChanged();
        }

        public void Select(int start, int length)
        {
            base.EditStrategy.Select(start, length);
        }

        protected virtual void ShowNullTextIfFocusedChanged(bool oldValue, bool newValue)
        {
            this.PropertyProvider.SetShowNullTextIfFocused(newValue);
        }

        public void SpinDown()
        {
            base.EditStrategy.PerformSpinDown();
            base.EditStrategy.FlushPendingEditActions(UpdateEditorSource.TextInput);
        }

        public void SpinUp()
        {
            base.EditStrategy.PerformSpinUp();
            base.EditStrategy.FlushPendingEditActions(UpdateEditorSource.TextInput);
        }

        private void SyncTextBlockTextDecorations()
        {
            base.SyncTextBlockProperty(this.TextDecorations, TextBlock.TextDecorationsProperty);
        }

        protected internal override Type StyleSettingsType =>
            typeof(TextEditStyleSettings);

        private TextEditPropertyProvider PropertyProvider =>
            base.PropertyProvider as TextEditPropertyProvider;

        [Category("Behavior")]
        public bool AllowSpinOnMouseWheel
        {
            get => 
                (bool) base.GetValue(AllowSpinOnMouseWheelProperty);
            set => 
                base.SetValue(AllowSpinOnMouseWheelProperty, value);
        }

        [Description("Gets or sets whether constantly displayed mask characters (literals) are included in an editor's value, for the Simple and Regular mask types. This is a dependency property."), Category("Mask")]
        public bool MaskSaveLiteral
        {
            get => 
                (bool) base.GetValue(MaskSaveLiteralProperty);
            set => 
                base.SetValue(MaskSaveLiteralProperty, value);
        }

        [Description("Gets or sets the automatic completion mode used by the editor in the RegEx mask mode. This is a dependency property."), Category("Mask")]
        public AutoCompleteType MaskAutoComplete
        {
            get => 
                (AutoCompleteType) base.GetValue(MaskAutoCompleteProperty);
            set => 
                base.SetValue(MaskAutoCompleteProperty, value);
        }

        [Category("Mask"), Description("Gets or sets whether placeholders are displayed in a masked editor, for the RegEx mask type. This is a dependency property.")]
        public bool MaskShowPlaceHolders
        {
            get => 
                (bool) base.GetValue(MaskShowPlaceHoldersProperty);
            set => 
                base.SetValue(MaskShowPlaceHoldersProperty, value);
        }

        [Category("Mask"), Description("Gets or sets the character used as the placeholder in a masked editor, for the Simple, Regular and RegEx mask types. This is a dependency property.")]
        public char MaskPlaceHolder
        {
            get => 
                (char) base.GetValue(MaskPlaceHolderProperty);
            set => 
                base.SetValue(MaskPlaceHolderProperty, value);
        }

        [Category("Mask"), Description("Gets or sets the mask type. This is a dependency property.")]
        public DevExpress.Xpf.Editors.MaskType MaskType
        {
            get => 
                (DevExpress.Xpf.Editors.MaskType) base.GetValue(MaskTypeProperty);
            set => 
                base.SetValue(MaskTypeProperty, value);
        }

        [Category("Mask"), Description("Gets or sets a mask expression. This is a dependency property.")]
        public string Mask
        {
            get => 
                (string) base.GetValue(MaskProperty);
            set => 
                base.SetValue(MaskProperty, value);
        }

        [Category("Mask"), Description("Gets or sets whether the editor can lose focus when a value hasn't been entered, for the Simple, Regular and RegEx mask types. This is a dependency property.")]
        public bool MaskIgnoreBlank
        {
            get => 
                (bool) base.GetValue(MaskIgnoreBlankProperty);
            set => 
                base.SetValue(MaskIgnoreBlankProperty, value);
        }

        [Category("Mask"), Description("Gets or sets whether display values are still formatted using the mask when the editor is not focused. This is a dependency property.")]
        public bool MaskUseAsDisplayFormat
        {
            get => 
                (bool) base.GetValue(MaskUseAsDisplayFormatProperty);
            set => 
                base.SetValue(MaskUseAsDisplayFormatProperty, value);
        }

        [Category("Mask"), Description("Gets or sets whether an editor beeps when an end-user tries to enter an invalid character. This is a dependency property.")]
        public bool MaskBeepOnError
        {
            get => 
                (bool) base.GetValue(MaskBeepOnErrorProperty);
            set => 
                base.SetValue(MaskBeepOnErrorProperty, value);
        }

        [TypeConverter(typeof(CultureInfoConverter)), Description("Gets or sets the culture whose settings are used by masks. This is a dependency property."), Category("Mask")]
        public CultureInfo MaskCulture
        {
            get => 
                (CultureInfo) base.GetValue(MaskCultureProperty);
            set => 
                base.SetValue(MaskCultureProperty, value);
        }

        [Description("Gets or sets a value that specifies the text decorations that are applied to the editor's content. This is a dependency property."), Category("Text")]
        public TextDecorationCollection TextDecorations
        {
            get => 
                (TextDecorationCollection) base.GetValue(TextDecorationsProperty);
            set => 
                base.SetValue(TextDecorationsProperty, value);
        }

        [Description("Gets or sets the selected text."), Browsable(false)]
        public string SelectedText
        {
            get => 
                base.EditBox.SelectedText;
            set => 
                base.EditStrategy.SetSelectedText(value);
        }

        [Browsable(false)]
        public string HighlightedText
        {
            get => 
                (string) base.GetValue(HighlightedTextProperty);
            internal set => 
                base.SetValue(HighlightedTextPropertyKey, value);
        }

        [Browsable(false)]
        public DevExpress.Xpf.Editors.HighlightedTextCriteria HighlightedTextCriteria
        {
            get => 
                (DevExpress.Xpf.Editors.HighlightedTextCriteria) base.GetValue(HighlightedTextCriteriaProperty);
            internal set => 
                base.SetValue(HighlightedTextCriteriaPropertyKey, value);
        }

        [Description("Gets or sets the number of selected characters."), Browsable(false)]
        public int SelectionLength
        {
            get => 
                base.EditBox.SelectionLength;
            set => 
                base.EditStrategy.SetSelectionLength(value);
        }

        [Description("Gets or sets a character index at which the selection starts."), Browsable(false)]
        public int SelectionStart
        {
            get => 
                (base.EditBox == null) ? -1 : base.EditBox.SelectionStart;
            set => 
                base.EditStrategy.SetSelectionStart(value);
        }

        [Description("Gets or sets the position of the input caret."), Browsable(false)]
        public int CaretIndex
        {
            get => 
                (base.EditBox == null) ? -1 : base.EditBox.CaretIndex;
            set => 
                base.EditStrategy.CaretIndexChanged(value);
        }

        protected internal virtual DevExpress.Xpf.Editors.MaskType DefaultMaskType =>
            DevExpress.Xpf.Editors.MaskType.None;

        [Description("Increments the value of a masked editor.")]
        public ICommand SpinUpCommand { get; }

        [Description("Decrements the value of a masked editor.")]
        public ICommand SpinDownCommand { get; }

        [Description("Gets or sets the character casing applied to the editor's content. This is a dependency property.")]
        public System.Windows.Controls.CharacterCasing CharacterCasing
        {
            get => 
                (System.Windows.Controls.CharacterCasing) base.GetValue(CharacterCasingProperty);
            set => 
                base.SetValue(CharacterCasingProperty, value);
        }

        public bool ShowNullTextIfFocused
        {
            get => 
                (bool) base.GetValue(ShowNullTextIfFocusedProperty);
            set => 
                base.SetValue(ShowNullTextIfFocusedProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TextEdit.<>c <>9 = new TextEdit.<>c();
            public static Func<TextInputSettingsBase, EditStrategyBase> <>9__110_0;

            internal void <.cctor>b__19_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TextEdit) d).HighlightedTextCriteriaChanged((HighlightedTextCriteria) e.NewValue);
            }

            internal void <.cctor>b__19_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TextEdit) d).HighlightedTextChanged((string) e.NewValue);
            }

            internal void <.cctor>b__19_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TextEdit) d).OnCharacterCasingChanged((CharacterCasing) e.NewValue);
            }

            internal void <.cctor>b__19_3(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((TextEdit) o).ShowNullTextIfFocusedChanged((bool) args.OldValue, (bool) args.NewValue);
            }

            internal EditStrategyBase <CreateEditStrategy>b__110_0(TextInputSettingsBase x) => 
                x.CreateEditStrategy();
        }
    }
}

