namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.ReflectionExtensions;
    using DevExpress.Xpf.Editors.Automation;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;

    [DXToolboxBrowsable(DXToolboxItemKind.Free), ContentProperty("Content"), LicenseProvider(typeof(DX_WPFEditors_LicenseProvider))]
    public class CheckEdit : BaseEdit, IBooleanExportSettings, IExportSettings, ICommandSource
    {
        public static readonly DependencyProperty IsCheckedProperty;
        public static readonly DependencyProperty IsThreeStateProperty;
        public static readonly DependencyProperty ClickModeProperty;
        public static readonly DependencyProperty HasContentProperty;
        private static readonly DependencyPropertyKey HasContentPropertyKey;
        public static readonly DependencyProperty ContentProperty;
        public static readonly DependencyProperty ContentTemplateProperty;
        public static readonly DependencyProperty CommandProperty;
        public static readonly DependencyProperty CommandParameterProperty;
        public static readonly DependencyProperty CommandTargetProperty;
        public static readonly DependencyProperty ContentTemplateSelectorProperty;
        public static readonly DependencyProperty CheckedGlyphProperty;
        public static readonly DependencyProperty UncheckedGlyphProperty;
        public static readonly DependencyProperty IndeterminateGlyphProperty;
        public static readonly DependencyProperty ActualGlyphProperty;
        private static readonly DependencyPropertyKey ActualGlyphPropertyKey;
        public static readonly DependencyProperty GlyphTemplateProperty;
        public static readonly DependencyProperty DisplayModeProperty;
        private static readonly DependencyPropertyKey DisplayModePropertyKey;
        public static readonly RoutedEvent CheckedEvent;
        public static readonly RoutedEvent UncheckedEvent;
        public static readonly RoutedEvent IndeterminateEvent;

        [Category("Behavior")]
        public event RoutedEventHandler Checked
        {
            add
            {
                base.AddHandler(CheckedEvent, value);
            }
            remove
            {
                base.RemoveHandler(CheckedEvent, value);
            }
        }

        [Category("Behavior")]
        public event RoutedEventHandler Indeterminate
        {
            add
            {
                base.AddHandler(IndeterminateEvent, value);
            }
            remove
            {
                base.RemoveHandler(IndeterminateEvent, value);
            }
        }

        [Category("Behavior")]
        public event RoutedEventHandler Unchecked
        {
            add
            {
                base.AddHandler(UncheckedEvent, value);
            }
            remove
            {
                base.RemoveHandler(UncheckedEvent, value);
            }
        }

        static CheckEdit()
        {
            Type ownerType = typeof(CheckEdit);
            CommandProperty = DependencyPropertyManager.Register("Command", typeof(ICommand), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((CheckEdit) d).CommandChanged((ICommand) e.OldValue, (ICommand) e.NewValue)));
            CommandParameterProperty = DependencyPropertyManager.Register("CommandParameter", typeof(object), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((CheckEdit) d).CommandParameterChanged(e.NewValue)));
            CommandTargetProperty = DependencyPropertyManager.Register("CommandTarget", typeof(IInputElement), ownerType, new FrameworkPropertyMetadata(null));
            IsCheckedProperty = ToggleButton.IsCheckedProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(CheckEdit.OnIsCheckedChanged), new CoerceValueCallback(CheckEdit.CoerceIsCheckedProperty), true, UpdateSourceTrigger.PropertyChanged));
            IsThreeStateProperty = ToggleButton.IsThreeStateProperty.AddOwner(ownerType, new FrameworkPropertyMetadata((d, e) => ((CheckEdit) d).IsThreeStateChanged()));
            ContentProperty = ContentControl.ContentProperty.AddOwner(ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, (d, e) => ((CheckEdit) d).ContentChanged(e)));
            ContentTemplateProperty = ContentControl.ContentTemplateProperty.AddOwner(ownerType);
            ClickModeProperty = ButtonBase.ClickModeProperty.AddOwner(ownerType);
            HasContentPropertyKey = DependencyPropertyManager.RegisterReadOnly("HasContent", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            HasContentProperty = HasContentPropertyKey.DependencyProperty;
            CheckedEvent = EventManager.RegisterRoutedEvent("Checked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), ownerType);
            UncheckedEvent = EventManager.RegisterRoutedEvent("Unchecked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), ownerType);
            IndeterminateEvent = EventManager.RegisterRoutedEvent("Indeterminate", RoutingStrategy.Bubble, typeof(RoutedEventHandler), ownerType);
            BaseEdit.EditValueProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, null, true, UpdateSourceTrigger.PropertyChanged));
            ContentTemplateSelectorProperty = ContentControl.ContentTemplateSelectorProperty.AddOwner(ownerType);
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(ownerType));
            Control.VerticalContentAlignmentProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(VerticalAlignment.Center));
            BaseEdit.ShowBorderProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(false));
            FrameworkElement.DataContextProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (o, args) => ((CheckEdit) o).OnDataContextChanged()));
            CheckedGlyphProperty = DependencyProperty.Register("CheckedGlyph", typeof(ImageSource), ownerType, new PropertyMetadata(null, (o, args) => ((CheckEdit) o).UpdateActualGlyph()));
            UncheckedGlyphProperty = DependencyProperty.Register("UncheckedGlyph", typeof(ImageSource), ownerType, new PropertyMetadata(null, (o, args) => ((CheckEdit) o).UpdateActualGlyph()));
            IndeterminateGlyphProperty = DependencyProperty.Register("IndeterminateGlyph", typeof(ImageSource), ownerType, new PropertyMetadata(null, (o, args) => ((CheckEdit) o).UpdateActualGlyph()));
            ActualGlyphPropertyKey = DependencyProperty.RegisterReadOnly("ActualGlyph", typeof(ImageSource), ownerType, new PropertyMetadata(null));
            ActualGlyphProperty = ActualGlyphPropertyKey.DependencyProperty;
            GlyphTemplateProperty = DependencyProperty.Register("GlyphTemplate", typeof(DataTemplate), ownerType);
            DisplayModePropertyKey = DependencyProperty.RegisterReadOnly("DisplayMode", typeof(CheckEditDisplayMode), ownerType, new PropertyMetadata(CheckEditDisplayMode.Check));
            DisplayModeProperty = DisplayModePropertyKey.DependencyProperty;
        }

        protected virtual object CoerceIsChecked(object isChecked) => 
            this.EditStrategy.CoerceValue(IsCheckedProperty, isChecked);

        protected static object CoerceIsCheckedProperty(DependencyObject obj, object value) => 
            ((CheckEdit) obj).CoerceIsChecked((bool?) value);

        protected virtual void CommandCanExecuteChanged(object sender, EventArgs e)
        {
            this.EditStrategy.UpdateCanExecute(this.Command);
        }

        protected virtual void CommandChanged(ICommand oldValue, ICommand newValue)
        {
            if (oldValue != null)
            {
                oldValue.CanExecuteChanged -= new EventHandler(this.CommandCanExecuteChanged);
            }
            if (newValue != null)
            {
                newValue.CanExecuteChanged += new EventHandler(this.CommandCanExecuteChanged);
            }
            this.EditStrategy.UpdateCanExecute(newValue);
        }

        protected virtual void CommandParameterChanged(object parameter)
        {
            this.EditStrategy.UpdateCanExecute(this.Command);
        }

        protected virtual void ContentChanged(DependencyPropertyChangedEventArgs e)
        {
            this.HasContent = e.NewValue != null;
        }

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new CheckEditPropertyProvider(this);

        protected override EditStrategyBase CreateEditStrategy() => 
            new CheckEditStrategy(this);

        protected internal override BaseEditStyleSettings CreateStyleSettings() => 
            new CheckEditStyleSettings();

        private bool? GetCheckedState(object editValue)
        {
            if (editValue != null)
            {
                return (!(editValue as bool) ? false : new bool?((bool) editValue));
            }
            return null;
        }

        private static EditorStringId GetCheckStringId(bool? isChecked)
        {
            if (isChecked == null)
            {
                return EditorStringId.CheckIndeterminate;
            }
            bool? nullable = isChecked;
            bool flag = true;
            return (((nullable.GetValueOrDefault() == flag) ? ((EditorStringId) (nullable != null)) : ((EditorStringId) false)) ? EditorStringId.CheckChecked : EditorStringId.CheckUnchecked);
        }

        protected internal override string GetPlainText()
        {
            if (this.Content == null)
            {
                return base.GetPlainText();
            }
            FrameworkElement content = this.Content as FrameworkElement;
            return ((content == null) ? this.Content.ToString() : content.Wrap<IFrameworkElementInstance>().GetPlainText());
        }

        protected override string GetStateName()
        {
            if (this.IsChecked != null)
            {
                if (this.IsChecked.Value)
                {
                    return "Checked";
                }
                if (!this.IsChecked.Value)
                {
                    return "Unchecked";
                }
            }
            return "Indeterminate";
        }

        protected virtual void IsThreeStateChanged()
        {
            this.EditStrategy.UpdateCheckBoxValue();
        }

        protected override bool NeedsLeftRight() => 
            false;

        protected override bool NeedsUpDown() => 
            false;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UpdateVisualState(false);
        }

        protected internal virtual void OnChecked(RoutedEventArgs e)
        {
            base.RaiseEvent(e);
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new CheckEditAutomationPeer(this);

        private void OnDataContextChanged()
        {
            if (base.EditCore != null)
            {
                base.EditCore.DataContext = base.DataContext;
            }
        }

        protected override void OnEditCoreAssigned()
        {
            base.OnEditCoreAssigned();
            if (base.EditCore != null)
            {
                base.EditCore.DataContext = base.DataContext;
            }
        }

        private void OnEditCoreGotKeyboardFocus(object sender, RoutedEventArgs e)
        {
            this.UpdateEditCoreIsFocused();
        }

        private void OnEditCoreLostKeyboardFocus(object sender, RoutedEventArgs e)
        {
            this.UpdateEditCoreIsFocused();
        }

        protected internal virtual void OnIndeterminate(RoutedEventArgs e)
        {
            base.RaiseEvent(e);
        }

        protected virtual void OnIsCheckedChanged(bool? oldValue, bool? value)
        {
            this.EditStrategy.IsCheckedChanged(oldValue, value);
            this.UpdateVisualState(true);
            CheckEditAutomationPeer peer = (CheckEditAutomationPeer) UIElementAutomationPeer.FromElement(this);
            if (peer != null)
            {
                peer.RaiseToggleStatePropertyChangedEvent(oldValue, this.IsChecked);
            }
        }

        private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CheckEdit) d).OnIsCheckedChanged((bool?) e.OldValue, (bool?) e.NewValue);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            e.Handled ??= this.EditStrategy.ProcessKeyDown(e.Key);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            e.Handled ??= this.EditStrategy.ProcessKeyUp(e.Key);
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            if (!e.GetHandled())
            {
                e.SetHandled(this.EditStrategy.ProcessMouseEnter());
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            if (!e.GetHandled())
            {
                e.SetHandled(this.EditStrategy.ProcessMouseLeave());
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            e.Handled ??= this.EditStrategy.ProcessMouseLeftButtonDown();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            e.Handled ??= this.EditStrategy.ProcessMouseLeftButtonUp();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ReferenceEquals(e.Property, ControlHelper.IsFocusedProperty))
            {
                this.UpdateEditCoreIsFocused();
            }
        }

        protected internal virtual void OnUnchecked(RoutedEventArgs e)
        {
            base.RaiseEvent(e);
        }

        protected override void ProcessActivatingKeyCore(Key key, ModifierKeys modifiers)
        {
            base.ProcessActivatingKeyCore(key, modifiers);
            this.EditStrategy.ProcessKeyDown(key);
        }

        protected override void SubscribeEditEventsCore()
        {
            base.SubscribeEditEventsCore();
            base.EditCore.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(this.OnEditCoreGotKeyboardFocus);
            base.EditCore.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(this.OnEditCoreLostKeyboardFocus);
            this.UpdateEditCoreIsFocused();
        }

        public void Toggle()
        {
            this.EditStrategy.PerformToggle();
        }

        protected override void UnsubscribeEditEventsCore()
        {
            base.EditCore.GotKeyboardFocus -= new KeyboardFocusChangedEventHandler(this.OnEditCoreGotKeyboardFocus);
            base.EditCore.LostKeyboardFocus -= new KeyboardFocusChangedEventHandler(this.OnEditCoreLostKeyboardFocus);
            base.UnsubscribeEditEventsCore();
        }

        protected internal virtual void UpdateActualGlyph()
        {
            this.EditStrategy.UpdateActualGlyph();
        }

        protected internal override void UpdateBorderInInplaceMode()
        {
        }

        protected internal override void UpdateDataContext(DependencyObject target)
        {
        }

        private void UpdateEditCoreIsFocused()
        {
            if (base.EditCore != null)
            {
                if (base.GetBindingExpression(ControlHelper.IsFocusedProperty) == null)
                {
                    ControlHelper.SetShowFocusedState(base.EditCore, FocusHelper.IsKeyboardFocused(base.EditCore));
                }
                else
                {
                    ControlHelper.SetShowFocusedState(base.EditCore, ControlHelper.GetIsFocused(this) || FocusHelper.IsKeyboardFocused(base.EditCore));
                }
            }
        }

        protected internal override void UpdateGlowInInplaceMode()
        {
        }

        protected internal virtual void UpdateIsEnabledCore()
        {
            base.CoerceValue(UIElement.IsEnabledProperty);
        }

        private CheckEditStrategy EditStrategy =>
            (CheckEditStrategy) base.EditStrategy;

        protected internal CheckEditSettings Settings =>
            (CheckEditSettings) base.Settings;

        [Description("Gets or sets whether editor's content is specified. This is a dependency property.")]
        protected override bool IsEnabledCore =>
            base.IsEnabledCore && this.EditStrategy.IsEnabledCore;

        public ImageSource ActualGlyph
        {
            get => 
                (ImageSource) base.GetValue(ActualGlyphProperty);
            internal set => 
                base.SetValue(ActualGlyphPropertyKey, value);
        }

        public DataTemplate GlyphTemplate
        {
            get => 
                (DataTemplate) base.GetValue(GlyphTemplateProperty);
            set => 
                base.SetValue(GlyphTemplateProperty, value);
        }

        public ImageSource CheckedGlyph
        {
            get => 
                (ImageSource) base.GetValue(CheckedGlyphProperty);
            set => 
                base.SetValue(CheckedGlyphProperty, value);
        }

        public ImageSource UncheckedGlyph
        {
            get => 
                (ImageSource) base.GetValue(UncheckedGlyphProperty);
            set => 
                base.SetValue(UncheckedGlyphProperty, value);
        }

        public ImageSource IndeterminateGlyph
        {
            get => 
                (ImageSource) base.GetValue(IndeterminateGlyphProperty);
            set => 
                base.SetValue(IndeterminateGlyphProperty, value);
        }

        public bool HasContent
        {
            get => 
                (bool) base.GetValue(HasContentProperty);
            private set => 
                base.SetValue(HasContentPropertyKey, value);
        }

        [Category("Action")]
        public ICommand Command
        {
            get => 
                (ICommand) base.GetValue(CommandProperty);
            set => 
                base.SetValue(CommandProperty, value);
        }

        [Category("Action")]
        public object CommandParameter
        {
            get => 
                base.GetValue(CommandParameterProperty);
            set => 
                base.SetValue(CommandParameterProperty, value);
        }

        [Category("Action")]
        public IInputElement CommandTarget
        {
            get => 
                (IInputElement) base.GetValue(CommandTargetProperty);
            set => 
                base.SetValue(CommandTargetProperty, value);
        }

        [TypeConverter(typeof(NullableBoolConverter)), Category("Common Properties"), Description("Gets or sets whether an editor is checked. This is a dependency property.")]
        public bool? IsChecked
        {
            get => 
                (bool?) base.GetValue(IsCheckedProperty);
            set => 
                base.SetValue(IsCheckedProperty, value);
        }

        [Category("Behavior"), Description("Gets or sets whether the check editor supports three states (checked, unchecked and indeterminate).")]
        public bool IsThreeState
        {
            get => 
                (bool) base.GetValue(IsThreeStateProperty);
            set => 
                base.SetValue(IsThreeStateProperty, value);
        }

        [Category("Behavior"), Description("Gets or sets a value that specifies when the editor's state changes in response to end-user manipulations. This is a dependency property.")]
        public System.Windows.Controls.ClickMode ClickMode
        {
            get => 
                (System.Windows.Controls.ClickMode) base.GetValue(ClickModeProperty);
            set => 
                base.SetValue(ClickModeProperty, value);
        }

        [Description("Gets or sets a text displayed next to check box glyph. This is a dependency property."), Category("Content"), TypeConverter(typeof(ObjectConverter))]
        public object Content
        {
            get => 
                base.GetValue(ContentProperty);
            set => 
                base.SetValue(ContentProperty, value);
        }

        [Browsable(false), Description("Gets or sets the template that defines the presentation of the editor's content represented by the CheckEdit.Content property. This is a dependency property.")]
        public DataTemplate ContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ContentTemplateProperty);
            set => 
                base.SetValue(ContentTemplateProperty, value);
        }

        [Browsable(false), Description("Gets or sets an object that chooses the editor's content template based on custom logic. This is a dependency property.")]
        public DataTemplateSelector ContentTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ContentTemplateSelectorProperty);
            set => 
                base.SetValue(ContentTemplateSelectorProperty, value);
        }

        public CheckEditDisplayMode DisplayMode
        {
            get => 
                (CheckEditDisplayMode) base.GetValue(DisplayModeProperty);
            internal set => 
                base.SetValue(DisplayModePropertyKey, value);
        }

        protected internal override Type StyleSettingsType =>
            typeof(CheckEditStyleSettingsBase);

        protected internal CheckEditBox CheckBox =>
            base.EditCore as CheckEditBox;

        bool? IBooleanExportSettings.BooleanValue =>
            this.IsChecked;

        string IBooleanExportSettings.CheckText =>
            EditorLocalizer.GetString(GetCheckStringId(this.IsChecked));

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CheckEdit.<>c <>9 = new CheckEdit.<>c();

            internal void <.cctor>b__22_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CheckEdit) d).CommandChanged((ICommand) e.OldValue, (ICommand) e.NewValue);
            }

            internal void <.cctor>b__22_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CheckEdit) d).CommandParameterChanged(e.NewValue);
            }

            internal void <.cctor>b__22_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CheckEdit) d).IsThreeStateChanged();
            }

            internal void <.cctor>b__22_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CheckEdit) d).ContentChanged(e);
            }

            internal void <.cctor>b__22_4(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((CheckEdit) o).OnDataContextChanged();
            }

            internal void <.cctor>b__22_5(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((CheckEdit) o).UpdateActualGlyph();
            }

            internal void <.cctor>b__22_6(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((CheckEdit) o).UpdateActualGlyph();
            }

            internal void <.cctor>b__22_7(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((CheckEdit) o).UpdateActualGlyph();
            }
        }
    }
}

