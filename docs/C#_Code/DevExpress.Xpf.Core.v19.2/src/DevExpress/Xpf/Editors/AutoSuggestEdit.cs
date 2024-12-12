namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Popups;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    [DXToolboxBrowsable(DXToolboxItemKind.Free), LicenseProvider(typeof(DX_WPFEditors_LicenseProvider))]
    public class AutoSuggestEdit : PopupBaseEdit
    {
        public static readonly DependencyProperty AcceptValueOnPopupContentSelectionChangedProperty;
        public static readonly DependencyProperty ImmediatePopupProperty;
        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty ItemTemplateProperty;
        public static readonly DependencyProperty ItemTemplateSelectorProperty;
        public static readonly DependencyProperty ItemsPanelProperty;
        public static readonly DependencyProperty ItemContainerStyleProperty;
        public static readonly DependencyProperty ItemContainerStyleSelectorProperty;
        public static readonly DependencyProperty ItemStringFormatProperty;
        public static readonly DependencyProperty DisplayMemberProperty;
        public static readonly DependencyProperty TextMemberProperty;
        public static readonly DependencyProperty AllowPopupTextHighlightingProperty;
        public static readonly DependencyProperty PopupHighlightedTextProperty;
        private static readonly DependencyPropertyKey PopupHighlightedTextPropertyKey;
        public static readonly DependencyProperty PopupHighlightedTextCriteriaProperty;

        public event EventHandler<AutoSuggestEditCustomPopupHighlightedTextEventArgs> CustomPopupHighlightedText;

        public event EventHandler<AutoSuggestEditQuerySubmittedEventArgs> QuerySubmitted;

        public event EventHandler<AutoSuggestEditSuggestionChoosingEventArgs> SuggestionChoosing;

        public event EventHandler<AutoSuggestEditSuggestionChosenEventArgs> SuggestionChosen;

        public event EventHandler<AutoSuggestEditTextChangedEventArgs> TextChanged;

        static AutoSuggestEdit()
        {
            Type forType = typeof(AutoSuggestEdit);
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(forType));
            PopupBaseEdit.PopupMaxHeightProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(SystemParameters.PrimaryScreenHeight / 3.0));
            AcceptValueOnPopupContentSelectionChangedProperty = DependencyProperty.Register("AcceptValueOnPopupContentSelectionChanged", typeof(bool), forType, new PropertyMetadata(false));
            BaseEdit.ValidateOnTextInputProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(false));
            ImmediatePopupProperty = DependencyProperty.Register("ImmediatePopup", typeof(bool), forType, new PropertyMetadata(false));
            ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(object), forType, new PropertyMetadata(null, (o, args) => ((AutoSuggestEdit) o).ItemsSourceChanged(args.NewValue)));
            ItemTemplateProperty = ItemsControl.ItemTemplateProperty.AddOwner(forType, new FrameworkPropertyMetadata(null));
            ItemsPanelProperty = ItemsControl.ItemsPanelProperty.AddOwner(forType, new FrameworkPropertyMetadata(ItemsControl.ItemsPanelProperty.DefaultMetadata.DefaultValue));
            ItemTemplateSelectorProperty = ItemsControl.ItemTemplateSelectorProperty.AddOwner(forType, new FrameworkPropertyMetadata(null));
            ItemContainerStyleProperty = ItemsControl.ItemContainerStyleProperty.AddOwner(forType);
            ItemContainerStyleSelectorProperty = ItemsControl.ItemContainerStyleSelectorProperty.AddOwner(forType);
            ItemStringFormatProperty = ItemsControl.ItemStringFormatProperty.AddOwner(forType);
            DisplayMemberProperty = DependencyProperty.Register("DisplayMember", typeof(string), forType, new PropertyMetadata(null, (o, args) => ((AutoSuggestEdit) o).DisplayMemberChanged((string) args.NewValue)));
            TextMemberProperty = DependencyProperty.Register("TextMember", typeof(string), forType, new PropertyMetadata(null, (o, args) => ((AutoSuggestEdit) o).TextMemberChanged((string) args.NewValue)));
            AllowPopupTextHighlightingProperty = DependencyProperty.Register("AllowPopupTextHighlighting", typeof(bool), forType, new PropertyMetadata(false));
            PopupHighlightedTextPropertyKey = DependencyProperty.RegisterReadOnly("PopupHighlightedText", typeof(string), forType, new PropertyMetadata(null));
            PopupHighlightedTextProperty = PopupHighlightedTextPropertyKey.DependencyProperty;
            PopupHighlightedTextCriteriaProperty = DependencyProperty.Register("PopupHighlightedTextCriteria", typeof(HighlightedTextCriteria), forType, new PropertyMetadata(HighlightedTextCriteria.StartsWith));
        }

        protected override void AcceptPopupValue()
        {
            base.AcceptPopupValue();
            this.EditStrategy.AcceptPopupValue(false);
        }

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new AutoSuggestEditPropertyProvider(this);

        protected override EditStrategyBase CreateEditStrategy() => 
            new AutoSuggestEditStrategy(this);

        protected internal override BaseEditStyleSettings CreateStyleSettings() => 
            new AutoSuggestEditListBoxStyleSettings();

        protected internal override TextInputSettingsBase CreateTextInputSettings() => 
            (base.MaskType == MaskType.None) ? ((TextInputSettingsBase) new TextInputAutoSuggestSettings(this)) : ((TextInputSettingsBase) new TextInputAutoSuggestMaskSettings(this));

        protected override VisualClientOwner CreateVisualClient() => 
            new AutoSuggestVisualClientOwner(this);

        protected internal override void DestroyPopupContent(EditorPopupBase popup)
        {
            base.DestroyPopupContent(popup);
            this.EditStrategy.PopupDestroyed();
        }

        protected virtual void DisplayMemberChanged(string newValue)
        {
            this.EditStrategy.DisplayMemberChanged(newValue);
        }

        protected override bool IsClosePopupWithAcceptGesture(Key key, ModifierKeys modifiers) => 
            base.IsClosePopupWithAcceptGesture(key, modifiers) || this.EditStrategy.IsClosePopupWithAcceptGesture(key, modifiers);

        protected override bool IsClosePopupWithCancelGesture(Key key, ModifierKeys modifiers) => 
            base.IsClosePopupWithCancelGesture(key, modifiers) || this.EditStrategy.IsClosePopupWithCancelGesture(key, modifiers);

        protected virtual void ItemsSourceChanged(object newValue)
        {
            this.EditStrategy.ItemsSourceChanged(newValue);
        }

        protected internal string RaiseCustomPopupHighlightedText(string text)
        {
            EventHandler<AutoSuggestEditCustomPopupHighlightedTextEventArgs> customPopupHighlightedText = this.CustomPopupHighlightedText;
            if (customPopupHighlightedText == null)
            {
                return Regex.Escape(text);
            }
            AutoSuggestEditCustomPopupHighlightedTextEventArgs e = new AutoSuggestEditCustomPopupHighlightedTextEventArgs(text);
            customPopupHighlightedText(this, e);
            return (e.Handled ? e.Text : Regex.Escape(text));
        }

        protected internal void RaiseQuerySubmitted(object value)
        {
            EventHandler<AutoSuggestEditQuerySubmittedEventArgs> querySubmitted = this.QuerySubmitted;
            if (querySubmitted != null)
            {
                querySubmitted(this, new AutoSuggestEditQuerySubmittedEventArgs(value?.ToString()));
            }
        }

        protected internal object RaiseSuggestionChoosing(object value)
        {
            EventHandler<AutoSuggestEditSuggestionChoosingEventArgs> suggestionChoosing = this.SuggestionChoosing;
            if (suggestionChoosing != null)
            {
                AutoSuggestEditSuggestionChoosingEventArgs e = new AutoSuggestEditSuggestionChoosingEventArgs(value);
                suggestionChoosing(this, e);
                if (e.Handled)
                {
                    return e.SelectedItem;
                }
            }
            return value;
        }

        protected internal void RaiseSuggestionChosen(object value)
        {
            EventHandler<AutoSuggestEditSuggestionChosenEventArgs> suggestionChosen = this.SuggestionChosen;
            if (suggestionChosen != null)
            {
                AutoSuggestEditSuggestionChosenEventArgs e = new AutoSuggestEditSuggestionChosenEventArgs(value);
                suggestionChosen(this, e);
            }
        }

        protected internal void RaiseTextChanged(string text, AutoSuggestEditChangeTextReason reason)
        {
            EventHandler<AutoSuggestEditTextChangedEventArgs> textChanged = this.TextChanged;
            if (textChanged != null)
            {
                AutoSuggestEditTextChangedEventArgs e = new AutoSuggestEditTextChangedEventArgs(text, reason);
                textChanged(this, e);
            }
        }

        public void SetEditText(string text)
        {
            base.EditBox.EditValue = text;
        }

        protected virtual void TextMemberChanged(string newValue)
        {
            this.EditStrategy.TextMemberChanged(newValue);
        }

        public bool AcceptValueOnPopupContentSelectionChanged
        {
            get => 
                (bool) base.GetValue(AcceptValueOnPopupContentSelectionChangedProperty);
            set => 
                base.SetValue(AcceptValueOnPopupContentSelectionChangedProperty, value);
        }

        public bool ImmediatePopup
        {
            get => 
                (bool) base.GetValue(ImmediatePopupProperty);
            set => 
                base.SetValue(ImmediatePopupProperty, value);
        }

        public object ItemsSource
        {
            get => 
                base.GetValue(ItemsSourceProperty);
            set => 
                base.SetValue(ItemsSourceProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ItemTemplateProperty);
            set => 
                base.SetValue(ItemTemplateProperty, value);
        }

        public ItemsPanelTemplate ItemsPanel
        {
            get => 
                (ItemsPanelTemplate) base.GetValue(ItemsPanelProperty);
            set => 
                base.SetValue(ItemsPanelProperty, value);
        }

        public DataTemplateSelector ItemTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ItemTemplateSelectorProperty);
            set => 
                base.SetValue(ItemTemplateSelectorProperty, value);
        }

        public Style ItemContainerStyle
        {
            get => 
                (Style) base.GetValue(ItemContainerStyleProperty);
            set => 
                base.SetValue(ItemContainerStyleProperty, value);
        }

        public StyleSelector ItemContainerStyleSelector
        {
            get => 
                (StyleSelector) base.GetValue(ItemContainerStyleSelectorProperty);
            set => 
                base.SetValue(ItemContainerStyleSelectorProperty, value);
        }

        public string ItemStringFormat
        {
            get => 
                (string) base.GetValue(ItemStringFormatProperty);
            set => 
                base.SetValue(ItemStringFormatProperty, value);
        }

        public string DisplayMember
        {
            get => 
                (string) base.GetValue(DisplayMemberProperty);
            set => 
                base.SetValue(DisplayMemberProperty, value);
        }

        public string TextMember
        {
            get => 
                (string) base.GetValue(TextMemberProperty);
            set => 
                base.SetValue(TextMemberProperty, value);
        }

        public bool AllowPopupTextHighlighting
        {
            get => 
                (bool) base.GetValue(AllowPopupTextHighlightingProperty);
            set => 
                base.SetValue(AllowPopupTextHighlightingProperty, value);
        }

        public string PopupHighlightedText
        {
            get => 
                (string) base.GetValue(PopupHighlightedTextProperty);
            protected internal set => 
                base.SetValue(PopupHighlightedTextPropertyKey, value);
        }

        public HighlightedTextCriteria PopupHighlightedTextCriteria
        {
            get => 
                (HighlightedTextCriteria) base.GetValue(TextEdit.HighlightedTextCriteriaProperty);
            set => 
                base.SetValue(PopupHighlightedTextCriteriaProperty, value);
        }

        private AutoSuggestEditStrategy EditStrategy =>
            base.EditStrategy as AutoSuggestEditStrategy;

        protected internal override Type StyleSettingsType =>
            typeof(AutoSuggestEditStyleSettings);

        public override FrameworkElement PopupElement =>
            base.VisualClient.InnerEditor;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AutoSuggestEdit.<>c <>9 = new AutoSuggestEdit.<>c();

            internal void <.cctor>b__15_0(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((AutoSuggestEdit) o).ItemsSourceChanged(args.NewValue);
            }

            internal void <.cctor>b__15_1(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((AutoSuggestEdit) o).DisplayMemberChanged((string) args.NewValue);
            }

            internal void <.cctor>b__15_2(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((AutoSuggestEdit) o).TextMemberChanged((string) args.NewValue);
            }
        }
    }
}

